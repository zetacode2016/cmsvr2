using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CMS2.DataAccess;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace CMS2.Client.SyncHelper
{
    public class SyncTracking
    {
        ConnectionStringSettings centralSettings = ConfigurationManager.ConnectionStrings["TrackingCentral"];
        ConnectionStringSettings clientSettings = ConfigurationManager.ConnectionStrings["Tracking"];

        private SqlConnection serverConn;
        private SqlConnection clientConn;

        public SyncTracking()
        {
            serverConn = new SqlConnection(centralSettings.ConnectionString);
            clientConn = new SqlConnection(clientSettings.ConnectionString);
        }

        public bool SyncEntity(string entityName, SyncDirectionOrder syncDirection)
        {
            try
            {
                DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(entityName);

                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(entityName, clientConn);
                scopeDesc.Tables.Add(tableDesc);

                SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
                serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
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
                MessageBox.Show("Sync Error: " + ex.Message);
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
    }
}
