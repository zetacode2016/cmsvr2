using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.SyncHelper;
using CMS2.Client.ViewModels;
using CMS2.Common.Constants;
using CMS2.Common.Identity;
using CMS2.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.Synchronization;

namespace CMS2.Client
{
    public partial class CMSMain : Form
    {
        private CmsBooking bookingForm;
        private CmsAcceptance acceptanceForm;
        private UcPayment paymentForm;
        private UcShipmentSummary shipmentSummaryForm;
        private UcPaymentSummary paymentSummaryForm;
        private UserManager<IdentityUser, Guid> _userManager;
        private FrmTracking trackingForm;
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewaySatService;
        private BranchCorpOfficeBL bcoService;
        private CmsDbCon dbConForm;
        //private AutoSync autoSync;
        private RevenueUnitBL revenueUnitService;

        //public CMSMain(UserManager<IdentityUser, Guid> userManager)
        public CMSMain()
        {
            InitializeComponent();
        }

        private void CMSMain_Load(object sender, EventArgs e)
        {
            GlobalVars.UnitOfWork = new CmsUoW();

            bookingForm = new CmsBooking();
            acceptanceForm = new CmsAcceptance();
            paymentForm = new UcPayment();
            shipmentSummaryForm = new UcShipmentSummary();
            paymentSummaryForm = new UcPaymentSummary();
            trackingForm = new FrmTracking();
            dbConForm = new CmsDbCon();

            areaService = new AreaBL(GlobalVars.UnitOfWork);
            bsoService = new BranchSatOfficeBL(GlobalVars.UnitOfWork);
            gatewaySatService = new GatewaySatOfficeBL(GlobalVars.UnitOfWork);
            bcoService = new BranchCorpOfficeBL(GlobalVars.UnitOfWork);
            revenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);

            trackingForm.TopLevel = false;
            bookingForm.TopLevel = false;
            acceptanceForm.TopLevel = false;
            dbConForm.TopLevel = true;

            panelMainContent.Controls.Add(bookingForm);
            panelMainContent.Controls.Add(acceptanceForm);
            panelMainContent.Controls.Add(paymentForm);
            panelMainContent.Controls.Add(shipmentSummaryForm);
            panelMainContent.Controls.Add(paymentSummaryForm);
            panelMainContent.Controls.Add(trackingForm);
            //panelMainContent.Controls.Add(dbConForm);

            GlobalVars.DeviceRevenueUnitId = Guid.Parse(ConfigurationSettings.AppSettings["RUId"]);
            GlobalVars.DeviceBcoId = Guid.Parse(ConfigurationSettings.AppSettings["BcoId"]);
            GlobalVars.UnitOfWork = new CmsUoW();
            if (GlobalVars.DeviceRevenueUnitId != Guid.Empty)
            {
                var temp = revenueUnitService.GetById(GlobalVars.DeviceRevenueUnitId);
                GlobalVars.DeviceBcoId = temp.City.BranchCorpOffice.BranchCorpOfficeId;
                GlobalVars.DeviceCityId = temp.City.CityId;
            }

            mnuSyncCentral.Enabled = false;
            mnuSyncCentral.Visible = false;
            if (GlobalVars.IsSubserver)
            {
                mnuSyncCentral.Enabled = true;
                mnuSyncCentral.Visible = true;
                GlobalVars.autoSync = new AutoSync();
                GlobalVars.autoSync.StartSync();
            }

            toolStripVersionTextBox.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void CMSMain_Shown(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            dbConForm.Hide();

            Login();
        }

        private void Login()
        {
            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                string username = loginForm.username;
                string password = loginForm.password;

                _userManager = new UserManager<IdentityUser, Guid>(new UserStore(GlobalVars.UnitOfWork));
                var user = _userManager.FindAsync(userName: username, password: password);
                if (user != null)
                {

                    EmployeeBL employeeService = new EmployeeBL();
                    EmployeePositionMappingBL employeePositionMappingService = new EmployeePositionMappingBL();
                    IdentityUser authenticatedUser = user.Result;

                    if (authenticatedUser != null)
                    {
                        var employee = employeeService.GetById(authenticatedUser.EmployeeId);
                        var roles = _userManager.GetRolesAsync(authenticatedUser.Id).Result.ToList();
                        AppUser.Principal = new GenericPrincipal(new GenericIdentity(authenticatedUser.UserName), roles.ToArray());
                        AppUser.User = authenticatedUser;
                        AppUser.Employee = employee;
                        AppUser.EmployeeAssignment =
                            employeePositionMappingService.FilterActiveBy(x => x.EmployeeId == employee.EmployeeId)
                                .FirstOrDefault();
                        if (AppUser.EmployeeAssignment != null)
                        {
                            switch (AppUser.EmployeeAssignment.LocationAssignment)
                            {
                                case AssignLocationConstant.Area:
                                    AppUser.EmployeeAssignment.AssignedLocation = areaService.GetById(AppUser.EmployeeAssignment.AssignedLocationId);
                                    break;
                                case AssignLocationConstant.BSO:
                                    AppUser.EmployeeAssignment.AssignedLocation = bsoService.GetById(AppUser.EmployeeAssignment.AssignedLocationId);
                                    break;
                                case AssignLocationConstant.GatewaySat:
                                    AppUser.EmployeeAssignment.AssignedLocation = gatewaySatService.GetById(AppUser.EmployeeAssignment.AssignedLocationId);
                                    break;
                                case AssignLocationConstant.BCO:
                                    AppUser.EmployeeAssignment.AssignedLocation = bcoService.GetById(AppUser.EmployeeAssignment.AssignedLocationId);
                                    break;
                            }
                        }
                        lblUserFullname.Text = "Welcome! " + AppUser.Employee.FullName;
                        btnLogOut.Enabled = true;
                    }
                    else
                    {
                        InvalidLogin();
                    }
                }
                else
                {
                    InvalidLogin();
                }
            }
            else
            {
                if (GlobalVars.IsSubserver)
                    GlobalVars.autoSync.StopSync();

                Application.Exit();
                //this.Close();
            }

            if (AppUser.Principal != null)
            {
                if (AppUser.Principal.IsInRole("Admin"))
                {
                    btnAppSetting.Enabled = true;
                }
            }

        }

        private void InvalidLogin()
        {
            if (MessageBox.Show("Invalid username and/or password. Try again?", "APCargo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Login();
            }
            else
            {
                this.Close();
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuSyncCentral_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();

            btnBooking.Enabled = false;
            btnAcceptance.Enabled = false;
            btnPayment.Enabled = false;
            btnShipmentSummary.Enabled = false;
            btnPaymentSummary.Enabled = false;

            SyncCentral syncCentral = new SyncCentral();
            syncCentral.ShowDialog(this.Owner);
            GlobalServices.GetData();

            btnBooking.Enabled = true;
            btnAcceptance.Enabled = true;
            btnPayment.Enabled = true;
            btnShipmentSummary.Enabled = true;
            btnPaymentSummary.Enabled = true;

            paymentSummaryForm.Hide();
            shipmentSummaryForm.Show();
            paymentForm.Show();
            acceptanceForm.Show();
            bookingForm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            lblUserFullname.Text = "";

            AppUser.Employee = null;
            AppUser.EmployeeAssignment = null;
            AppUser.Principal = null;
            AppUser.User = null;

            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();

            btnLogOut.Enabled = false;

            Login();
        }

        private NewPasswordViewModel GetNewPassword()
        {
            ChangePassword changePasswordForm = new ChangePassword();
            if (changePasswordForm.ShowDialog() == DialogResult.OK)
            {
                NewPasswordViewModel vm = new NewPasswordViewModel();
                vm.OldPassword = changePasswordForm.oldPassword;
                vm.NewPassword1 = changePasswordForm.newPassword1;
                vm.NewPassword2 = changePasswordForm.newPassword2;
                return vm;
            }
            else
            { return null; }
        }

        private async void lblUserFullname_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblUserFullname.Text))
            {
                var vm = GetNewPassword();
                if (vm != null)
                {
                    if (vm.NewPassword1.Equals(vm.NewPassword2))
                    {
                        try
                        {
                            _userManager = new UserManager<IdentityUser, Guid>(new UserStore(GlobalVars.UnitOfWork));
                            IdentityResult result = await _userManager.ChangePasswordAsync(AppUser.User.Id, vm.OldPassword, vm.NewPassword1);
                            if (result.Succeeded)
                            {
                                SyncCms sync = new SyncCms();
                                sync.OpenConnection();
                                sync.SyncEntity("User", SyncDirectionOrder.Upload);
                                sync.CloseConnection();
                                MessageBox.Show("Successfully changed password.", "Data Error", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Failed to change password.", "Data Error", MessageBoxButtons.OK);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("New password does not match.", "Data Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            bookingForm.Show();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            panelMainContent.AutoScroll = true;
            bookingForm.Width = panelMainContent.Width;
            bookingForm.Height = 690;
        }

        private void btnAcceptance_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Show();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            panelMainContent.AutoScroll = true;
            acceptanceForm.Width = panelMainContent.Width;
            acceptanceForm.Height = 690;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Show();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            panelMainContent.AutoScroll = true;
        }

        private void btnShipmentSummary_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Show();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            panelMainContent.AutoScroll = true;
        }

        private void btnPaymentSummary_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Show();
            trackingForm.Hide();
            panelMainContent.AutoScroll = true;
        }

        private void btnTracking_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Show();
            panelMainContent.AutoScroll = false;
            trackingForm.Width = panelMainContent.Width;
            trackingForm.Height = panelMainContent.Height;
        }

        private void btnAppSetting_Click(object sender, EventArgs e)
        {
            bookingForm.Hide();
            acceptanceForm.Hide();
            paymentForm.Hide();
            shipmentSummaryForm.Hide();
            paymentSummaryForm.Hide();
            trackingForm.Hide();
            //dbConForm.Show();
            panelMainContent.AutoScroll = true;
            dbConForm.Width = panelMainContent.Width;
            dbConForm.Height = 690;

            dbConForm.ShowDialog();
        }
    }
}
