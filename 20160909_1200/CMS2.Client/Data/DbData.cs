using System;
using System.ComponentModel;
using System.Configuration;
using CMS2.DataAccess;
using Microsoft.Synchronization;

namespace CMS2.Client.Data
{
    public class DbData
    {
        private bool success = false;
        string insertString = "";
        CmsContext context = new CmsContext();
        SyncHelper.SyncCms sync = new SyncHelper.SyncCms();
        private string latestDbDate = "";

        public void InitialData(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 26; // # of processes

            //1
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            //25
            #region InitalData
            InitialData_A();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_B();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_C();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_D();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_E();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_F();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_G();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_H();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_I();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_J();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_K();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_L();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_M();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_N();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_O();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_P();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_Q();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_R();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_S();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_T();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_U();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_V();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_W();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_X();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            InitialData_Y();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            #endregion

        }

        public void UpdateDatabase(object sender, DoWorkEventArgs e)
        {
            //string[] latestDbDate = ConfigurationSettings.AppSettings["LatestDbDate"].Split('/');
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            //1
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            //1
            #region StructureChange
            //if (DateTime.Now >
            //    new DateTime(Convert.ToInt32(latestDbDate[2]), Convert.ToInt32(latestDbDate[1]),
            //        Convert.ToInt32(latestDbDate[0])))
            //{
            //    percent = index * 100 / max;
            //    _worker.ReportProgress(percent);
            //    index++;
            //}
            Upd20160712A();
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            
            
            #endregion
        }

        #region InitialData
        private void InitialData_A()
        {
            sync.SyncEntity("Group", SyncDirectionOrder.Download);
        }

        private void InitialData_B()
        {
            sync.SyncEntity("Region", SyncDirectionOrder.Download);
        }

        private void InitialData_C()
        {
            sync.SyncEntity("BranchCorpOffice", SyncDirectionOrder.Download);
        }
        private void InitialData_D()
        {
            sync.SyncEntity("Cluster", SyncDirectionOrder.Download);
        }
        private void InitialData_E()
        {
            sync.SyncEntity("City", SyncDirectionOrder.Download);
        }
        private void InitialData_F()
        {
            sync.SyncEntity("City", SyncDirectionOrder.Download);
        }
        private void InitialData_G()
        {
            sync.SyncEntity("RevenueUnitType", SyncDirectionOrder.Download);
        }
        private void InitialData_H()
        {
            sync.SyncEntity("RevenueUnit", SyncDirectionOrder.Download);
        }
        private void InitialData_I()
        {
            sync.SyncEntity("Employee", SyncDirectionOrder.Download);
        }
        private void InitialData_J()
        {
            sync.SyncEntity("Position", SyncDirectionOrder.Download);
        }
        private void InitialData_K()
        {
            sync.SyncEntity("Department", SyncDirectionOrder.Download);
        }
        private void InitialData_L()
        {
            sync.SyncEntity("EmployeePositionMapping", SyncDirectionOrder.Download);
        }
        private void InitialData_M()
        {
            sync.SyncEntity("User", SyncDirectionOrder.Download);
        }
        private void InitialData_N()
        {
            sync.SyncEntity("Role", SyncDirectionOrder.Download);
        }
        private void InitialData_O()
        {
            sync.SyncEntity("RoleUser", SyncDirectionOrder.Download);
        }
        private void InitialData_P()
        {
            sync.SyncEntity("Commodity", SyncDirectionOrder.Download);
        }
        private void InitialData_Q()
        {
            sync.SyncEntity("ExpressRate", SyncDirectionOrder.Download);
        }
        private void InitialData_R()
        {
            sync.SyncEntity("FuelSurcharge", SyncDirectionOrder.Download);
        }
        private void InitialData_S()
        {
            sync.SyncEntity("ShipmentBasicFee", SyncDirectionOrder.Download);
        }
        private void InitialData_T()
        {
            sync.SyncEntity("BookingRemark", SyncDirectionOrder.Download);
        }
        private void InitialData_U()
        {
            sync.SyncEntity("BookingStatus", SyncDirectionOrder.Download);
        }
        private void InitialData_V()
        {
            sync.SyncEntity("PaymentTerm", SyncDirectionOrder.Download);
        }
        private void InitialData_W()
        {
            sync.SyncEntity("ServiceMode", SyncDirectionOrder.Download);
        }
        private void InitialData_X()
        {
            sync.SyncEntity("PaymentMode", SyncDirectionOrder.Download);
        }
        private void InitialData_Y()
        {
            sync.SyncEntity("DeliveryStatus", SyncDirectionOrder.Download);
        }
        private void InitialData_Z()
        {
            sync.SyncEntity("", SyncDirectionOrder.Download);
        }
        #endregion

        #region StructureChange

        private void Upd20160712A()
        {
            try
            {
                insertString = "alter table Client alter column Street nvarchar(250) null";
                context.Database.ExecuteSqlCommand(insertString);

                insertString = "alter table Booking alter column OriginStreet nvarchar(250) null";
                context.Database.ExecuteSqlCommand(insertString);

                insertString = "alter table Booking alter column DestinationStreet nvarchar(250) null";
                context.Database.ExecuteSqlCommand(insertString);
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Alter Column: " + ex.Message);
            }
        }

        #endregion

    }
}


//#region 
//public void UpdateDb20160315
//{
//           try
//           {
//               insertString = "";
//               if (context.Database.ExecuteSqlCommand(insertString) > 0)
//               {
//                   insertString = "";
//                   context.Database.ExecuteSqlCommand(insertString);

//               }
//           }
//           catch (Exception ex)
//           {
//               //MessageBox.Show("Alter Column: " + ex.Message);
//           }
//}
//           #endregion


//try
//            {
//                // Add column ShipmentId in PackageNumber
//                insertString =
//                    "IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'ShipmentId' AND OBJECT_ID = OBJECT_ID(N'PackageNumber')) " +
//                    "alter table PackageNumber add CONSTRAINT [PackageNumber_Shipment_ShipmentId] FOREIGN KEY (ShipmentId) REFERENCES Shipment (ShipmentId) ON UPDATE NO ACTION ON DELETE No action " +
//                    " if not exists(select * from sys.indexes where name='IX_ShipmentId' and object_id = OBJECT_ID('PackageNumber') " +
//                    " CREATE NONCLUSTERED INDEX IX_ShipmentId ON PackageNumber (ShipmentId) WITH (PAD_INDEX = OFF,DROP_EXISTING = OFF,STATISTICS_NORECOMPUTE = OFF,SORT_IN_TEMPDB = OFF,ONLINE = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
//                    "update PackageNumber set ShipmentId=null"; //" ALTER TABLE PackageNumber alter column ShipmentId uniqueidentifier not null";
//                result = result + context.Database.ExecuteSqlCommand(insertString);
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show("Alter Column: " + ex.Message);
//            }