using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CMS2.Client.SyncHelper;

namespace CMS2.Client
{
    public partial class SyncCentral : Form
    {
        List<string> entities = new List<string>();
        private SyncCms syncCms;
        private SyncTracking syncTracking;
        private bool DoSyncTracking;
        private List<string> tracking;
        private string syncDirection;

        public SyncCentral()
        {
            InitializeComponent();
        }

        private void SyncCentral_Load(object sender, EventArgs e)
        {
            syncCms = new SyncCms();
            rdoDownload.Checked = true;
            DoSyncTracking = false;
            // TODO display central server connection
        }

        private void SetEntitiesToSync()
        {
            List<string> shipment = new List<string>() { "Company", "Client", "StatementOfAccount", "Booking", "Shipment", "PackageDimension", "PackageNumber", "StatementOfAccountPayment", "Payment", "PaymentTurnover", "ShipmentAdjustment", "Delivery", "DeliveredPackage", "DeliveryReceipt" };

            List<string> corporate = new List<string>() { "Company", "Client", "StatementOfAccount"};

            List<string> rate = new List<string>() {"TransShipmentRoute","TransShipmentLeg","Crating", "Packaging", "ShipMode", "ApplicableRate", "ServiceType", "ServiceMode", "CommodityType", "ShipmentBasicFee", "FuelSurcharge", "RateMatrix", "ExpressRate", "Commodity" };

            List<string> employeeUser = new List<string>() { "Department", "Position", "Employee", "EmployeePositionMapping", "Role", "User", "RoleUser" };

            List<string> maintenance = new List<string>() { "Group", "Region","BranchCorpOffice", "Cluster", "City", "RevenueUnitType", "RevenueUnit", "AccountStatus", "AccountType", "AdjustmentReason", "ApplicationSetting", "BookingRemark", "BookingStatus", "BusinessType", "Industry", "OrganizationType", "BillingPeriod", "PaymentMode", "PaymentTerm", "PaymentType", "GatewayType","Gateway", "GoodsDescription", "DeliveryStatus", "DeliveryRemark", "Truck", "TruckAreaMapping" };

            tracking = new List<string>() {"users", "gatewayacceptance", "gatewaytransmittal", "bundle","inbound","distribution2","holdcargo","transfer","status","branchacceptance"};

            entities.Clear();


            if (chkMaintenance.Checked)
            { entities.AddRange(maintenance); }

            if (chkUserAccount.Checked)
            { entities.AddRange(employeeUser); }

            if (chkRate.Checked)
            { entities.AddRange(rate); }

            if (chkClient.Checked)
            { entities.AddRange(corporate); } 
            
            if (chkTransaction.Checked)
            { entities.AddRange(shipment); }

            if (chkTracking.Checked)
            {
                DoSyncTracking = true;
            }
        }

        private void ClearSelection()
        {
            chkTransaction.Checked = false;
            chkClient.Checked = false;
            chkRate.Checked = false;
            chkUserAccount.Checked = false;
            chkMaintenance.Checked = false;
        }

        private void rdoUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUpload.Checked)
            {
                chkMaintenance.Checked = false;
                chkRate.Checked = false;
                chkMaintenance.Enabled = false;
                chkRate.Enabled = false;
                syncDirection = "Upload";
            }
            else
            {
                chkMaintenance.Enabled = true;
                chkRate.Enabled = true;
            }
        }

        private void rdoDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDownload.Checked)
            {
                syncDirection = "Download";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GlobalVars.autoSync.StopSync();
            SetEntitiesToSync();
            DisableSelections();
            btnStart.Enabled = false;
            btnClose.Enabled = true;
            ProgressIndicator process;
            if (chkDeprovision.Checked)
            {
                process = new ProgressIndicator("Synchronizing", "Resetting DB ...", Deprovision);
                process.ShowDialog();
            }
            process = new ProgressIndicator("Synchronizing", syncDirection.ToString() + "ing ...", Synchronize);
            process.ShowDialog();
            SynchronizeCompleted();
            GlobalVars.autoSync.StartSync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Synchronize(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;

            int percent = 1;
            int index = 1;
            int max = entities.Count +1;
            if (DoSyncTracking)
            {
                max = max + 10; 
            }

            #region SyncCms
            
            //syncCms.SyncEntity("User", "Upload");
            if (entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    if (_worker.CancellationPending)
                    {
                        break;
                    }
                    syncCms.SyncEntity(item, syncDirection);

                    percent = index * 100 / max;
                    _worker.ReportProgress(percent);
                    index++;
                }
                syncCms.CloseConnection();
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
           
            #endregion

            #region SyncTracking
            if (DoSyncTracking)
            {
                syncTracking.OpenConnection();
                if (syncTracking.IsCentralConnected())
                {
                    for (int x = 1; x < tracking.Count; x++)
                    {
                        if (_worker.CancellationPending)
                        {
                            break;
                        }

                        //syncTracking.SyncEntity(tracking[x], syncDirection);

                        percent = index * 100 / max;
                        _worker.ReportProgress(percent);
                        index++;
                    }
                }
                syncTracking.CloseConnection();
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
            #endregion
        }

        private void Deprovision(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;

            int percent = 1;
            int index = 1;
            int max = entities.Count + 1;
            if (DoSyncTracking)
            {
                max = max + 11;
                entities.Add(tracking[0]);
            }

            #region SyncCms
            if (entities.Count > 0)
            {
                syncCms.OpenConnection();
                if (syncCms.IsCentralConnected())
                {
                    foreach (var item in entities)
                    {
                        if (_worker.CancellationPending)
                        {
                            break;
                        }

                        syncCms.DeProvision(item);

                        percent = index * 100 / max;
                        _worker.ReportProgress(percent);
                        index++;
                    }
                }
                syncCms.CloseConnection();
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
            #endregion

            #region SyncTracking
            if (DoSyncTracking)
            {
                syncTracking.OpenConnection();
                if (syncTracking.IsCentralConnected())
                {
                    for (int x = 1; x < tracking.Count; x++)
                    {
                        if (_worker.CancellationPending)
                        {
                            break;
                        }

                        syncTracking.DeProvision(tracking[x]);

                        percent = index * 100 / max;
                        _worker.ReportProgress(percent);
                        index++;
                    }
                }
                syncTracking.CloseConnection();
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
            #endregion
        }

        private void SynchronizeCompleted()
        {
            ClearSelection();
            EnableSelections();
            rdoDownload.Checked = true;
            btnStart.Enabled = true;
            btnClose.Enabled = true;
            DoSyncTracking = false;
        }

        private void DisableSelections()
        {
            rdoDownload.Enabled = false;
            rdoUpload.Enabled = false;
            chkTransaction.Enabled = false;
            chkClient.Enabled = false;
            chkRate.Enabled = false;
            chkUserAccount.Enabled = false;
            chkMaintenance.Enabled = false;
        }

        private void EnableSelections()
        {
            rdoDownload.Enabled = true;
            rdoUpload.Enabled = true;
            chkTransaction.Enabled = true;
            chkClient.Enabled = true;
            chkRate.Enabled = true;
            chkUserAccount.Enabled = true;
            chkMaintenance.Enabled = true;
        }
    }
}
