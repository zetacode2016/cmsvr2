using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using CMS2.Common;

namespace CMS2.Client.SyncHelper
{
    public class AutoSync
    {
        Thread threadMainSync;
        private Thread threadDownloadTransaction;
        private Thread threadUploadTransaction;
        private Thread threadDownloadMaintenance;
        int intervalUploadTransaction = Convert.ToInt32(ConfigurationSettings.AppSettings["UpInter"]); // in minutes
        int intervalDownloadBooking = Convert.ToInt32(ConfigurationSettings.AppSettings["DownInter"]); // in minutes
        int intervalDownloadMaintenance = 60; // in minutes
        private bool isWorking;
        List<string> uploadTransaction = new List<string>();
        List<string> downloadTransaction = new List<string>();
        List<string> downloadMaintenace = new List<string>();
        private List<string> tracking;
        private List<string> transaction;
        private List<string> corporate;
        private List<string> rate;
        private List<string> employeeUser;
        private List<string> maintenance;
        SyncCms syncCms;

        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        public AutoSync()
        {
            SetEntitiesToSync();
            syncCms = new SyncCms();

            threadMainSync = new Thread(MainSync);
            threadMainSync.IsBackground = true;

            threadUploadTransaction = new Thread(UploadTransaction);
            threadUploadTransaction.IsBackground = true;

            threadDownloadTransaction = new Thread(DownLoadTransaction);
            threadDownloadTransaction.IsBackground = true;

            threadDownloadMaintenance = new Thread(DownLoadMaintenance);
            threadDownloadMaintenance.IsBackground = true;

        }

        public void MainSync()
        {
            Thread.Sleep(60 * 60000);
            threadDownloadTransaction.Start();
            Thread.Sleep(10 * 60000);
            threadUploadTransaction.Start();
            Thread.Sleep(1440 * 60000);
            threadDownloadMaintenance.Start();
        }

        public void StartSync()
        {
            try
            {
                if (!threadMainSync.IsAlive)
                {
                    threadMainSync.Start();
                    uploadTransaction.Clear();
                    uploadTransaction.AddRange(transaction);

                    downloadTransaction.Clear();
                    downloadTransaction.AddRange(transaction);

                    downloadMaintenace.Clear();
                    downloadMaintenace.AddRange(maintenance);
                    downloadMaintenace.AddRange(employeeUser);
                    downloadMaintenace.AddRange(rate);
                    downloadMaintenace.AddRange(corporate);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    logs.ErrorLogs(LogPath, "StartAutoSync", ex.Message);
                else
                    logs.ErrorLogs(LogPath, "StartAutoSync", ex.InnerException.Message);
            }
        }

        public void StopSync()
        {
            try
            {
                if (threadMainSync.IsAlive)
                {
                    threadUploadTransaction.Abort();
                    threadDownloadTransaction.Abort();
                    threadDownloadMaintenance.Abort();
                    threadMainSync.Abort();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    logs.ErrorLogs(LogPath, "StopAutoSync", ex.Message);
                else
                    logs.ErrorLogs(LogPath, "StopAutoSync", ex.InnerException.Message);
            }
            
        }

        private void DownLoadTransaction()
        {
            while (isWorking)
            {
                foreach (var item in downloadTransaction)
                {
                    syncCms.SyncEntity(item, "Download");
                }
                Thread.Sleep(intervalDownloadBooking*(60000)); // Sleep is in milliseconds
            }
        }

        private void DownLoadMaintenance()
        {
            while (isWorking)
            {
                foreach (var item in downloadMaintenace)
                {
                    syncCms.SyncEntity(item, "Download");
                }
                Thread.Sleep(intervalDownloadMaintenance * (60000)); // Sleep is in milliseconds
            }
        }

        private void UploadTransaction()
        {
            while (isWorking)
            {
                foreach (var item in uploadTransaction)
                {
                    syncCms.SyncEntity(item, "Upload");
                }
                Thread.Sleep(intervalUploadTransaction * (60000)); // Sleep is in milliseconds
            }
        }

        private void SetEntitiesToSync()
        {
            transaction = new List<string>() { "Company", "Client", "StatementOfAccount", "Booking", "Shipment", "PackageDimension", "PackageNumber", "StatementOfAccountPayment", "Payment", "PaymentTurnover", "ShipmentAdjustment", "Delivery", "DeliveredPackage", "DeliveryReceipt" };

            corporate = new List<string>() { "Company", "Client", "StatementOfAccount" };

            rate = new List<string>() { "TransShipmentRoute", "TransShipmentLeg", "Crating", "Packaging", "ShipMode", "ApplicableRate", "ServiceType", "ServiceMode", "CommodityType", "ShipmentBasicFee", "FuelSurcharge", "RateMatrix", "ExpressRate", "Commodity" };

            employeeUser = new List<string>() { "Department", "Position", "Employee", "EmployeePositionMapping", "Role", "User", "RoleUser" };

            maintenance = new List<string>() { "Group", "Region", "BranchCorpOffice", "Cluster", "City", "RevenueUnitType", "RevenueUnit", "AccountStatus", "AccountType", "AdjustmentReason", "ApplicationSetting", "BookingRemark", "BookingStatus", "BusinessType", "Industry", "OrganizationType", "BillingPeriod", "PaymentMode", "PaymentTerm", "PaymentType", "GatewayType", "Gateway", "GoodsDescription","DeliveryStatus", "DeliveryRemark", "Truck", "TruckAreaMapping" };

            tracking = new List<string>() { "users", "gatewayacceptance", "gatewaytransmittal", "bundle", "inbound", "distribution2", "holdcargo", "transfer", "status", "branchacceptance" };
        }
    }
}
