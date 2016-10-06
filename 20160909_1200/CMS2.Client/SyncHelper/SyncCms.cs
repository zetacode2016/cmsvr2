using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CMS2.Common;
using CMS2.DataAccess;
using CMS2.Entities;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace CMS2.Client.SyncHelper
{
    public class SyncCms
    {
        ConnectionStringSettings centralSettings = ConfigurationManager.ConnectionStrings["CmsCentral"];
        ConnectionStringSettings clientSettings = ConfigurationManager.ConnectionStrings["Cms"];

        private SqlConnection serverConn;
        private SqlConnection clientConn;

        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        public SyncCms()
        {
            serverConn = new SqlConnection(centralSettings.ConnectionString);
            clientConn = new SqlConnection(clientSettings.ConnectionString);
        }

        #region SyncFramework
        public bool SyncEntity(string entityName, SyncDirectionOrder syncDirection)
        {

            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(entityName);

            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(entityName, clientConn);
            scopeDesc.Tables.Add(tableDesc);

            try
            {
                SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
                serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision.ObjectSchema = ".dbo";
                if (!serverProvision.ScopeExists(entityName))
                    serverProvision.Apply();

                SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);
                if (!clientProvision.ScopeExists(entityName))
                    clientProvision.Apply();


                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(entityName, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(entityName, serverConn);

                syncOrchestrator.Direction = syncDirection;

                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Sync Error: " + ex.Message);
                return false;
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (serverConn.State == ConnectionState.Closed)
                    serverConn.Open();
            }
            catch (Exception ex)
            { }

            try
            {
                if (clientConn.State == ConnectionState.Closed)
                    clientConn.Open();
            }
            catch (Exception ex)
            { }
        }

        public void CloseConnection()
        {
            try
            {
                if (serverConn.State == ConnectionState.Open)
                    serverConn.Close();
            }
            catch (Exception ex) { }

            try
            {
                if (clientConn.State == ConnectionState.Open)
                    clientConn.Close();
            }
            catch (Exception ex) { }

        }

        public bool IsCentralConnected()
        {
            if (serverConn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeProvision(string entity)
        {
            string insertString = "";
            CmsContext context = new CmsContext();
            string dbObjName = "";
            try
            {
                dbObjName = entity + "_bulkdelete";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_bulkinsert";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_bulkupdate";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_delete";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_deletemetadata";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_insert";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_insertmetadata";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_update";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_updatemetadata";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_selectchanges";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_selectrow";
                insertString = "IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = '" + dbObjName + "') drop PROCEDURE " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);

                dbObjName = entity + "_tracking";
                insertString = "IF EXISTS(select 1 from sysobjects where name='" + dbObjName + "') drop table " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);

                dbObjName = entity + "_insert_trigger";
                insertString = "IF EXISTS(select 1 from sys.triggers where name='" + dbObjName + "') drop trigger " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_update_trigger";
                insertString = "IF EXISTS(select 1 from sys.triggers where name='" + dbObjName + "') drop trigger " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);
                dbObjName = entity + "_delete_trigger";
                insertString = "IF EXISTS(select 1 from sys.triggers where name='" + dbObjName + "') drop trigger " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);

                dbObjName = entity + "_bulktype";
                insertString = "IF EXISTS(select 1 from sys.types where is_table_type = 1 and name='" + dbObjName + "') drop type " + dbObjName;
                context.Database.ExecuteSqlCommand(insertString);

                insertString = "delete from scope_config where config_id=(select scope_config_id from scope_info where sync_scope_name='" + entity + "') delete from scope_info where sync_scope_name='" + entity + "'";
                context.Database.ExecuteSqlCommand(insertString);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Create Table: " + ex.Message);
            }
        }
        #endregion

        #region CustomSync
        public void SyncEntity(string entity, string direction)
        {
            switch (entity)
            {
                case "ApplicableRate":
                    var syncApplicableRate = new SyncEntity<ApplicableRate>();
                    syncApplicableRate.Sync(direction);
                    break;
                case "ApprovingAuthority":
                    var syncApprovingAuthority = new SyncEntity<ApprovingAuthority>();
                    syncApprovingAuthority.Sync(direction);
                    break;
                case "Booking":
                    var syncBooking = new SyncEntity<Booking>();
                    if (direction.Equals("Download"))
                    {
                        syncBooking.Sync(direction, z => z.DestinationCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                        syncBooking.Sync(direction, z => z.OriginCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                    }
                    else
                    {
                        syncBooking.Sync(direction);
                    }
                    break;
                case "Company":
                    var syncCompany = new SyncEntity<Company>();
                    syncCompany.Sync(direction); 
                    break;
                case "Client":
                    var syncClient = new SyncEntity<Entities.Client>();
                    syncClient.Sync(direction);
                    break;
                case "Commodity":
                    var syncCommodity = new SyncEntity<Commodity>();
                    syncCommodity.Sync(direction);
                    break;
                case "CommodityType":
                    var syncCommodityType = new SyncEntity<CommodityType>();
                    syncCommodityType.Sync(direction);
                    break;
                case "Crating":
                    var syncCrating = new SyncEntity<Crating>();
                    syncCrating.Sync(direction);
                    break;
                case "DeliveredPackage":
                    var syncDeliveredPackage = new SyncEntity<DeliveredPackage>();
                    syncDeliveredPackage.Sync(direction);
                    break;
                case "Delivery":
                    var syncDelivery = new SyncEntity<Delivery>();
                    syncDelivery.Sync(direction);
                    break;
                case "DeliveryReceipt":
                    var syncDeliveryReceipt = new SyncEntity<DeliveryReceipt>();
                    syncDeliveryReceipt.Sync(direction);
                    break;
                case "DeliveryRemark":
                    var syncDeliveryRemark = new SyncEntity<DeliveryRemark>();
                    syncDeliveryRemark.Sync(direction);
                    break;
                case "DeliveryStatus":
                    var syncDeliveryStatus = new SyncEntity<DeliveryStatus>();
                    syncDeliveryStatus.Sync(direction);
                    break;
                case "Employee":
                    var syncEmployee = new SyncEntity<Employee>();
                    syncEmployee.Sync(direction);
                    break;
                case "EmployeePositionMapping":
                    var syncEmployeePositionMapping = new SyncEntity<EmployeePositionMapping>();
                    if (direction.Equals("Download"))
                    {
                        var id = GlobalVars.DeviceBcoId;
                        if (GlobalVars.DeviceRevenueUnitId != Guid.Empty)
                            id = GlobalVars.DeviceRevenueUnitId;
                        syncEmployeePositionMapping.Sync(direction, y => y.AssignedLocationId == id);
                    }
                    else
                    {
                        syncEmployeePositionMapping.Sync(direction);
                    }
                    break;
                case "Department":
                    var syncDepartment = new SyncEntity<Department>();
                    syncDepartment.Sync(direction);
                    break;
                case "ExpressRate":
                    var syncExpressRate = new SyncEntity<ExpressRate>();
                    syncExpressRate.Sync(direction);
                    break;
                case "FuelSurcharge":
                    var syncFuelSurcharge = new SyncEntity<FuelSurcharge>();
                    syncFuelSurcharge.Sync(direction);
                    break;
                case "PackageDimension":
                    var syncPackageDimension = new SyncEntity<PackageDimension>();
                    if (direction.Equals("Download"))
                    {
                        syncPackageDimension.Sync(direction, z => z.Shipment.DestinationCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                        syncPackageDimension.Sync(direction, z => z.Shipment.OriginCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                    }
                    else
                    {
                        syncPackageDimension.Sync(direction);
                    }
                    break;
                case "PackageNumber":
                    var syncPackageNumber = new SyncEntity<PackageNumber>();
                    if (direction.Equals("Download"))
                    {
                        syncPackageNumber.Sync(direction, z => z.Shipment.DestinationCity.BranchCorpOfficeId ==GlobalVars.DeviceBcoId);
                        syncPackageNumber.Sync(direction, z => z.Shipment.OriginCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                    }
                    else
                    {
                        syncPackageNumber.Sync(direction);
                    }
                    break;
                case "Packaging":
                    var syncPackaging = new SyncEntity<Packaging>();
                    syncPackaging.Sync(direction);
                    break;
                case "Payment":
                    var syncPayment = new SyncEntity<Payment>();
                    if (direction.Equals("Download"))
                    {
                        syncPayment.Sync(direction, z => z.Shipment.DestinationCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                        syncPayment.Sync(direction, z => z.Shipment.OriginCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                    }
                    else
                    {
                        syncPayment.Sync(direction);
                    }
                    break;
                case "PaymentTurnover":
                    var syncPaymentTurnover = new SyncEntity<PaymentTurnover>();
                    syncPaymentTurnover.Sync(direction);
                    break;
                case "Position":
                    var syncPosition = new SyncEntity<Position>();
                    syncPosition.Sync(direction);
                    break;
                case "RateMatrix":
                    var syncRateMatrix = new SyncEntity<RateMatrix>();
                    syncRateMatrix.Sync(direction);
                    break;
                case "ServiceMode":
                    var syncServiceMode = new SyncEntity<ServiceMode>();
                    syncServiceMode.Sync(direction);
                    break;
                case "ServiceType":
                    var syncServiceType = new SyncEntity<ServiceType>();
                    syncServiceType.Sync(direction);
                    break;
                case "Shipment":
                    var syncShipment = new SyncEntity<Shipment>();
                    if (direction.Equals("Download"))
                    {
                        syncShipment.Sync(direction, z => z.DestinationCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                        syncShipment.Sync(direction,z => z.OriginCity.BranchCorpOfficeId == GlobalVars.DeviceBcoId);
                    }
                    else
                    {
                        syncShipment.Sync(direction);
                    }
                    break;
                case "ShipmentAdjustment":
                    var syncShipmentAdjustment = new SyncEntity<ShipmentAdjustment>();
                    syncShipmentAdjustment.Sync(direction);
                    break;
                case "ShipmentBasicFee":
                    var syncShipmentBasicFee = new SyncEntity<ShipmentBasicFee>();
                    syncShipmentBasicFee.Sync(direction);
                    break;
                case "ShipMode":
                    var syncShipMode = new SyncEntity<ShipMode>();
                    syncShipMode.Sync(direction);
                    break;
                case "StatementOfAccount":
                    var syncStatementOfAccount = new SyncEntity<StatementOfAccount>();
                    if (direction.Equals("Download"))
                    {
                        syncStatementOfAccount.Sync(direction);
                    }
                    else
                    {
                        syncStatementOfAccount.Sync(direction);
                    }
                    break;
                case "StatementOfAccountPayment":
                    var syncStatementOfAccountPayment = new SyncEntity<StatementOfAccountPayment>();
                   syncStatementOfAccountPayment.Sync(direction);
                    break;
                case "TransShipmentRoute":
                    var syncTransShipmentRoute = new SyncEntity<TransShipmentRoute>();
                    syncTransShipmentRoute.Sync(direction);
                    break;
                case "TransShipmentLeg":
                    var syncTransShipmentLeg = new SyncEntity<TransShipmentLeg>();
                    syncTransShipmentLeg.Sync(direction);
                    break;
                case "Truck":
                    var syncTruck = new SyncEntity<Truck>();
                    syncTruck.Sync(direction);
                    break;
                case "TruckAreaMapping":
                    var syncTruckAreaMapping = new SyncEntity<TruckAreaMapping>();
                    syncTruckAreaMapping.Sync(direction);
                    break;
                case "Role":
                    var syncRole = new SyncEntity<Role>();
                    syncRole.Sync(direction);
                    break;
                case "User":
                    var syncUser = new SyncEntity<User>();
                    if (direction.Equals("Download"))
                    {
                        var id = GlobalVars.DeviceBcoId;
                        if (GlobalVars.DeviceRevenueUnitId != Guid.Empty)
                            id = GlobalVars.DeviceRevenueUnitId;
                        syncUser.Sync(direction, x =>x.Employee.EmployeePositionMappings.Where(y => y.AssignedLocationId == id && y.RecordStatus==1).Select(y => y.EmployeeId).ToList().Contains(x.EmployeeId));
                    }
                    else
                    {
                        syncUser.Sync(direction);
                    }
                    break;
                case "RoleUser":
                    //var syncRoleUser = new SyncEntity<RoleUser>();
                    //syncRoleUser.Sync(direction);
                    break;
                case "Group":
                    var syncGroup = new SyncEntity<Group>();
                    syncGroup.Sync(direction);
                    break;
                case "Region":
                    var syncRegion = new SyncEntity<Region>();
                    syncRegion.Sync(direction);
                    break;
                case "BranchCorpOffice":
                    var syncBranchCorpOffice = new SyncEntity<BranchCorpOffice>();
                    syncBranchCorpOffice.Sync(direction);
                    break;
                case "Cluster":
                    var syncCluster = new SyncEntity<Cluster>();
                    syncCluster.Sync(direction);
                    break;
                case "City":
                    var syncCity = new SyncEntity<City>();
                    syncCity.Sync(direction);
                    break;
                case "RevenueUnitType":
                    var syncRevenueUnitType = new SyncEntity<RevenueUnitType>();
                    syncRevenueUnitType.Sync(direction);
                    break;
                case "RevenueUnit":
                    var syncRevenueUnit = new SyncEntity<RevenueUnit>();
                    syncRevenueUnit.Sync(direction);
                    break;
                case "AccountStatus":
                    var syncAccountStatus = new SyncEntity<AccountStatus>();
                    syncAccountStatus.Sync(direction);
                    break;
                case "AccountType":
                    var syncAccountType = new SyncEntity<AccountType>();
                    syncAccountType.Sync(direction);
                    break;
                case "AdjustmentReason":
                    var syncAdjustmentReason = new SyncEntity<AdjustmentReason>();
                    syncAdjustmentReason.Sync(direction);
                    break;
                case "ApplicationSetting":
                    var syncApplicationSetting = new SyncEntity<ApplicationSetting>();
                    syncApplicationSetting.Sync(direction);
                    break;
                case "BookingRemark":
                    var syncBookingRemark = new SyncEntity<BookingRemark>();
                    syncBookingRemark.Sync(direction);
                    break;
                case "BookingStatus":
                    var syncBookingStatus = new SyncEntity<BookingStatus>();
                    syncBookingStatus.Sync(direction);
                    break;
                case "BusinessType":
                    var syncBusinessType = new SyncEntity<BusinessType>();
                    syncBusinessType.Sync(direction);
                    break;
                case "Industry":
                    var syncIndustry = new SyncEntity<Industry>();
                    syncIndustry.Sync(direction);
                    break;
                case "OrganizationType":
                    var syncOrganizationType = new SyncEntity<OrganizationType>();
                    syncOrganizationType.Sync(direction);
                    break;
                case "BillingPeriod":
                    var syncBillingPeriod = new SyncEntity<BillingPeriod>();
                    syncBillingPeriod.Sync(direction);
                    break;
                case "PaymentMode":
                    var syncPaymentMode = new SyncEntity<PaymentMode>();
                    syncPaymentMode.Sync(direction);
                    break;
                case "PaymentTerm":
                    var syncPaymentTerm = new SyncEntity<PaymentTerm>();
                    syncPaymentTerm.Sync(direction);
                    break;
                case "PaymentType":
                    var syncPaymentType = new SyncEntity<PaymentType>();
                    syncPaymentType.Sync(direction);
                    break;
                case "GatewayType":
                    //var syncGatewayType = new SyncEntity<GatewayType>();
                    //syncGatewayType.Sync(direction);
                    break;
                case "Gateway":
                    //var syncGateway = new SyncEntity<Gateway>();
                    //syncGateway.Sync(direction);
                    break;
                case "GoodsDescription":
                    var syncGoodsDescription = new SyncEntity<GoodsDescription>();
                    syncGoodsDescription.Sync(direction);
                    break;

            }

        }
        #endregion

    }

    public class SyncEntity<TEntity> where TEntity : class, new()
    {
        static ConnectionStringSettings centralSettings = ConfigurationManager.ConnectionStrings["CmsCentral"];
        static ConnectionStringSettings localSettings = ConfigurationManager.ConnectionStrings["Cms"];

        static SqlConnection centralConn = new SqlConnection(centralSettings.ConnectionString);
        static SqlConnection localConn = new SqlConnection(localSettings.ConnectionString);

        static CmsContext centralContext = new CmsContext(centralConn.ConnectionString);
        static CmsContext localContext = new CmsContext(localConn.ConnectionString);

        private DbSet<TEntity> centralDbSet = centralContext.Set<TEntity>();
        DbSet<TEntity> localDbSet = localContext.Set<TEntity>();

        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        public void Sync(string direction, Expression<Func<TEntity, bool>> filter = null)
        {

            string propertyName = "";
            string primaryKey = "";
            Guid primaryValue = new Guid();
            bool IsNew = false;
            bool isNotMapped = false;
            bool isForeign = false;
            bool isList = false;

            try
            {
                if (localContext.Database.Connection.State == ConnectionState.Closed)
                    localContext.Database.Connection.Open();
                if (centralContext.Database.Connection.State == ConnectionState.Closed)
                    centralContext.Database.Connection.Open();
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    logs.ErrorLogs(LogPath, "CustomSyncServerConnection", ex.Message);
                else
                    logs.ErrorLogs(LogPath, "CustomSyncServerConnection", ex.InnerException.Message);

                return;
            }

            switch (direction)
            {
                case "Download":
                    try
                    {
                        List<TEntity> centralEntities = centralDbSet.ToList();
                        if (filter != null)
                            centralEntities = centralDbSet.Where(filter).ToList();
                        foreach (TEntity centralEntity in centralEntities)
                        {
                            IsNew = false;
                            TEntity localEntity = null;
                            primaryKey = GetPrimaryKey(centralEntity);
                            primaryValue = Guid.Parse(GetEntityId(centralEntity).ToString());
                            localEntity = localDbSet.Find(primaryValue);
                            if (localEntity == null)
                            {
                                IsNew = true;
                                localEntity = new TEntity();
                            }
                            foreach (PropertyInfo localproperty in localEntity.GetType().GetProperties())
                            {
                                propertyName = localproperty.Name;
                                var propertyAttributes = centralEntity.GetType().GetProperty(propertyName).GetCustomAttributes(false);
                                var customAttributes = localproperty.CustomAttributes;
                                isNotMapped = false;
                                isForeign = false;
                                isList = false;
                                foreach (var att in propertyAttributes)
                                {
                                    if (att.GetType() == typeof(NotMappedAttribute))
                                    {
                                        isNotMapped = true;
                                        break;
                                    }
                                }
                                foreach (var att in customAttributes)
                                {
                                    if (att.AttributeType.Name.Equals("ForeignKeyAttribute"))
                                    {
                                        isForeign = true;
                                        break;
                                    }
                                }
                                if (localproperty.PropertyType.BaseType != null)
                                {
                                    if (localproperty.PropertyType.BaseType.Name.Equals("BaseEntity"))
                                    {
                                        isForeign = true;
                                    }
                                }
                                if (localproperty.PropertyType.Name.Contains("List"))
                                {
                                    isList = true;
                                }
                                var centralpropertyValue = centralEntity.GetType().GetProperty(propertyName).GetValue(centralEntity);
                                if (propertyName.Equals("Record_Status") || // specific properties to be excluded
                                    propertyName.Equals("RecordStatusString") ||
                                    propertyName.Equals("FullName") ||
                                    isNotMapped ||
                                    isForeign ||
                                    isList
                                    )
                                { }
                                else
                                {
                                    if (IsNew)
                                    {
                                        localproperty.SetValue(localEntity, centralpropertyValue);
                                    }
                                    else
                                    {
                                        if (propertyName.Equals(primaryKey) || // specific properties to be excluded
                                            propertyName.Contains("Created"))
                                        {
                                        }
                                        else
                                        {
                                            localproperty.SetValue(localEntity, centralpropertyValue);
                                        }
                                    }
                                }
                            }
                            if (IsNew)
                            {
                                try
                                {
                                    localDbSet.Add(localEntity);
                                    localContext.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    if (ex.InnerException==null)
                                    logs.ErrorLogs(LogPath, "AutoSyncDownloadAdd", ex.Message);
                                    else
                                        logs.ErrorLogs(LogPath, "AutoSyncDownloadAdd", ex.InnerException.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    localContext.Entry(localEntity).State = EntityState.Modified;
                                    localContext.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    if (ex.InnerException == null)
                                        logs.ErrorLogs(LogPath, "AutoSyncDownloadUpdate", ex.Message);
                                    else
                                        logs.ErrorLogs(LogPath, "AutoSyncDownloadUpdate", ex.InnerException.Message);
                                }
                            }

                        }
                        
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null)
                            logs.ErrorLogs(LogPath, "AutoSyncDownload", ex.Message);
                        else
                            logs.ErrorLogs(LogPath, "AutoSyncDownload", ex.InnerException.Message);
                    }
                    break;
                case "Upload":
                    try
                    {
                        List<TEntity> localEntities = localDbSet.ToList();
                        foreach (TEntity _localEntity in localEntities)
                        {
                            IsNew = false;
                            TEntity centralEntity = null;
                            primaryKey = GetPrimaryKey(_localEntity);
                            primaryValue = Guid.Parse(GetEntityId(_localEntity).ToString());
                            centralEntity = centralDbSet.Find(primaryValue);
                            if (centralEntity == null)
                            {
                                IsNew = true;
                                centralEntity = new TEntity();
                            }
                            foreach (PropertyInfo centralproperty in centralEntity.GetType().GetProperties())
                            {
                                propertyName = centralproperty.Name;
                                var propertyAttributes = _localEntity.GetType().GetProperty(propertyName).GetCustomAttributes(false);
                                var customAttributes = centralproperty.CustomAttributes;
                                isNotMapped = false;
                                isForeign = false;
                                isList = false;
                                foreach (var att in propertyAttributes)
                                {
                                    if (att.GetType() == typeof(NotMappedAttribute))
                                    {
                                        isNotMapped = true;
                                        break;
                                    }
                                }
                                foreach (var att in customAttributes)
                                {
                                    if (att.AttributeType.Name.Equals("ForeignKeyAttribute"))
                                    {
                                        isForeign = true;
                                        break;
                                    }
                                }
                                if (centralproperty.PropertyType.BaseType != null)
                                {
                                    if (centralproperty.PropertyType.BaseType.Name.Equals("BaseEntity"))
                                    {
                                        isForeign = true;
                                    }
                                }
                                if (centralproperty.PropertyType.Name.Contains("List"))
                                {
                                    isList = true;
                                }
                                var localpropertyValue = _localEntity.GetType().GetProperty(propertyName).GetValue(_localEntity);
                                if (propertyName.Equals("Record_Status") || // specific properties to be excluded
                                    propertyName.Equals("RecordStatusString") ||
                                    propertyName.Equals("FullName") ||
                                    isNotMapped ||
                                    isForeign ||
                                    isList
                                    )
                                { }
                                else
                                {
                                    if (IsNew)
                                    {
                                        centralproperty.SetValue(centralEntity, localpropertyValue);
                                    }
                                    else
                                    {
                                        if (propertyName.Equals(primaryKey) || // specific properties to be excluded
                       propertyName.Contains("Created"))
                                        { }
                                        else
                                            centralproperty.SetValue(centralEntity, localpropertyValue);
                                    }
                                }
                            }
                            if (IsNew)
                            {
                                try
                                {
                                    centralDbSet.Add(centralEntity);
                                    centralContext.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    if (ex.InnerException == null)
                                        logs.ErrorLogs(LogPath, "AutoSyncUploadAdd", ex.Message);
                                    else
                                        logs.ErrorLogs(LogPath, "AutoSyncUploadAdd", ex.InnerException.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    centralContext.Entry(centralEntity).State = EntityState.Modified;
                                    centralContext.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    if (ex.InnerException == null)
                                        logs.ErrorLogs(LogPath, "AutoSyncUploadUpdate", ex.Message);
                                    else
                                        logs.ErrorLogs(LogPath, "AutoSyncUploadUpdate", ex.InnerException.Message);
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null)
                            logs.ErrorLogs(LogPath, "AutoSyncUpload", ex.Message);
                        else
                            logs.ErrorLogs(LogPath, "AutoSyncUpload", ex.InnerException.Message);
                    }
                    break;
            }
        }

        private dynamic GetEntityId(TEntity entity)
        {
            string primaryKey = GetPrimaryKey(entity);
            Type _type =
                entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().PropertyType;

            if (_type == typeof(Int32))
            {
                return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
            }

            if (_type == typeof(Int64))
            {
                return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
            }

            if (_type == typeof(Guid))
            {
                return new Guid(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
            }

            return null;
        }

        private string GetPrimaryKey(TEntity entity)
        {
            var key =
                entity.GetType()
                    .GetProperties()
                    .FirstOrDefault(x => x.GetCustomAttributes(typeof(KeyAttribute)).Any());

            return key.Name;
        }

    }

}
