using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Common.Constants;
using CMS2.Common.Enums;
using CMS2.Entities;
using CMS2.Entities.Models;

namespace CMS2.Client
{
    public partial class CmsBooking : Form
    {
        Booking booking;
        Entities.Client shipper;
        Entities.Client consignee;
        private AutoCompleteStringCollection shipperLastNames;
        private AutoCompleteStringCollection shipperFirstNames;
        private AutoCompleteStringCollection shipperCompany;
        private AutoCompleteStringCollection clientConsigneeLastNames;
        private AutoCompleteStringCollection clientConsigneeFirstNames;
        AutoCompleteStringCollection consigneeCompany;
        private AutoCompleteStringCollection shipperBco;
        private AutoCompleteStringCollection shipperCity;
        private AutoCompleteStringCollection consgineeBco;
        private AutoCompleteStringCollection consgineeCity;
        private AutoCompleteStringCollection assignedTo;
        private BindingSource bsBookingStatus;
        private BindingSource bsBookingRemark;
        private BindingSource bsAreas;
        private BindingSource bsOriginBco;
        private BindingSource bsDestinationBco;

        private BookingStatusBL bookingStatusService;
        private BookingRemarkBL bookingRemarkService;
        private BranchCorpOfficeBL bcoService;
        private AreaBL areaService;
        private BookingBL bookingService;
        private ClientBL clientService;
        private RevenueUnitBL revenueUnitService;
        private CityBL cityService;
        private CompanyBL companyService;

        private List<BookingStatus> bookingStatus;
        private List<BookingRemark> bookingRemarks;
        private List<BranchCorpOffice> branchCorpOffices;
        private List<RevenueUnit> areas;
        private List<Entities.Client> clients;
        private List<RevenueUnit> revenueUnits;
        private List<City> cities;
        private List<Company> companies;

        public CmsBooking()
        {
            InitializeComponent();
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            bsBookingStatus = new BindingSource();
            bsBookingRemark = new BindingSource();
            bsAreas = new BindingSource();
            bsOriginBco = new BindingSource();
            bsDestinationBco = new BindingSource();

            bookingStatus = new List<BookingStatus>();
            bookingRemarks = new List<BookingRemark>();
            branchCorpOffices = new List<BranchCorpOffice>();
            areas = new List<RevenueUnit>();
            clients = new List<Entities.Client>();
            revenueUnits = new List<RevenueUnit>();
            cities = new List<City>();
            companies = new List<Company>();

            bookingStatusService = new BookingStatusBL(GlobalVars.UnitOfWork);
            bookingRemarkService = new BookingRemarkBL(GlobalVars.UnitOfWork);
            bcoService = new BranchCorpOfficeBL(GlobalVars.UnitOfWork);
            areaService = new AreaBL(GlobalVars.UnitOfWork);
            bookingService = new BookingBL(GlobalVars.UnitOfWork);
            clientService = new ClientBL(GlobalVars.UnitOfWork);
            revenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);
            cityService = new CityBL(GlobalVars.UnitOfWork);
            companyService = new CompanyBL(GlobalVars.UnitOfWork);

            bookingStatus = bookingStatusService.FilterActive().OrderBy(x => x.BookingStatusName).ToList();
            bookingRemarks = bookingRemarkService.FilterActive().OrderBy(x => x.BookingRemarkName).ToList();
            branchCorpOffices = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
            clients = clientService.FilterActive();
            revenueUnits = revenueUnitService.FilterActive();
            cities = cityService.FilterActive().OrderBy(x => x.CityName).ToList();
            companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();

            bsBookingStatus.DataSource = bookingStatus;
            bsBookingRemark.DataSource = bookingRemarks;
            bsAreas.DataSource = areas;
            bsOriginBco.DataSource = branchCorpOffices;
            bsDestinationBco.DataSource = branchCorpOffices;

            lstBookingStatus.DataSource = bsBookingStatus;
            lstBookingStatus.ValueMember = "BookingStatusId";
            lstBookingStatus.DisplayMember = "BookingStatusName";
            lstBookingStatus.SelectedIndex = -1;

            lstBookingRemarks.DataSource = bsBookingRemark;
            lstBookingRemarks.DisplayMember = "BookingRemarkName";
            lstBookingRemarks.ValueMember = "BookingRemarkId";
            lstBookingRemarks.SelectedIndex = -1;

            lstAssignedTo.DataSource = bsAreas;
            lstAssignedTo.DisplayMember = "RevenueUnitName";
            lstAssignedTo.ValueMember = "RevenueUnitId";

            lstOriginBco.DataSource = bsOriginBco;
            lstOriginBco.DisplayMember = "BranchCorpOfficeName";
            lstOriginBco.ValueMember = "BranchCorpOfficeId";
            lstOriginBco.SelectedIndex = -1;

            lstDestinationBco.DataSource = bsDestinationBco;
            lstDestinationBco.DisplayMember = "BranchCorpOfficeName";
            lstDestinationBco.ValueMember = "BranchCorpOfficeId";
            lstDestinationBco.SelectedIndex = -1;

            AddDailyBooking();
        }

        private void CmsBooking_Shown(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;

            bsBookingStatus.ResetBindings(false);
            bsBookingStatus.ResetBindings(false);
            bsBookingRemark.ResetBindings(false);
            bsAreas.ResetBindings(false);
            bsOriginBco.ResetBindings(false);
            bsDestinationBco.ResetBindings(false);

            PopulateGrid();

            var _areas = areas.Where(x => x.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();
            lstAssignedTo.DataSource = _areas;
            lstAssignedTo.DisplayMember = "RevenueUnitName";
            lstAssignedTo.ValueMember = "RevenueUnitId";
        }

        private void Booking_Enter(object sender, EventArgs e)
        {
            ResetAll();
            PopulateGrid();
        }

        private void NewShipment()
        {
            ShipmentModel newShipment = new ShipmentModel();
            newShipment.ShipperId = booking.ShipperId;
            newShipment.Shipper = booking.Shipper;
            newShipment.OriginCityId = booking.OriginCityId;
            newShipment.OriginCity = booking.OriginCity;
            newShipment.OriginAddress = booking.OriginAddress1;
            if (!String.IsNullOrEmpty(booking.OriginAddress2))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginAddress2;
            if (!String.IsNullOrEmpty(booking.OriginStreet))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginStreet;
            newShipment.OriginBarangay = booking.OriginBarangay;
            if (!String.IsNullOrEmpty(booking.OriginBarangay))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginBarangay;
            newShipment.ConsigneeId = booking.ConsigneeId;
            newShipment.Consignee = booking.Consignee;
            newShipment.DestinationCityId = booking.DestinationCityId;
            newShipment.DestinationCity = booking.DestinationCity;
            newShipment.DestinationAddress = booking.DestinationAddress1;
            if (!String.IsNullOrEmpty(booking.DestinationAddress2))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationAddress2;
            if (!String.IsNullOrEmpty(booking.DestinationStreet))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationStreet;
            newShipment.DestinationBarangay = booking.DestinationBarangay;
            if (!String.IsNullOrEmpty(booking.DestinationBarangay))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationBarangay;

            newShipment.BookingId = booking.BookingId;
            newShipment.Booking = booking;

            this.Hide();
            Panel mainPanel = (Panel)this.ParentForm.Controls.Find("panelMainContent", false)[0];
            CmsAcceptance acceptanceTab = (CmsAcceptance)mainPanel.Controls.Find("CmsAcceptance", false)[0];
            acceptanceTab.shipmentModel = newShipment;
            acceptanceTab.Show();

            ResetAll();
        }

        private void AddDailyBooking()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Now.AddDays(-1);
            List<Booking> todayBooking = bookingService.FilterActiveBy(x => x.DateBooked.Year == today.Year && x.DateBooked.Month == today.Month && x.DateBooked.Day == today.Day).ToList();
            List<Booking> yesterdayBooking = bookingService.FilterActiveBy(x => x.HasDailyBooking == true && x.DateBooked.Year == yesterday.Year && x.DateBooked.Month == yesterday.Month && x.DateBooked.Day == yesterday.Day).ToList();
            foreach (var item in yesterdayBooking)
            {
                if (!todayBooking.Exists(x => x.PreviousBookingId == item.BookingId))
                {
                    Booking newBooking = new Booking();
                    newBooking.BookingId = Guid.NewGuid();
                    newBooking.BookingNo = GetBookingNumber();
                    newBooking.DateBooked = DateTime.Now;
                    newBooking.ShipperId = item.ShipperId;
                    newBooking.OriginAddress1 = item.OriginAddress1;
                    newBooking.OriginAddress2 = item.OriginAddress2;
                    newBooking.OriginBarangay = item.OriginBarangay;
                    newBooking.OriginCityId = item.OriginCityId;
                    newBooking.ConsigneeId = item.ConsigneeId;
                    newBooking.DestinationAddress1 = item.DestinationAddress1;
                    newBooking.DestinationAddress2 = item.DestinationAddress2;
                    newBooking.DestinationBarangay = item.DestinationBarangay;
                    newBooking.DestinationCityId = item.DestinationCityId;
                    newBooking.Remarks = item.Remarks;
                    newBooking.HasDailyBooking = item.HasDailyBooking;
                    newBooking.BookedById = item.BookedById;
                    newBooking.AssignedToAreaId = item.AssignedToAreaId;
                    newBooking.BookingStatusId =
                        bookingStatus.Where(x => x.BookingStatusName.Equals("Pending"))
                            .First()
                            .BookingStatusId;
                    newBooking.BookingRemarkId =
                        bookingRemarks.Where(x => x.BookingRemarkName.Equals("Lack of Time"))
                            .First()
                            .BookingRemarkId;
                    newBooking.CreatedBy = item.CreatedBy;
                    newBooking.CreatedDate = DateTime.Now;
                    newBooking.ModifiedBy = item.CreatedBy;
                    newBooking.ModifiedDate = DateTime.Now;
                    newBooking.RecordStatus = (int)RecordStatus.Active;
                    newBooking.PreviousBookingId = item.BookingId;
                    bookingService.AddEdit(newBooking);
                }
            }
        }

        private void PopulateGrid(List<Booking> bookings = null)
        {
            List<Booking> _bookings;
            if (bookings == null)
            {
                _bookings = bookingService.FilterActiveBy(x => !x.BookingStatus.BookingStatusName.Equals("Picked-up"))
                        .OrderBy(x => x.DateBooked)
                        .ToList();
            }
            else
            {
                _bookings = bookings;
            }

            gridBookings.Rows.Clear();
            int index = 0;
            int selectedIndex = 0;
            foreach (var _booking in _bookings)
            {
                if (booking!=null)
                {
                    if (_booking.BookingId == booking.BookingId)
                    {
                        selectedIndex = index;
                    }
                }
                gridBookings.Rows.Add();
                gridBookings.Rows[index].Cells["colBookingId"].Value = _booking.BookingId.ToString();
                gridBookings.Rows[index].Cells["colBookingDate"].Value = _booking.DateBooked.ToString("MMM dd, yyyy");
                gridBookings.Rows[index].Cells["colBookingTime"].Value = _booking.DateBooked.ToString("hh:mm tt");
                gridBookings.Rows[index].Cells["colBookingNo"].Value = _booking.BookingNo;
                gridBookings.Rows[index].Cells["colShipperName"].Value = _booking.Shipper.FullName;
                gridBookings.Rows[index].Cells["colOriginCity"].Value = _booking.OriginCity.CityName;
                gridBookings.Rows[index].Cells["colConsigneeName"].Value = _booking.Consignee.FullName;
                gridBookings.Rows[index].Cells["colDestinationCity"].Value = _booking.DestinationCity.CityName;
                gridBookings.Rows[index].Cells["colBookedBy"].Value = _booking.BookedBy.FullName;
                gridBookings.Rows[index].Cells["colBookingStatus"].Value = _booking.BookingStatus.BookingStatusName;
                index++;
            }
            if (booking != null)
                gridBookings.Rows[selectedIndex].Selected = true;

        }

        private void gridBookings_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Guid rowId = Guid.Parse(gridBookings.Rows[e.RowIndex].Cells["colBookingId"].Value.ToString());
            gridBookings.Rows[e.RowIndex].Selected = true;
            BookingSelected(rowId);
        }

        private void gridBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Guid rowId = Guid.Parse(gridBookings.Rows[e.RowIndex].Cells["colBookingId"].Value.ToString());
            gridBookings.Rows[e.RowIndex].Selected = true;
            BookingSelected(rowId);
        }

        private void gridBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Guid rowId = Guid.Parse(gridBookings.Rows[e.RowIndex].Cells["colBookingId"].Value.ToString());
            gridBookings.Rows[e.RowIndex].Selected = true;
            BookingSelected(rowId);
            NewShipment();
        }

        private void BookingSelected(Guid id)
        {
            booking = bookingService.FilterActiveBy(x => x.BookingId == id).FirstOrDefault();

            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            btnAcceptance.Enabled = true;
            btnDelete.Enabled = true;

            PopulateForm();
        }

        private void PopulateForm()
        {
            if (booking != null)
            {
                txtShipperAccountNo.Text = booking.Shipper.AccountNo;
                txtShipperLastName.Text = booking.Shipper.LastName;
                txtShipperFirstName.Text = booking.Shipper.FirstName;
                if (booking.Shipper.CompanyId != null)
                {
                    txtShipperCompany.Text = booking.Shipper.Company.CompanyName + " - " + booking.Shipper.Company.AccountNo;
                }
                else
                {
                    txtShipperCompany.Text = booking.Shipper.CompanyName;
                }
                txtShipperAddress1.Text = booking.OriginAddress1;
                txtShipperAddress2.Text = booking.OriginAddress2;
                txtShipperStreet.Text = booking.OriginStreet;
                txtShipperBarangay.Text = booking.OriginBarangay;
                var bcoId = cities.FirstOrDefault(x => x.CityId == booking.OriginCityId).BranchCorpOffice.BranchCorpOfficeId;
                SelectedOriginCity(bcoId);
                lstOriginBco.SelectedValue = bcoId;
                lstOriginCity.SelectedValue = booking.OriginCityId;
                txtShipperContactNo.Text = booking.Shipper.ContactNo;
                txtShipperMobile.Text = booking.Shipper.Mobile;
                txtShipperEmail.Text = booking.Shipper.Email;

                txtConsigneeAccountNo.Text = booking.Consignee.AccountNo;
                txtConsigneeLastName.Text = booking.Consignee.LastName;
                txtConsigneeFirstName.Text = booking.Consignee.FirstName;
                if (booking.Consignee.CompanyId != null)
                {
                    txtConsigneeCompany.Text = booking.Consignee.Company.CompanyName + " - " + booking.Consignee.Company.AccountNo;
                }
                else
                {
                    txtConsigneeCompany.Text = booking.Consignee.CompanyName;
                }
                txtConsigneeAddress1.Text = booking.DestinationAddress1;
                txtConsigneeAddress2.Text = booking.DestinationAddress2;
                txtConsgineeStreet.Text = booking.DestinationStreet;
                txtConsigneeBarangay.Text = booking.DestinationBarangay;
                bcoId = cities.FirstOrDefault(x => x.CityId == booking.DestinationCityId).BranchCorpOffice.BranchCorpOfficeId;
                SelectedDestinationCity(bcoId);
                lstDestinationBco.SelectedValue = bcoId;
                lstDestinationCity.SelectedValue = booking.DestinationCityId;
                txtConsigneeContactNo.Text = booking.Consignee.ContactNo;
                txtConsigneeMobile.Text = booking.Consignee.Mobile;
                txtConsigneeEmail.Text = booking.Consignee.Email;

                dateDateBooked.Value = booking.DateBooked;
                txtBookedBy.Text = booking.BookedBy.FullName;
                txtRemarks.Text = booking.Remarks;
                txtBookingNo.Text = booking.BookingNo;
                chkHasDailyBooking.Checked = booking.HasDailyBooking;
                lstAssignedTo.SelectedValue = booking.AssignedToAreaId;
                lstBookingStatus.SelectedValue = booking.BookingStatusId;
                lstBookingRemarks.SelectedIndex = -1;
                if (booking.BookingRemarkId != null)
                {
                    lstBookingRemarks.SelectedValue = booking.BookingRemarkId;
                }

                shipper = clientService.GetById(booking.ShipperId);
                consignee = clientService.GetById(booking.ConsigneeId);

                if (!booking.BookingStatus.BookingStatusName.Equals("Picked-up"))
                {
                    txtShipperLastName.Enabled = true;
                    txtShipperFirstName.Enabled = true;
                    txtShipperCompany.Enabled = true;
                    txtShipperAddress1.Enabled = true;
                    txtShipperAddress2.Enabled = true;
                    txtShipperStreet.Enabled = true;
                    txtShipperBarangay.Enabled = true;
                    lstOriginCity.Enabled = true;
                    txtShipperContactNo.Enabled = true;
                    txtShipperMobile.Enabled = true;
                    txtShipperEmail.Enabled = true;

                    txtConsigneeLastName.Enabled = true;
                    txtConsigneeFirstName.Enabled = true;
                    txtConsigneeCompany.Enabled = true;
                    txtConsigneeAddress1.Enabled = true;
                    txtConsigneeAddress2.Enabled = true;
                    txtConsgineeStreet.Enabled = true;
                    txtConsigneeBarangay.Enabled = true;
                    lstDestinationCity.Enabled = true;
                    txtConsigneeContactNo.Enabled = true;
                    txtConsigneeMobile.Enabled = true;
                    txtConsigneeEmail.Enabled = true;

                    txtRemarks.Enabled = true;
                    dateDateBooked.Enabled = true;
                    chkHasDailyBooking.Enabled = true;
                    txtBookedBy.Enabled = true;
                    txtBookingNo.Enabled = true;
                    lstAssignedTo.Enabled = true;
                    lstBookingStatus.Enabled = true;
                    lstBookingRemarks.Enabled = true;

                    btnNew.Enabled = true;
                    btnSave.Enabled = true;
                    btnReset.Enabled = true;
                    btnAcceptance.Enabled = true;
                    btnDelete.Enabled = true;
                    lstOriginBco.Enabled = true;
                    lstDestinationBco.Enabled = true;
                }
                else
                {
                    btnNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnReset.Enabled = false;
                    btnAcceptance.Enabled = true;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void NewBooking()
        {
            if (AppUser.Principal.Identity.IsAuthenticated)
            {
                string bookingNo = GetBookingNumber();
                txtBookingNo.Text = bookingNo;
                booking = new Booking();
                booking.BookingNo = bookingNo;
                booking.BookedById = AppUser.User.EmployeeId;

                txtShipperAccountNo.Text = "";
                txtShipperLastName.Text = "";
                txtShipperFirstName.Text = "";
                txtShipperCompany.Text = "";
                txtShipperAddress1.Text = "";
                txtShipperAddress2.Text = "N/A";
                txtShipperStreet.Text = "N/A";
                txtShipperBarangay.Text = "N/A";
                if (lstOriginBco.Items.Count > 0)
                    lstOriginBco.SelectedIndex = -1;
                if (lstOriginCity.Items.Count > 0)
                    lstOriginCity.SelectedIndex = 0;
                txtShipperContactNo.Text = "0000000";
                txtShipperMobile.Text = "00000000000";
                txtShipperEmail.Text = "N/A";

                txtConsigneeAccountNo.Text = "";
                txtConsigneeLastName.Text = "";
                txtConsigneeFirstName.Text = "";
                txtConsigneeCompany.Text = "";
                txtConsigneeAddress1.Text = "";
                txtConsigneeAddress2.Text = "N/A";
                txtConsgineeStreet.Text = "N/A";
                txtConsigneeBarangay.Text = "N/A";
                if (lstDestinationBco.Items.Count > 0)
                    lstDestinationBco.SelectedIndex = -1;
                if (lstDestinationCity.Items.Count > 0)
                    lstDestinationCity.SelectedIndex = 0;
                txtConsigneeContactNo.Text = "0000000";
                txtConsigneeMobile.Text = "00000000000";
                txtConsigneeEmail.Text = "N/A";

                txtRemarks.Text = "";
                dateDateBooked.Value = DateTime.Now;
                chkHasDailyBooking.Checked = false;
                txtBookedBy.Text = "";

                if (lstAssignedTo.Items.Count > 0)
                    lstAssignedTo.SelectedIndex = 0;

                if (lstBookingStatus.Items.Count > 0)
                {
                    lstBookingStatus.SelectedIndex = 1;
                }
                if (lstBookingRemarks.Items.Count > 0)
                    lstBookingRemarks.SelectedIndex = -1;

                txtShipperLastName.Enabled = true;
                txtShipperFirstName.Enabled = true;
                txtShipperCompany.Enabled = true;
                txtShipperAddress1.Enabled = true;
                txtShipperAddress2.Enabled = true;
                txtShipperStreet.Enabled = true;
                txtShipperBarangay.Enabled = true;
                lstOriginBco.Enabled = true;
                lstOriginCity.Enabled = true;
                txtShipperContactNo.Enabled = true;
                txtShipperMobile.Enabled = true;
                txtShipperEmail.Enabled = true;

                txtConsigneeLastName.Enabled = true;
                txtConsigneeFirstName.Enabled = true;
                txtConsigneeCompany.Enabled = true;
                txtConsigneeAddress1.Enabled = true;
                txtConsigneeAddress2.Enabled = true;
                txtConsgineeStreet.Enabled = true;
                txtConsigneeBarangay.Enabled = true;
                lstDestinationBco.Enabled = true;
                lstDestinationCity.Enabled = true;
                txtConsigneeContactNo.Enabled = true;
                txtConsigneeMobile.Enabled = true;
                txtConsigneeEmail.Enabled = true;

                txtRemarks.Enabled = true;
                dateDateBooked.Enabled = true;
                chkHasDailyBooking.Enabled = true;
                txtBookedBy.Enabled = true;
                txtBookingNo.Enabled = true;
                lstAssignedTo.Enabled = true;
                lstBookingStatus.Enabled = false;
                lstBookingRemarks.Enabled = false;

                btnNew.Enabled = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                btnAcceptance.Enabled = false;
                btnDelete.Enabled = false;

                lstOriginBco.SelectedValue = GlobalVars.DeviceBcoId;
            }
        }

        private void DeleteBooking()
        {
            booking.ModifiedBy = AppUser.User.Id;
            booking.ModifiedDate = DateTime.Now;
            booking.RecordStatus = (int)RecordStatus.Deleted;
            bookingService.AddEdit(booking);
            ResetAll();
            PopulateGrid();
        }

        private void ResetAll()
        {
            txtShipperAccountNo.Text = "";
            txtShipperLastName.Text = "";
            txtShipperFirstName.Text = "";
            txtShipperCompany.Text = "";
            txtShipperAddress1.Text = "";
            txtShipperAddress2.Text = "";
            txtShipperStreet.Text = "";
            txtShipperBarangay.Text = "";
            if (lstOriginBco.Items.Count > 0)
                lstOriginBco.SelectedIndex = -1;
            if (lstOriginCity.Items.Count > 0)
                lstOriginCity.SelectedIndex = -1;
            txtShipperContactNo.Text = "";
            txtShipperMobile.Text = "";
            txtShipperEmail.Text = "";

            txtConsigneeAccountNo.Text = "";
            txtConsigneeLastName.Text = "";
            txtConsigneeFirstName.Text = "";
            txtConsigneeCompany.Text = "";
            txtConsigneeAddress1.Text = "";
            txtConsigneeAddress2.Text = "";
            txtConsgineeStreet.Text = "";
            txtConsigneeBarangay.Text = "";
            if (lstDestinationBco.Items.Count > 0)
                lstDestinationBco.SelectedIndex = -1;
            if (lstDestinationCity.Items.Count > 0)
                lstDestinationCity.SelectedIndex = -1;
            txtConsigneeContactNo.Text = "";
            txtConsigneeMobile.Text = "";
            txtConsigneeEmail.Text = "";

            txtRemarks.Text = "";
            dateDateBooked.Value = DateTime.Now;
            chkHasDailyBooking.Checked = false; txtBookedBy.Text = "";
            txtBookingNo.Text = "";
            if (lstAssignedTo.Items.Count > 0)
                lstAssignedTo.SelectedIndex = 0;
            if (lstBookingStatus.Items.Count > 0)
                lstBookingStatus.SelectedIndex = 0;
            if (lstBookingRemarks.Items.Count > 0)
                lstBookingRemarks.SelectedIndex = -1;

            txtShipperLastName.Enabled = false;
            txtShipperFirstName.Enabled = false;
            txtShipperCompany.Enabled = false;
            txtShipperAddress1.Enabled = false;
            txtShipperAddress2.Enabled = false;
            txtShipperStreet.Enabled = false;
            txtShipperBarangay.Enabled = false;
            lstOriginBco.Enabled = false;
            lstOriginCity.Enabled = false;
            txtShipperContactNo.Enabled = false;
            txtShipperMobile.Enabled = false;
            txtShipperEmail.Enabled = false;

            txtConsigneeLastName.Enabled = false;
            txtConsigneeFirstName.Enabled = false;
            txtConsigneeCompany.Enabled = false;
            txtConsigneeAddress1.Enabled = false;
            txtConsigneeAddress2.Enabled = false;
            txtConsgineeStreet.Enabled = false;
            txtConsigneeBarangay.Enabled = false;
            lstDestinationBco.Enabled = false;
            lstDestinationCity.Enabled = false;
            txtConsigneeContactNo.Enabled = false;
            txtConsigneeMobile.Enabled = false;
            txtConsigneeEmail.Enabled = false;

            txtRemarks.Enabled = false;
            dateDateBooked.Enabled = false;
            chkHasDailyBooking.Enabled = false;
            txtBookedBy.Enabled = false;
            txtBookingNo.Enabled = false;
            lstAssignedTo.Enabled = false;
            lstBookingStatus.Enabled = false;
            lstBookingRemarks.Enabled = false;

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            btnAcceptance.Enabled = false;
            btnDelete.Enabled = false;
        }

        private string GetBookingNumber()
        {
            string date = DateTime.Now.ToString("yy");
            int lastBooking = 1;
            var deviceCode = ConfigurationSettings.AppSettings["DeviceCode"];
            var bookings = bookingService.FilterActive();
            if (bookings != null && bookings.Count > 0)
            {
                lastBooking = Convert.ToInt32(bookings.Max(x => Convert.ToInt32(x.BookingNo.Substring(x.BookingNo.Length - 5, 5)))) + 1;
            }
            if (string.IsNullOrEmpty(deviceCode))
            {
                deviceCode = "C000";
            }

            return deviceCode + date + lastBooking.ToString("00000");
        }

        private void CreateShipper()
        {
            var _client = clients.FirstOrDefault(x => x.LastName.Equals(txtShipperLastName.Text.Trim()) && x.FirstName.Equals(txtShipperFirstName.Text.Trim()));
            if (_client != null)
            {
                shipper = _client;
                booking.ShipperId = shipper.ClientId;
                booking.Shipper = shipper;
                txtShipperAccountNo.Text = shipper.AccountNo;
                if (shipper.CompanyId != null)
                { txtShipperCompany.Text = shipper.Company.CompanyName + " - " + shipper.Company.AccountNo; }
                else
                {
                    txtShipperCompany.Text = shipper.CompanyName;
                }
                txtShipperAddress1.Text = shipper.Address1;
                txtShipperAddress2.Text = shipper.Address2;
                txtShipperStreet.Text = shipper.Street;
                txtShipperBarangay.Text = shipper.Barangay;
                if (shipper.City != null)
                {
                    lstOriginCity.SelectedValue = shipper.City.CityId;
                }
                txtShipperContactNo.Text = shipper.ContactNo;
                txtShipperMobile.Text = shipper.Mobile;
                txtShipperEmail.Text = shipper.Email;
            }
            else
            {
                shipper = new Entities.Client();
                shipper.LastName = txtShipperLastName.Text.Trim();
                shipper.FirstName = txtShipperFirstName.Text.Trim();
            }
        }

        private void CreateConsignee()
        {
            var _client = clients.FirstOrDefault(x => x.LastName.Equals(txtConsigneeLastName.Text.Trim()) && x.FirstName.Equals(txtConsigneeFirstName.Text.Trim()));
            if (_client != null)
            {
                consignee = _client;
                booking.ConsigneeId = consignee.ClientId;
                booking.Consignee = consignee;
                txtConsigneeAccountNo.Text = consignee.AccountNo;
                if (consignee.CompanyId != null)
                { txtConsigneeCompany.Text = consignee.Company.CompanyName + " - " + consignee.Company.AccountNo; }
                else
                { txtConsigneeCompany.Text = consignee.CompanyName; }

                txtConsigneeAddress1.Text = consignee.Address1;
                txtConsigneeAddress2.Text = consignee.Address2;
                txtConsgineeStreet.Text = consignee.Street;
                txtConsigneeBarangay.Text = consignee.Barangay;
                if (consignee.City != null)
                {
                    lstDestinationCity.SelectedValue = consignee.City.CityId;
                }
                txtConsigneeContactNo.Text = consignee.ContactNo;
                txtConsigneeMobile.Text = consignee.Mobile;
                txtConsigneeEmail.Text = consignee.Email;
            }
            else
            {
                consignee = new Entities.Client();
                consignee.LastName = txtConsigneeLastName.Text.Trim();
                consignee.FirstName = txtConsigneeFirstName.Text.Trim();
            }
            btnSave.Enabled = true;
        }

        #region ShipperConsigneeNavigation
        private void txtShippperLastName_Enter(object sender, EventArgs e)
        {
            shipperLastNames = new AutoCompleteStringCollection();
            var lastnames = clients.OrderBy(x => x.LastName).Select(x => x.LastName).ToList();
            foreach (var item in lastnames)
            {
                shipperLastNames.Add(item);
            }
            txtShipperLastName.AutoCompleteCustomSource = shipperLastNames;
        }

        private void txtShippperLastName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperFirstName.Focus();
            }
        }

        private void txtShipperFirstName_Enter(object sender, EventArgs e)
        {
            shipperFirstNames = new AutoCompleteStringCollection();
            var firstnames = clients.Where(x => x.LastName.Equals(txtShipperLastName.Text.Trim())).OrderBy(x => x.FirstName).Select(x => x.FirstName).ToList();
            foreach (var item in firstnames)
            {
                shipperFirstNames.Add(item);
            }
            txtShipperFirstName.AutoCompleteCustomSource = shipperFirstNames;
        }

        private void txtShipperFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperCompany.Focus();
            }
        }

        private void txtShipperFirstName_Leave(object sender, EventArgs e)
        {
            CreateShipper();
        }

        private void txtShipperCompany_Enter(object sender, EventArgs e)
        {
            shipperCompany = new AutoCompleteStringCollection();
            if (shipper != null)
            {
                var _clients = clients.Where(x => x.LastName.Equals(shipper.LastName) && x.FirstName.Equals(shipper.FirstName)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                if (_clients.Count > 0)
                {
                    foreach (var item in _clients)
                    {
                        if (item.CompanyId == null)
                            shipperCompany.Add(item.CompanyName);
                        else
                            shipperCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }
                else
                {
                    var _companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();
                    foreach (var item in _companies)
                    {
                        if (!string.IsNullOrEmpty(item.CompanyName))
                            shipperCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }

                txtShipperCompany.AutoCompleteCustomSource = shipperCompany;
            }

        }

        private void txtShipperCompany_Leave(object sender, EventArgs e)
        {

        }

        private void txtConsigneeLastName_Enter(object sender, EventArgs e)
        {
            clientConsigneeLastNames = new AutoCompleteStringCollection();
            var lastnames = clients.OrderBy(x => x.LastName).Select(x => x.LastName).ToList();
            foreach (var item in lastnames)
            {
                clientConsigneeLastNames.Add(item);
            }
            txtConsigneeLastName.AutoCompleteCustomSource = clientConsigneeLastNames;
        }

        private void txtConsigneeLastName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeFirstName.Focus();
            }
        }

        private void txtConsigneeFirstName_Enter(object sender, EventArgs e)
        {
            clientConsigneeFirstNames = new AutoCompleteStringCollection();
            var firstnames = clients.Where(x => x.LastName.Equals(txtConsigneeLastName.Text.Trim())).OrderBy(x => x.FirstName).Select(x => x.FirstName).ToList();
            foreach (var item in firstnames)
            {
                clientConsigneeFirstNames.Add(item);
            }
            txtConsigneeFirstName.AutoCompleteCustomSource = clientConsigneeFirstNames;
        }

        private void txtConsigneeFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeCompany.Focus();
            }
        }

        private void txtConsigneeFirstName_Leave(object sender, EventArgs e)
        {
            CreateConsignee();
        }

        private void txtConsigneeCompany_Enter(object sender, EventArgs e)
        {
            consigneeCompany = new AutoCompleteStringCollection();
            if (consignee != null)
            {
                var _clients = clients.Where(x => x.LastName.Equals(consignee.LastName) && x.FirstName.Equals(consignee.FirstName)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                if (_clients.Count > 0)
                {
                    foreach (var item in _clients)
                    {
                        if (item.CompanyId == null)
                            shipperCompany.Add(item.CompanyName);
                        else
                            shipperCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }
                else
                {
                    var _companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();
                    foreach (var item in _companies)
                    {
                        if (!string.IsNullOrEmpty(item.CompanyName))
                            consigneeCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }

                txtConsigneeCompany.AutoCompleteCustomSource = consigneeCompany;
            }

        }

        private void txtConsigneeCompany_Leave(object sender, EventArgs e)
        {

        }
        #endregion

        #region ControlNavigation
        private void lstOriginBco_Validated(object sender, EventArgs e)
        {
            if (lstOriginBco.SelectedValue == null || lstOriginBco.SelectedIndex < 0)
            {
                MessageBox.Show("BCO not selected", "Data Error", MessageBoxButtons.OK);
            }
            else
            {
                var bcoId = Guid.Parse(lstOriginBco.SelectedValue.ToString());
                SelectedOriginCity(bcoId);

                lstAssignedTo.DataSource = null;
                lstAssignedTo.Refresh();
                areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                var _areas = areas.Where(x => x.City.BranchCorpOfficeId == bcoId).ToList();
                lstAssignedTo.DataSource = _areas;
                lstAssignedTo.DisplayMember = "RevenueUnitName";
                lstAssignedTo.ValueMember = "RevenueUnitId";
                assignedTo = new AutoCompleteStringCollection();
                foreach (var item in _areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
                {
                    assignedTo.Add(item);
                }
                lstAssignedTo.AutoCompleteCustomSource = assignedTo;

                lstOriginCity.Focus();
            }
        }

        private void lstOriginBco_SelectionChangeCommitted(object sender, EventArgs e)
        {
          
        }

        private void lstOriginBco_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void SelectedOriginCity(Guid bcoId)
        {
            var cityIds = revenueUnits.Where(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x => x.City.CityId).ToList();
            var _cities = cities.Where(x => cityIds.Contains(x.CityId)).OrderBy(x => x.CityName).ToList();
            lstOriginCity.DataSource = _cities;
            lstOriginCity.DisplayMember = "CityName";
            lstOriginCity.ValueMember = "CityId";
            
        }

        private void lstDestinationBco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (lstDestinationBco.SelectedIndex >= 0)
            //{
            //    var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
            //    SelectedDestinationCity(bcoId);
            //}
        }

        private void lstDestinationBco_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (lstDestinationBco.SelectedValue == null)
            //    {
            //        MessageBox.Show("BCO not selected", "Data Error", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
            //        SelectedDestinationCity(bcoId);
            //        lstDestinationCity.Focus();
            //    }
            //}
        }

        private void SelectedDestinationCity(Guid bcoId)
        {
            var cityIds = revenueUnits.Where(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x => x.City.CityId).ToList();
            var _cities = cities.Where(x => cityIds.Contains(x.CityId)).OrderBy(x => x.CityName).ToList();
            lstDestinationCity.DataSource = _cities;
            lstDestinationCity.DisplayMember = "CityName";
            lstDestinationCity.ValueMember = "CityId";
        }

        private void lstOriginCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //txtShipperContactNo.Focus();
        }

        private void lstOriginCity_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtShipperContactNo.Focus();
            //}
        }
        
        private void lstDestinationCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtConsigneeContactNo.Focus();
        }

        private void lstDestinationCity_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeContactNo.Focus();
            }
        }
        
        private void chkHasDailyBooking_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //lstAssignedTo.Focus();
            }
        }

        private void txtShipperCompany_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperAddress1.Focus();
            }
        }

        private void txtShipperContactNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperMobile.Focus();
            }
        }

        private void txtShipperEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeLastName.Focus();
            }
        }

        private void txtConsigneeCompany_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeAddress1.Focus();
            }
        }

        private void txtConsigneeContactNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeMobile.Focus();
            }
        }

        private void txtConsigneeEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateDateBooked.Focus();
            }
        }

        #endregion

        #region ButtonEvents

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBooking();
        }

        private void btnSave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveBooking();
            }
        }

        private void SaveBooking()
        {
            if (IsValidData())
            {
                #region ShipperInfo
                shipper.CreatedBy = AppUser.User.Id;
                shipper.CreatedDate = DateTime.Now;
                shipper.ModifiedBy = AppUser.User.Id;
                shipper.ModifiedDate = DateTime.Now;
                shipper.RecordStatus = (int)RecordStatus.Active;
                shipper.CompanyName = txtShipperCompany.Text.Trim();
                shipper.Address1 = txtShipperAddress1.Text.Trim();
                shipper.Address2 = txtShipperAddress2.Text.Trim();
                shipper.Street = txtShipperStreet.Text.Trim();
                shipper.Barangay = txtShipperBarangay.Text.Trim();
                if (lstOriginCity.SelectedIndex >= 0)
                {
                    shipper.CityId = Guid.Parse(lstOriginCity.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Invalid Shipper City.", "Data Error", MessageBoxButtons.OK);
                    return;
                }

                shipper.ContactNo = txtShipperContactNo.Text.Trim();
                shipper.Mobile = txtShipperMobile.Text.Trim();
                shipper.Email = txtShipperEmail.Text.Trim();
                if (shipper.CompanyId == null)
                {
                    if (shipper.CompanyName.Contains(" - "))
                    {
                        Guid id = GetCompanyIdByString(shipper.CompanyName);
                        if (id == Guid.Empty)
                            shipper.CompanyId = null;
                        else
                            shipper.CompanyId = id;
                    }
                }
                #endregion

                #region ConsingnessInfo
                consignee.CreatedBy = AppUser.User.Id;
                consignee.CreatedDate = DateTime.Now;
                consignee.ModifiedBy = AppUser.User.Id;
                consignee.ModifiedDate = DateTime.Now;
                consignee.RecordStatus = (int)RecordStatus.Active;
                consignee.CompanyName = txtConsigneeCompany.Text.Trim();
                consignee.Address1 = txtConsigneeAddress1.Text.Trim();
                consignee.Address2 = txtConsigneeAddress2.Text.Trim();
                consignee.Street = txtConsgineeStreet.Text.Trim();
                consignee.Barangay = txtConsigneeBarangay.Text.Trim();
                if (lstDestinationCity.SelectedIndex>=0)
                {
                    consignee.CityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Invalid Consignee City.", "Data Error", MessageBoxButtons.OK);
                    return;
                }
                consignee.ContactNo = txtConsigneeContactNo.Text.Trim();
                consignee.Mobile = txtConsigneeMobile.Text.Trim();
                consignee.Email = txtConsigneeEmail.Text.Trim();
                if (consignee.CompanyId == null)
                {
                    if (consignee.CompanyName.Contains(" - "))
                    {
                        Guid id = GetCompanyIdByString(consignee.CompanyName);
                        if (id == Guid.Empty)
                            consignee.CompanyId = null;
                        else
                            consignee.CompanyId = id;
                    }
                }
                #endregion

                #region CaptureBookingInput
                booking.OriginAddress1 = txtShipperAddress1.Text.Trim();
                booking.OriginAddress2 = txtShipperAddress2.Text.Trim();
                booking.OriginStreet = txtShipperStreet.Text.Trim();
                booking.OriginBarangay = txtShipperBarangay.Text.Trim();
                booking.OriginCityId = Guid.Parse(lstOriginCity.SelectedValue.ToString());
                booking.DestinationAddress1 = txtConsigneeAddress1.Text.Trim();
                booking.DestinationAddress2 = txtConsigneeAddress2.Text.Trim();
                booking.DestinationStreet = txtConsgineeStreet.Text.Trim();
                booking.DestinationBarangay = txtConsigneeBarangay.Text.Trim();
                booking.DestinationCityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
                booking.DateBooked = dateDateBooked.Value;
                booking.Remarks = txtRemarks.Text;
                booking.HasDailyBooking = chkHasDailyBooking.Checked;
                booking.AssignedToAreaId = Guid.Parse(lstAssignedTo.SelectedValue.ToString());
                booking.BookingStatusId = Guid.Parse(lstBookingStatus.SelectedValue.ToString());
                if (lstBookingRemarks.SelectedValue != null)
                    booking.BookingRemarkId = Guid.Parse(lstBookingRemarks.SelectedValue.ToString());
                booking.ModifiedBy = AppUser.User.Id;
                booking.ModifiedDate = DateTime.Now;
                booking.RecordStatus = (int)RecordStatus.Active;
                if (booking.BookingId == null || booking.BookingId == Guid.Empty)
                {
                    booking.BookingId = Guid.NewGuid();
                    booking.CreatedBy = AppUser.User.Id;
                    booking.CreatedDate = DateTime.Now;
                }
                #endregion

                ProgressIndicator saving = new ProgressIndicator("Booking", "Saving ...", Saving);
                saving.ShowDialog();

                PopulateGrid();
                ResetAll();
                booking = null;
                shipper = null;
                consignee = null;
            }
            else
            {

            }
        }

        private void Saving(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            #region NewClient
            if (shipper.ClientId == null || shipper.ClientId == Guid.Empty)
            {
                shipper.ClientId = Guid.NewGuid();
                if (shipper.City == null)
                    shipper.City = cities.FirstOrDefault(x => x.CityId == shipper.CityId);
                if (shipper.CompanyId == null)
                {
                    // non-rep client account #
                    shipper.AccountNo = "1" + clientService.GetNewAccountNo(shipper.City.CityCode, false);
                }
                else
                {
                    shipper.AccountNo = "2" + clientService.GetNewAccountNo(shipper.City.CityCode, false);
                }
                clientService.Add(shipper);
                booking.ShipperId = shipper.ClientId;
            }

            if (consignee.ClientId == null || consignee.ClientId == Guid.Empty)
            {
                consignee.ClientId = Guid.NewGuid();
                if (consignee.City == null)
                    consignee.City = cities.FirstOrDefault(x => x.CityId == consignee.CityId);
                if (consignee.CompanyId == null)
                {
                    // non-rep client account #
                    consignee.AccountNo = "1" + clientService.GetNewAccountNo(consignee.City.CityCode, false);
                }
                else
                {
                    consignee.AccountNo = "2" + clientService.GetNewAccountNo(consignee.City.CityCode, false);
                }
                consignee.AccountNo = clientService.GetNewAccountNo(consignee.City.CityCode, false);
                clientService.Add(consignee);
                booking.ConsigneeId = consignee.ClientId;
            }
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            #endregion

            #region SaveBooking
            bookingService.AddEdit(booking);

            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            #endregion

        }

        private void btnAcceptance_Click(object sender, EventArgs e)
        {
            btnAcceptance.Enabled = false;
            btnSave.Enabled = false;
            NewShipment();
        }

        private void btnAcceptance_KeyUp(object sender, KeyEventArgs e)
        {
            btnAcceptance.Enabled = false;
            btnSave.Enabled = false;
            NewShipment();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void btnReset_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResetAll();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewBooking();
        }

        private void btnNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NewBooking();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBooking();
        }

        private void btnDelete_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DeleteBooking();
            }
        }

        #endregion

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void btnSearchBooking_Click(object sender, EventArgs e)
        {
            string searchInput = txtSearchInput.Text.Trim();
            string searchIn = lstSearchIn.SelectedItem.ToString();
            List<Booking> bookings = new List<Booking>();

            switch (searchIn)
            {
                case "Booking Date":
                    DateTime bookingDate;
                    if (DateTime.TryParse(searchInput, out bookingDate))
                    {
                        bookings = bookingService.FilterActiveBy(x => x.DateBooked.Year == bookingDate.Year && x.DateBooked.Month == bookingDate.Month && x.DateBooked.Day == bookingDate.Day).ToList();
                    }
                    break;
                case "Booking No":
                    bookings =
                           bookingService.FilterActiveBy(x => x.BookingNo.Equals(searchInput)).ToList();
                    break;
                case "Shipper Lastname":
                    bookings =
                           bookingService.FilterActiveBy(x => x.Shipper.LastName.Contains(searchInput)).ToList();
                    break;
                default:
                    bookings = bookingService.FilterActive();
                    break;
            }

            PopulateGrid(bookings);
        }

        private bool IsValidData()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtShipperLastName.Text))
            {
                MessageBox.Show("Invalid Shipper Lastname", "Data Error", MessageBoxButtons.OK);
                //txtShipperLastName.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtShipperFirstName.Text))
            {
                MessageBox.Show("Invalid Shipper Firstname", "Data Error", MessageBoxButtons.OK);
                //txtShipperFirstName.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtShipperAddress1.Text))
            {
                MessageBox.Show("Invalid Shipper Address", "Data Error", MessageBoxButtons.OK);
                //txtShipperAddress.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtConsigneeLastName.Text))
            {
                MessageBox.Show("Invalid Consignee Lastname", "Data Error", MessageBoxButtons.OK);
                //txtConsigneeLastName.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtConsigneeFirstName.Text))
            {
                MessageBox.Show("Invalid Consignee Firstname", "Data Error", MessageBoxButtons.OK);
                //txtConsigneeFirstName.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtConsigneeAddress1.Text))
            {
                MessageBox.Show("Invalid Consignee Address", "Data Error", MessageBoxButtons.OK);
                //txtConsigneeAddress.Focus();
                isValid = false;
            }
            else if (lstAssignedTo.SelectedIndex <= -1)
            {
                MessageBox.Show("Booking is not assigned to an Area.", "Data Error", MessageBoxButtons.OK);
                //lstAssignedTo.Focus();
                isValid = false;
            }
            else if (lstBookingStatus.SelectedIndex <= -1)
            {
                MessageBox.Show("Invalid Booking Status", "Data Error", MessageBoxButtons.OK);
                //lstBookingStatus.Focus();
                isValid = false;
            }

            if (lstOriginBco.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Shipper BCO.", "Data Error", MessageBoxButtons.OK);
                isValid = false;
            }

            if (lstOriginCity.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Shipper City.", "Data Error", MessageBoxButtons.OK);
                isValid = false;
            }

            if (lstDestinationBco.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Consignee BCO.", "Data Error", MessageBoxButtons.OK);
                isValid = false;
            }

            if (lstDestinationCity.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Consignee City.", "Data Error", MessageBoxButtons.OK);
                    isValid = false;
                }

                if (string.IsNullOrEmpty(txtShipperContactNo.Text) && string.IsNullOrEmpty(txtShipperMobile.Text))
            {
                MessageBox.Show("Shipper Contact and Mobile cannot be empty.", "Data Error", MessageBoxButtons.OK);
                //txtShipperContactNo.Focus();
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtShipperContactNo.Text) && (!IsNumericOnly(7, 7, txtShipperContactNo.Text)))
            {
                MessageBox.Show("Invalid Shipper Contact No.", "Data Error", MessageBoxButtons.OK);
                //txtShipperContactNo.Focus();
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtShipperMobile.Text) && (!IsNumericOnly(11, 11, txtShipperMobile.Text)))
            {
                MessageBox.Show("Invalid Shipper Mobile.", "Data Error", MessageBoxButtons.OK);
                //txtShipperContactNo.Focus();
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtConsigneeContactNo.Text) && string.IsNullOrEmpty(txtConsigneeMobile.Text))
            {
                MessageBox.Show("Consginee Contact and Mobile cannot be empty.", "Data Error", MessageBoxButtons.OK);
                //txtConsigneeContactNo.Focus();
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtConsigneeContactNo.Text) && (!IsNumericOnly(0, 7, txtConsigneeContactNo.Text)))
            {
                MessageBox.Show("Invalid Consginee Contact No.", "Data Error", MessageBoxButtons.OK);
                //txtShipperContactNo.Focus();
                isValid = false;
            }
            if (!string.IsNullOrEmpty(txtConsigneeMobile.Text) && (!IsNumericOnly(0, 11, txtConsigneeMobile.Text)))
            {
                MessageBox.Show("Invalid Consginee Mobile.", "Data Error", MessageBoxButtons.OK);
                //txtShipperContactNo.Focus();
                isValid = false;
            }

            return isValid;
        }

        private Boolean IsNumericOnly(int intMin, int intMax, string strNumericOnly)
        {
            Boolean blnResult = false;
            Regex regexPattern = new Regex("^[0-9\\s]{" + intMin.ToString() + "," + intMax.ToString() + "}$");
            if ((strNumericOnly.Length >= intMin & strNumericOnly.Length <= intMax))
            {
                // check here if there are other none alphanumeric characters
                strNumericOnly = strNumericOnly.Trim();
                blnResult = regexPattern.IsMatch(strNumericOnly);
            }
            else
            {
                blnResult = false;
            }
            return blnResult;
        }

        private void lstOriginBco_Enter(object sender, EventArgs e)
        {
            shipperBco = new AutoCompleteStringCollection();
            var bcos = branchCorpOffices.OrderBy(x => x.BranchCorpOfficeName).Select(x => x.BranchCorpOfficeName).ToList();
            foreach (var item in bcos)
            {
                shipperBco.Add(item);
            }
            lstOriginBco.AutoCompleteCustomSource = shipperBco;
        }

        private void lstOriginCity_Enter(object sender, EventArgs e)
        {
            if (lstOriginBco.SelectedIndex >= 0)
            {
                var bcoId = Guid.Parse(lstOriginBco.SelectedValue.ToString());
                var cityIds = revenueUnits.Where(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x => x.City.CityId).ToList();
                var _cities = cities.Where(x => cityIds.Contains(x.CityId)).OrderBy(x => x.CityName).Select(x => x.CityName).ToList();
                shipperCity = new AutoCompleteStringCollection();
                foreach (var item in _cities)
                {
                    shipperCity.Add(item);
                }
                lstOriginCity.AutoCompleteCustomSource = shipperCity;
            }
        }

        private void lstOriginCity_Validated(object sender, EventArgs e)
        {
            txtShipperContactNo.Focus();
        }

        private void lstDestinationBco_Enter(object sender, EventArgs e)
        {
            consgineeBco = new AutoCompleteStringCollection();
            var bcos = branchCorpOffices.OrderBy(x => x.BranchCorpOfficeName).Select(x => x.BranchCorpOfficeName).ToList();
            foreach (var item in bcos)
            {
                consgineeBco.Add(item);
            }
            lstDestinationBco.AutoCompleteCustomSource = consgineeBco;
        }

        private void lstDestinationBco_Validated(object sender, EventArgs e)
        {
            if (lstDestinationBco.SelectedValue == null || lstDestinationBco.SelectedIndex < 0)
            {
                MessageBox.Show("BCO not selected", "Data Error", MessageBoxButtons.OK);
            }
            else
            {
                var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
                SelectedDestinationCity(bcoId);
                lstDestinationCity.Focus();
            }
        }

        private void lstDestinationCity_Enter(object sender, EventArgs e)
        {
            if (lstDestinationBco.SelectedIndex > 0)
            {
                var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
                var cityIds = revenueUnits.Where(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x => x.City.CityId).ToList();
                var _cities = cities.Where(x => cityIds.Contains(x.CityId)).OrderBy(x => x.CityName).Select(x => x.CityName).ToList();
                consgineeCity = new AutoCompleteStringCollection();
                foreach (var item in _cities)
                {
                    consgineeCity.Add(item);
                }
                lstDestinationCity.AutoCompleteCustomSource = consgineeCity;
            }
        }

        private void lstAssignedTo_Enter(object sender, EventArgs e)
        {
            //assignedTo = new AutoCompleteStringCollection();
            //foreach (var item in areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
            //{
            //    assignedTo.Add(item);
            //}
            //lstAssignedTo.AutoCompleteCustomSource = assignedTo;
        }

        private void txtShipperAddress1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperAddress2.Focus();
            }
        }

        private void txtShipperAddress2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperStreet.Focus();
            }
        }

        private void txtShipperStreet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperBarangay.Focus();
            }
        }

        private void txtShipperBarangay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstOriginBco.Focus();
            }
        }

        private void txtShipperMobile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipperEmail.Focus();
            }
        }

        private void txtConsigneeAddress1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeAddress2.Focus();
            }
        }

        private void txtConsigneeAddress2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsgineeStreet.Focus();
            }
        }

        private void txtConsgineeStreet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeBarangay.Focus();
            }
        }

        private void txtConsigneeBarangay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstDestinationBco.Focus();
            }
        }

        private void txtConsigneeMobile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtConsigneeEmail.Focus();
            }
        }

        private void dateDateBooked_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstAssignedTo.Focus();
            }
        }

        private void lstAssignedTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstBookingStatus.Focus();
            }
        }

        private void lstBookingStatus_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstBookingRemarks.Focus();
            }
        }

        private void lstBookingRemarks_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
        }

        private void txtRemarks_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private Guid GetCompanyIdByString(string companyname)
        {
            int startindex = companyname.IndexOf("-") + 2;
            string acctNo = companyname.Substring(startindex, companyname.Length - startindex);
            var company = companies.First(x => x.AccountNo.Equals(acctNo));
            if (company != null)
                return company.CompanyId;
            else
                return new Guid();
        }

        private void txtShipperAddress2_Enter(object sender, EventArgs e)
        {
            txtShipperAddress2.SelectAll();
        }

        private void txtShipperStreet_Enter(object sender, EventArgs e)
        {
            txtShipperStreet.SelectAll();
        }

        private void txtShipperBarangay_Enter(object sender, EventArgs e)
        {
            txtShipperBarangay.SelectAll();
        }

        private void txtShipperContactNo_Enter(object sender, EventArgs e)
        {
            txtShipperContactNo.SelectAll();
        }

        private void txtShipperMobile_Enter(object sender, EventArgs e)
        {
            txtShipperMobile.SelectAll();
        }

        private void txtShipperEmail_Enter(object sender, EventArgs e)
        {
            txtShipperEmail.SelectAll();
        }

        private void txtConsigneeAddress2_Enter(object sender, EventArgs e)
        {
            txtConsigneeAddress2.SelectAll();
        }

        private void txtConsgineeStreet_Enter(object sender, EventArgs e)
        {
            txtConsgineeStreet.SelectAll();
        }

        private void txtConsigneeBarangay_Enter(object sender, EventArgs e)
        {
            txtConsigneeBarangay.SelectAll();
        }

        private void txtConsigneeContactNo_Enter(object sender, EventArgs e)
        {
            txtConsigneeContactNo.SelectAll();
        }

        private void txtConsigneeMobile_Enter(object sender, EventArgs e)
        {
            txtConsigneeMobile.SelectAll();
        }

        private void txtConsigneeEmail_Enter(object sender, EventArgs e)
        {
            txtConsigneeEmail.SelectAll();
        }

       
    }
}
// TODO transfer this to service
//private string GetNewClientAccountNo()
//{
//    string date = DateTime.Now.ToString("yyMMdd");
//    clientService.FilterActiveBy(x => x.AccountNo.Substring(3, 6).Equals(date));
//    var lastClient = clients.Max(x => x.AccountNo.Substring(9, 3));
//    if (lastClient != null)
//    {
//        int lastSequence = Convert.ToInt32(lastClient);
//        return AppUser.EmployeeAssignment.AssignedLocation.City.CityCode + date + (lastSequence + 1).ToString("000");
//    }
//    else
//    {
//        return AppUser.EmployeeAssignment.AssignedLocation.City.CityCode + date + "001";
//    }
//}