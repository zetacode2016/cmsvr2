using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Client.ReportModels;
using CMS2.Client.ViewModels;
using CMS2.Common;
using CMS2.Common.Enums;
using CMS2.Entities;
using CMS2.Entities.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CMS2.Client
{
    public partial class CmsAcceptance : Form
    {
        #region ClassVariables

        private ShipmentModel shipment;
        private PackageDimensionModel packageDimensionModel;
        private BindingSource bsCommodityType;
        private BindingSource bsCommodity;
        private BindingSource bsServiceType;
        private BindingSource bsServiceMode;
        private BindingSource bsPaymentMode;
        private BindingSource bsCrating;
        private BindingSource bsPackaging;
        private BindingSource bsGoodsDescription;
        private BindingSource bsShipMode;
        private AutoCompleteStringCollection commodityTypeCollection;
        private AutoCompleteStringCollection commodityCollection;
        private AutoCompleteStringCollection serviceTypeCollection;
        private AutoCompleteStringCollection serviceModeCollection;
        private AutoCompleteStringCollection shipModeCollection;
        private AutoCompleteStringCollection goodsDescCollection;
        private AutoCompleteStringCollection paymentModeCollection;

        private CommodityTypeBL commodityTypeService;
        private CommodityBL commodityService;
        private ServiceTypeBL serviceTypeService;
        private ServiceModeBL serviceModeService;
        private PaymentModeBL paymentModeService;
        private ShipmentBL shipmentService;
        private BookingBL bookingService;
        private BookingStatusBL bookingStatusService;
        private ShipmentBasicFeeBL shipmentBasicFeeService;
        private CratingBL cratingService;
        private PackagingBL packagingService;
        private GoodsDescriptionBL goodsDescriptionService;
        private ShipModeBL shipModeService;
        private RateMatrixBL rateMatrixService;
        private PaymentTermBL paymentTermService;

        private CommodityType commodityType;
        private List<CommodityType> commodityTypes;
        private List<Commodity> commodities;
        private List<ServiceType> serviceTypes;
        private List<ServiceMode> serviceModes;
        private List<PaymentMode> paymentModes;
        private List<ShipmentBasicFee> shipmentBasicFees;
        private List<Crating> cratings;
        private List<Packaging> packagings;
        private List<GoodsDescription> goodsDescriptions;
        private List<ShipMode> shipModes;
        private List<PaymentTerm> paymentTerms;

        public ShipmentModel shipmentModel { get; set; } // model from Booking

        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        public Logs logs = new Logs();

        #endregion

        public CmsAcceptance()
        {
            InitializeComponent();
        }

        private void CmsAcceptance_Load(object sender, EventArgs e)
        {
            shipment = new ShipmentModel();
            shipment.PackageDimensions = new List<PackageDimensionModel>();

            bsCommodityType = new BindingSource();
            bsCommodity = new BindingSource();
            bsServiceType = new BindingSource();
            bsServiceMode = new BindingSource();
            bsPaymentMode = new BindingSource();
            bsCrating = new BindingSource();
            bsPackaging = new BindingSource();
            bsGoodsDescription = new BindingSource();
            bsShipMode = new BindingSource();

            commodityTypes = new List<CommodityType>();
            commodities = new List<Commodity>();
            serviceTypes = new List<ServiceType>();
            serviceModes = new List<ServiceMode>();
            paymentModes = new List<PaymentMode>();
            shipmentBasicFees = new List<ShipmentBasicFee>();
            cratings = new List<Crating>();
            packagings = new List<Packaging>();
            goodsDescriptions = new List<GoodsDescription>();
            shipModes = new List<ShipMode>();
            paymentTerms = new List<PaymentTerm>();

            commodityTypeService = new CommodityTypeBL(GlobalVars.UnitOfWork);
            commodityService = new CommodityBL(GlobalVars.UnitOfWork);
            serviceTypeService = new ServiceTypeBL(GlobalVars.UnitOfWork);
            serviceModeService = new ServiceModeBL(GlobalVars.UnitOfWork);
            paymentModeService = new PaymentModeBL(GlobalVars.UnitOfWork);
            shipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
            bookingService = new BookingBL(GlobalVars.UnitOfWork);
            bookingStatusService = new BookingStatusBL(GlobalVars.UnitOfWork);
            shipmentBasicFeeService = new ShipmentBasicFeeBL(GlobalVars.UnitOfWork);
            cratingService = new CratingBL(GlobalVars.UnitOfWork);
            packagingService = new PackagingBL(GlobalVars.UnitOfWork);
            goodsDescriptionService = new GoodsDescriptionBL(GlobalVars.UnitOfWork);
            shipModeService = new ShipModeBL(GlobalVars.UnitOfWork);
            rateMatrixService = new RateMatrixBL(GlobalVars.UnitOfWork);
            paymentTermService = new PaymentTermBL(GlobalVars.UnitOfWork);

            commodityTypes = commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList();
            commodities = commodityService.FilterActive().OrderBy(x => x.CommodityName).ToList();
            serviceTypes = serviceTypeService.FilterActive().OrderBy(x => x.ServiceTypeName).ToList();
            serviceModes = serviceModeService.FilterActive().OrderBy(x => x.ServiceModeName).ToList();
            paymentModes = paymentModeService.FilterActive().OrderBy(x => x.PaymentModeName).ToList();
            shipmentBasicFees = shipmentBasicFeeService.FilterActive();
            cratings = cratingService.FilterActive().OrderBy(x => x.CratingName).ToList();
            packagings = packagingService.FilterActive().OrderBy(x => x.PackagingName).ToList();
            goodsDescriptions = goodsDescriptionService.FilterActive().OrderBy(x => x.GoodsDescriptionName).ToList();
            shipModes = shipModeService.FilterActive().OrderBy(x => x.ShipModeName).ToList();
            paymentTerms = paymentTermService.FilterActive().OrderBy(x => x.PaymentTermName).ToList();

            bsCommodityType.DataSource = commodityTypes;
            bsCommodity.DataSource = commodities;
            bsServiceType.DataSource = serviceTypes;
            bsServiceMode.DataSource = serviceModes;
            bsPaymentMode.DataSource = paymentModes;
            bsCrating.DataSource = cratings;
            bsPackaging.DataSource = packagings;
            bsGoodsDescription.DataSource = goodsDescriptions;
            bsShipMode.DataSource = shipModes;

            dateAcceptedDate.Value = DateTime.Now;

            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnPayment.Enabled = false;

            lstCommodityType.DataSource = bsCommodityType;
            lstCommodityType.DisplayMember = "CommodityTypeName";
            lstCommodityType.ValueMember = "CommodityTypeId";

            lstCommodity.DataSource = bsCommodity;
            lstCommodity.DisplayMember = "CommodityName";
            lstCommodity.ValueMember = "CommodityId";

            lstServiceType.DataSource = bsServiceType;
            lstServiceType.DisplayMember = "ServiceTypeName";
            lstServiceType.ValueMember = "ServiceTypeId";

            lstServiceMode.DataSource = bsServiceMode;
            lstServiceMode.DisplayMember = "ServiceModeName";
            lstServiceMode.ValueMember = "ServiceModeId";

            lstPaymentMode.DataSource = bsPaymentMode;
            lstPaymentMode.DisplayMember = "PaymentModeName";
            lstPaymentMode.ValueMember = "PaymentModeId";

            lstCrating.DataSource = bsCrating;
            lstCrating.DisplayMember = "CratingName";
            lstCrating.ValueMember = "CratingId";

            lstGoodsDescription.DataSource = bsGoodsDescription;
            lstGoodsDescription.DisplayMember = "GoodsDescriptionName";
            lstGoodsDescription.ValueMember = "GoodsDescriptionId";

            lstShipMode.DataSource = bsShipMode;
            lstShipMode.DisplayMember = "ShipModeName";
            lstShipMode.ValueMember = "ShipModeId";

            DisableForm();
        }

        private void CmsAcceptance_Leave(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void CmsAcceptance_Shown(object sender, EventArgs e)
        {
            this.Height = 690;
            panelContent.Height = 630;
            this.Top = 20;
            panelContent.Top = 0;
            this.AutoScroll = true;
            panelContent.Left = (this.Width - panelContent.Width)/2;

            bsCommodityType.ResetBindings(false);
            bsCommodity.ResetBindings(false);
            bsServiceType.ResetBindings(false);
            bsServiceMode.ResetBindings(false);
            bsPaymentMode.ResetBindings(false);
            bsCrating.ResetBindings(false);
            bsPackaging.ResetBindings(false);
            bsGoodsDescription.ResetBindings(false);
            bsShipMode.ResetBindings(false);

            lstCommodityType.SelectedIndex = -1;
            lstCommodity.SelectedIndex = -1;
            lstCrating.SelectedIndex = -1;
        }

        private void CmsAcceptance_VisibleChanged(object sender, EventArgs e)
        {
            if (shipmentModel != null)
            {
                shipment = new ShipmentModel();
                shipment.ShipmentId = Guid.NewGuid();
                shipment.OriginCityId = shipmentModel.OriginCityId;
                shipment.OriginCity = shipmentModel.OriginCity;
                shipment.DestinationCityId = shipmentModel.DestinationCityId;
                shipment.DestinationCity = shipmentModel.DestinationCity;
                shipment.ShipperId = shipmentModel.ShipperId;
                shipment.Shipper = shipmentModel.Shipper;
                shipment.OriginAddress = shipmentModel.OriginAddress;
                shipment.ConsigneeId = shipmentModel.ConsigneeId;
                shipment.Consignee = shipmentModel.Consignee;
                shipment.DestinationAddress = shipmentModel.DestinationAddress;
                shipment.BookingId = shipmentModel.BookingId;

                PopulateForm();
                DisableForm();

                txtAirwayBill.Focus();
                btnSearchShipment.Enabled = false;
            }
            else
            {
                txtAirwayBill.Focus();
                btnSearchShipment.Enabled = true;
            }
        }

        private void SaveShipment(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            #region SaveShipment

            shipmentService.AddEdit(shipment);
            percent = index*100/max;
            _worker.ReportProgress(percent);
            index++;

            #endregion

            #region UpdateBookingStatus

            var booking = bookingService.GetById(shipment.BookingId);
            if (booking != null)
            {
                var bookingStatus = bookingStatusService.FilterActiveBy(x => x.BookingStatusName.Equals("Picked-up"));
                if (bookingStatus != null)
                {
                    booking.BookingStatusId = bookingStatus.FirstOrDefault().BookingStatusId;
                    booking.ModifiedBy = AppUser.User.Id;
                    booking.ModifiedDate = DateTime.Now;
                    bookingService.Edit(booking);
                }
            }
            percent = index*100/max;
            _worker.ReportProgress(percent);
            index++;

            #endregion
        }

        private void ResetAll()
        {
            dateAcceptedDate.Value = DateTime.Now;
            txtShipperAccountNo.Text = "";
            txtShipperFullName.Text = "";
            txtShipperCompany.Text = "";
            txtShipperAddress.Text = "";
            txtShipperBarangay.Text = "";
            txtShipperContactNo.Text = "";
            txtShipperMobile.Text = "";
            txtShipperEmail.Text = "";
            txtConsigneeAccountNo.Text = "";
            txtConsigneeFullName.Text = "";
            txtConsigneeCompany.Text = "";
            txtConsigneeAddress.Text = "";
            txtConsigneeBarangay.Text = "";
            txtConsigneeContactNo.Text = "";
            txtConsingneeMobile.Text = "";
            txtConsigneeEmail.Text = "";
            txtAirwayBill.Text = "";
            txtQuantity.Text = "1";
            txtWeight.Text = "0";
            txtWidth.Text = "0";
            txtLength.Text = "0";
            txtHeight.Text = "0";
            txtTotalEvm.Text = "";
            txtTotalWeightCharge.Text = "";

            var summaryControls =
                this.Controls["panelContent"].Controls["groupSummary"].Controls["layoutSummary"].Controls;
            foreach (Control item in summaryControls)
            {
                if (item.Name.Contains("txtSum"))
                {
                    item.Text = "0.00";
                }
            }

            shipment.PackageDimensions = new List<PackageDimensionModel>();
            RefreshGridPackages();

            shipment = new ShipmentModel();

            if (lstCommodityType.Items.Count > 0)
                lstCommodityType.SelectedIndex = -1;
            if (lstCommodity.Items.Count > 0)
                lstCommodity.SelectedIndex = -1;
            if (lstServiceMode.Items.Count > 0)
                lstServiceMode.SelectedIndex = -1;
            if (lstPaymentMode.Items.Count > 0)
                lstPaymentMode.SelectedIndex = -1;
            if (lstServiceType.Items.Count > 0)
                lstServiceType.SelectedIndex = -1;
            if (lstShipMode.Items.Count > 0)
                lstShipMode.SelectedIndex = 0;
            if (lstGoodsDescription.Items.Count > 0)
                lstGoodsDescription.SelectedIndex = 0;

            txtNotes.Text = "";
            btnCompute.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnSearchShipment.Enabled = true;

            chkNonVatable.Checked = false;

        }

        // Compute Shipment Charges
        private void ComputeCharges()
        {
            shipment.DateAccepted = dateAcceptedDate.Value;
            shipment.CommodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
            shipment.CommodityType = commodityTypes.Find(x => x.CommodityTypeId == shipment.CommodityTypeId);
            shipment.ServiceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
            shipment.ServiceType = serviceTypes.Find(x => x.ServiceTypeId == shipment.ServiceTypeId);
            shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
            shipment.ServiceMode = serviceModes.Find(x => x.ServiceModeId == shipment.ServiceModeId);
            shipment.ShipModeId = Guid.Parse(lstShipMode.SelectedValue.ToString());
            shipment.ShipMode = shipModes.Find(x => x.ShipModeId == shipment.ShipModeId);
            if (shipment.GoodsDescriptionId == null || shipment.GoodsDescription == null)
            {
                if (lstGoodsDescription.SelectedValue == null)
                {
                    lstGoodsDescription.SelectedIndex = 0;
                }
                shipment.GoodsDescriptionId = Guid.Parse(lstGoodsDescription.SelectedValue.ToString());
                shipment.GoodsDescription =
                    goodsDescriptions.Find(x => x.GoodsDescriptionId == shipment.GoodsDescriptionId);
            }

            if (shipment.PaymentModeId == null || shipment.PaymentMode == null)
            {
                if (lstPaymentMode.SelectedValue == null)
                {
                    lstPaymentMode.SelectedIndex = 0;
                }
                shipment.PaymentModeId = Guid.Parse(lstPaymentMode.SelectedValue.ToString());
                shipment.PaymentMode = paymentModes.Find(x => x.PaymentModeId == shipment.PaymentModeId);
            }

            shipment.DeclaredValue = Decimal.Parse(txtDeclaredValue.Text.Trim());
            shipment.HandlingFee = Decimal.Parse(txtHandlingFee.Text.Trim());
            shipment.QuarantineFee = Decimal.Parse(txtQuarantineFee.Text.Trim());
            shipment.Discount = Decimal.Parse(txtRfa.Text.Trim());
            if (shipment.Shipper != null)
            {
                if (shipment.Shipper.Company != null)
                {
                    shipment.Discount = shipment.Shipper.Company.Discount;
                }
            }
            
            shipment = shipmentService.ComputeCharges(shipment);
            PopulateSummary();
        }

        private void PopulateForm()
        {
            if (shipment != null)
            {
                if (shipment.Shipper!=null)
                {
                    txtShipperAccountNo.Text = shipment.Shipper.AccountNo;
                    txtShipperFullName.Text = shipment.Shipper.LastName + ", " + shipment.Shipper.FirstName;
                    if (shipment.Shipper.CompanyId != null)
                    {
                        txtShipperCompany.Text = shipment.Shipper.Company.CompanyName;
                    }
                    else
                    {
                        txtShipperCompany.Text = shipment.Shipper.CompanyName;
                    }
                    txtShipperAddress.Text = shipment.Shipper.Address1 + ", " + shipment.Shipper.Address2;
                    txtShipperBarangay.Text = shipment.Shipper.Barangay;
                    txtShipperContactNo.Text = shipment.Shipper.ContactNo;
                    txtShipperMobile.Text = shipment.Shipper.Mobile;
                    txtShipperEmail.Text = shipment.Shipper.Email;
                }
                txtShipperCity.Text = shipment.OriginCity.CityName;

                if (shipment.Consignee!=null)
                {
                    txtConsigneeAccountNo.Text = shipment.Consignee.AccountNo;
                    txtConsigneeFullName.Text = shipment.Consignee.LastName + ", " + shipment.Consignee.FirstName;
                    if (shipment.Consignee.CompanyId != null)
                    {
                        txtConsigneeCompany.Text = shipment.Consignee.Company.CompanyName;
                    }
                    else
                    {
                        txtConsigneeCompany.Text = shipment.Consignee.CompanyName;
                    }
                    txtConsigneeAddress.Text = shipment.Consignee.Address1 + ", " + shipment.Consignee.Address2;
                    txtConsigneeBarangay.Text = shipment.Consignee.Barangay;
                    txtConsigneeContactNo.Text = shipment.Consignee.ContactNo;
                    txtConsingneeMobile.Text = shipment.Consignee.Mobile;
                    txtConsigneeEmail.Text = shipment.Consignee.Email;
                }
                txtConsigneeCity.Text = shipment.DestinationCity.CityName;

                lstServiceType.SelectedIndex = -1;
                lstServiceMode.SelectedIndex = -1;
                lstPaymentMode.SelectedIndex = -1;
                if (shipment.CommodityTypeId != null && shipment.CommodityTypeId != Guid.Empty)
                    lstCommodityType.SelectedValue = shipment.CommodityTypeId;
                if (shipment.CommodityId != null && shipment.CommodityId != Guid.Empty)
                    lstCommodity.SelectedValue = shipment.CommodityId;
                if (shipment.ServiceModeId != null && shipment.ServiceModeId != Guid.Empty)
                    lstServiceMode.SelectedValue = shipment.ServiceModeId;
                if (shipment.PaymentModeId != null && shipment.ServiceModeId != Guid.Empty)
                    lstPaymentMode.SelectedValue = shipment.PaymentModeId;
                if (shipment.ServiceTypeId != null && shipment.ServiceTypeId != Guid.Empty)
                    lstServiceType.SelectedValue = shipment.ServiceTypeId;
                if (shipment.ShipModeId != null && shipment.ShipModeId != Guid.Empty)
                    lstShipMode.SelectedValue = shipment.ShipModeId;
                if (shipment.GoodsDescriptionId != null && shipment.GoodsDescriptionId != Guid.Empty)
                    lstGoodsDescription.SelectedValue = shipment.GoodsDescriptionId;
                txtQuantity.Text = shipment.Quantity.ToString();
                txtWeight.Text = shipment.Weight.ToString("N");
                txtDeclaredValue.Text = shipment.DeclaredValueString;
                txtHandlingFee.Text = shipment.HandlingFeeString;
                txtQuarantineFee.Text = shipment.QuanrantineFeeString;
                txtRfa.Text = shipment.Discount.ToString("N");
                txtNotes.Text = shipment.Notes;
                chkNonVatable.Checked = false;
                if ((shipment.EvatId == null || shipment.EvatId == Guid.Empty) || shipment.EVat == null)
                    chkNonVatable.Checked = true;
            }
        }

        private void PopulateSummary()
        {
            txtSumChargeableWeight.Text = shipment.ChargeableWeightString;
            txtSumWeightCharge.Text = shipment.WeightChargeString;
            if (shipment.AwbFee != null)
            {
                txtSumAwbFee.Text = shipment.AwbFee.Amount.ToString("N");
            }
            else
            {
                txtSumAwbFee.Text = "0.00";
            }
            txtSumValuation.Text = "0.00";
            txtSumValuation.Text = shipment.ValuationAmountString;
            if (shipment.DeliveryFee != null)
                txtSumDeliveryFee.Text = shipment.DeliveryFee.AmountString;
            else
            {
                txtSumDeliveryFee.Text = "0.00";
            }
            if (shipment.FreightCollectCharge != null)
            {
                txtSumFreightCollect.Text = shipment.FreightCollectCharge.Amount.ToString("N");
            }
            else
            {
                txtSumFreightCollect.Text = "0.00";
            }
            if (shipment.PeracFee != null)
            {
                txtSumPeracFee.Text = shipment.PeracFee.Amount.ToString("N");
            }
            else
            {
                txtSumPeracFee.Text = "0.00";
            }
            if (shipment.DangerousFee != null)
            {
                txtSumDangerousFee.Text = shipment.DangerousFee.AmountString;
            }
            else
            {
                txtSumDangerousFee.Text = "0.00";
            }
            txtSumFuelSurcharge.Text = shipment.FuelSurchargeAmountstring;
            txtSumCratingFee.Text = shipment.CratingFeeString;
            txtSumDrainingFee.Text = shipment.DrainingFeeString;
            txtSumPackagingFee.Text = shipment.PackagingFeeString;
            txtSumHandlingFee.Text = shipment.HandlingFeeString;
            txtSumQuarantineFee.Text = shipment.QuanrantineFeeString;
            txtSumDiscount.Text = shipment.DiscountAmountString;
            if (shipment.Insurance != null)
            {
                txtSumInsurance.Text = shipment.InsuranceAmountString;
            }
            else
            {
                txtSumInsurance.Text = "0.00";
            }
            txtSumSubTotal.Text = shipment.ShipmentSubTotalString;
            txtSumVatAmount.Text = shipment.ShipmentVatAmountString;
            txtSumTotal.Text = shipment.ShipmentTotalString;
        }

        private void AddPackage()
        {
            if (shipment.PackageDimensions == null)
                shipment.PackageDimensions = new List<PackageDimensionModel>();

            if (shipment.CommodityTypeId == null || shipment.CommodityType == null)
            {
                lstCommodityType.Focus();
            }

            if (shipment.ServiceTypeId == null || shipment.ServiceType == null)
            {
                if (lstServiceType.SelectedValue == null)
                {
                    lstServiceType.SelectedIndex = 0;
                }
                shipment.ServiceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
                shipment.ServiceType = serviceTypes.Find(x => x.ServiceTypeId == shipment.ServiceTypeId);
            }

            if (shipment.ServiceModeId == null || shipment.ServiceMode == null)
            {
                if (lstServiceMode.SelectedValue == null)
                {
                    lstServiceMode.SelectedIndex = 0;
                }
                shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
                shipment.ServiceMode = serviceModes.Find(x => x.ServiceModeId == shipment.ServiceModeId);
            }

            if (shipment.ShipModeId == null || shipment.ShipMode == null)
            {
                if (lstShipMode.SelectedValue == null)
                {
                    lstShipMode.SelectedIndex = 0;
                }
                shipment.ShipModeId = Guid.Parse(lstShipMode.SelectedValue.ToString());
                shipment.ShipMode = shipModes.Find(x => x.ShipModeId == shipment.ShipModeId);
            }

            if (shipment.PaymentModeId == null || shipment.PaymentMode == null)
            {
                if (lstPaymentMode.SelectedValue == null)
                {
                    lstPaymentMode.SelectedIndex = 0;
                }
                shipment.PaymentModeId = Guid.Parse(lstPaymentMode.SelectedValue.ToString());
                shipment.PaymentMode = paymentModes.Find(x => x.PaymentModeId == shipment.PaymentModeId);
            }

            try
            {
                shipment.Quantity = Int32.Parse(txtQuantity.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
            }
            if (shipment.Quantity <= 0)
            {
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
            }
            try
            {
                shipment.Weight = Decimal.Parse(txtWeight.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
            }
            if (shipment.Weight <= 0)
            {
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
            }
            decimal length = 0;
            decimal width = 0;
            decimal height = 0;
            try
            {
                length = Decimal.Parse(txtLength.Text);
                width = Decimal.Parse(txtWidth.Text);
                height = Decimal.Parse(txtHeight.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Dimension.", "Data Error", MessageBoxButtons.OK);
            }

            int index = 0;
            for (index = 0; index < shipment.PackageDimensions.Count; index++)
            {
                var temp = shipment.PackageDimensions.Find(x => x.Index == index);
                if (temp == null)
                {
                    break;
                }
            }
            packageDimensionModel = new PackageDimensionModel();
            packageDimensionModel.Index = index;
            packageDimensionModel.Length = length;
            packageDimensionModel.Width = width;
            packageDimensionModel.Height = height;
            packageDimensionModel.CommodityTypeId = shipment.CommodityTypeId;
            packageDimensionModel.CratingId = null;
            if (lstCrating.SelectedValue != null)
            {
                packageDimensionModel.CratingId = Guid.Parse(lstCrating.SelectedValue.ToString());
                packageDimensionModel.CratingName = lstCrating.SelectedText;
            }
            packageDimensionModel.ForPackaging = chkPackaging.Checked;
            packageDimensionModel.ForDraining = chkDraining.Checked;
            shipment.PackageDimensions.Add(packageDimensionModel);
            gridPackage.Enabled = true;
            RefreshGridPackages();
            btnCompute.Enabled = true;

            txtLength.Text = "0";
            txtWidth.Text = "0";
            txtHeight.Text = "0";
            lstCrating.SelectedIndex = -1;
            chkPackaging.Checked = false;
            chkDraining.Checked = false;
        }

        // this removes a Package from grid
        private void gridPackage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView) sender;
            if (grid.CurrentCell.ColumnIndex == grid.Columns["colDelete"].Index)
            {
                if (grid.Columns["colDelete"] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    DataGridViewRow deletedRow = grid.Rows[e.RowIndex];
                    int index = Convert.ToInt32(deletedRow.Cells["colIndex"].Value);
                    shipment.PackageDimensions.Remove(shipment.PackageDimensions.FirstOrDefault(x => x.Index == index));
                    RefreshGridPackages();
                }
            }
        }

        // refresh the grid including EVM, CratingFee and WeightCharge computations
        private void RefreshGridPackages()
        {
            if (shipment.PackageDimensions != null)
            {
                //decimal totalWeightCharge = 0;
                decimal totalEvm = 0;
                if (shipment.PackageDimensions.Count > 0)
                {
                    shipment.WeightCharge = 0;
                    gridPackage.Rows.Clear();
                    int index = 0;
                    shipment = shipmentService.ComputePackageEvmCrating(shipment);
                    foreach (var item in shipment.PackageDimensions)
                    {
                        totalEvm = totalEvm + item.Evm;
                        gridPackage.Rows.Add();
                        gridPackage.Rows[index].Cells["colIndex"].Value = item.Index;
                        gridPackage.Rows[index].Cells["colLength"].Value = item.Length;
                        gridPackage.Rows[index].Cells["colWidth"].Value = item.Width;
                        gridPackage.Rows[index].Cells["colHeight"].Value = item.Height;
                        gridPackage.Rows[index].Cells["colCrating"].Value = item.CratingFeeString;
                        gridPackage.Rows[index].Cells["colPackaging"].Value = item.ForPackaging;
                        gridPackage.Rows[index].Cells["colDraining"].Value = item.DrainingFeeString;
                        index++;
                    }

                    shipment = shipmentService.ComputePackageWeightCharge(shipment);
                }
                else
                {
                    shipment.WeightCharge = 0;
                    totalEvm = 0;
                    gridPackage.Rows.Clear();
                }
                txtTotalEvm.Text = totalEvm.ToString("N");
                txtTotalWeightCharge.Text = shipment.WeightChargeString;
            }
        }

        #region ButtonEvents

        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            AddPackage();
        }

        private void btnAddPackage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddPackage();
            }
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            ComputeCharges();
            btnSave.Enabled = true;
        }

        private void btnCompute_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ComputeCharges();
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAirwayBill.Text))
            {
                txtAirwayBill.Focus();
                return;
            }
            else
            {
                shipment.AirwayBillNo = txtAirwayBill.Text.Trim();
            }

           btnReset.Enabled = false;
            btnCompute.Enabled = false;
            btnSave.Enabled = false;

            #region CaptureShipmentInput

            if (shipment.ShipmentId == null || shipment.ShipmentId == Guid.Empty)
            {
                shipment.ShipmentId = Guid.NewGuid();
            }

            shipment.CreatedDate = DateTime.Now;
            shipment.CreatedBy = AppUser.User.Id;
            shipment.LastPaymentDate = null;
            shipment.DateOfFullPayment = null;
            shipment.AcceptedAreaId = AppUser.EmployeeAssignment.AssignedLocationId;
            shipment.AcceptedArea = (RevenueUnit) AppUser.EmployeeAssignment.AssignedLocation;
            shipment.AcceptedById = AppUser.Employee.EmployeeId;
            shipment.AcceptedBy = AppUser.Employee;
            if (shipment.CommodityId == null || shipment.CommodityId == Guid.Empty)
                shipment.CommodityId = Guid.Parse(lstCommodity.SelectedValue.ToString());
            shipment.Notes = txtNotes.Text;
            shipment.GoodsDescriptionId = Guid.Parse(lstGoodsDescription.SelectedValue.ToString());

            //shipment.DestinationCityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
            shipment.ModifiedBy = AppUser.User.Id;
            shipment.ModifiedDate = DateTime.Now;
            shipment.RecordStatus = (int) RecordStatus.Active;

            // TOO this is default payment term
            shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("Cash")).PaymentTermId;
            if (shipment.PaymentMode.PaymentModeCode.Equals("PP"))
            {
                shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("Cash")).PaymentTermId;
            }
            else if (shipment.PaymentMode.PaymentModeCode.Equals("FC"))
            {
                shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("COD")).PaymentTermId;
            }
            else
            {
                if (shipment.Consignee.Company != null && shipment.Consignee.CompanyId != null)
                {
                    if (shipment.PaymentMode.PaymentModeCode.Equals("CAC"))
                    {
                        shipment.PaymentTermId = shipment.Consignee.Company.PaymentTerm.PaymentTermId;
                    }
                }

                if (shipment.Shipper.Company != null && shipment.Shipper.CompanyId != null)
                {
                    if (shipment.PaymentMode.PaymentModeCode.Equals("CAS"))
                    {
                        shipment.PaymentTermId = shipment.Shipper.Company.PaymentTerm.PaymentTermId;
                    }
                }
            }

            #region ShipmentPackages

            foreach (var item in shipment.PackageDimensions)
            {
                item.ShipmentId = shipment.ShipmentId;
                item.CreatedBy = AppUser.User.Id;
                item.CreatedDate = DateTime.Now;
                item.ModifiedBy = AppUser.User.Id;
                item.ModifiedDate = DateTime.Now;
                item.RecordStatus = (int) RecordStatus.Active;
            }

            #endregion

            #endregion

            ProgressIndicator saving = new ProgressIndicator("Acceptance", "Saving ...", SaveShipment);
            saving.ShowDialog();

            ResetAll();
            ClearSummaryData();
            DisableForm();
            shipmentModel = shipment;
            btnReset.Enabled = true;
            btnPrint.Enabled = true;
            btnSearchShipment.Enabled = true;

            PrintAwb();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintAwb();
        }

        private void PrintAwb()
        {
            btnReset.Enabled = false;
            btnCompute.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            #region SetDataSet

            ShipmentPrint shipmentPrint = new ShipmentPrint();
            DataRow dr = shipmentPrint.Tables["Shipment"].NewRow();
            dr["DateAccepted"] = shipmentModel.DateAccepted.ToString("MMM dd, yyyy h:mmtt");
            dr["BranchAccepted"] = shipmentModel.AcceptedArea.City.CityName;
            dr["Origin"] = shipmentModel.OriginCity.CityName;
            dr["Destination"] = shipmentModel.DestinationCity.CityName;
            dr["ShipperName"] = shipmentModel.Shipper.FullName;
            dr["ShipperAddress"] = shipmentModel.Shipper.Address1;
            dr["ConsigneeName"] = shipmentModel.Consignee.FullName;
            dr["ConsigneeAddress"] = shipmentModel.Consignee.Address1;
            dr["ServiceMode"] = shipmentModel.ServiceMode.ServiceModeName;
            dr["PaymentMode"] = shipmentModel.PaymentMode.PaymentModeCode;
            dr["DeclaredValue"] = shipmentModel.DeclaredValueString;
            dr["WeightCharge"] = shipmentModel.WeightChargeString;
            if (shipmentModel.AwbFee != null)
            {
                dr["AirwayBillFee"] = shipmentModel.AwbFee.AmountString;
            }
            else
            {
                dr["AirwayBillFee"] = "0.00";
            }
            dr["Valuation"] = shipmentModel.ValuationAmountString;
            if (shipmentModel.FreightCollectCharge != null)
            {
                dr["FreightCollect"] = shipmentModel.FreightCollectCharge.AmountString;
            }
            else
            {
                dr["FreightCollect"] = "0.00";
            }
            dr["Insurance"] = shipmentModel.InsuranceAmountString;
            dr["FuelSurcharge"] = shipmentModel.FuelSurchargeAmountstring;
            if (shipmentModel.DeliveryFee != null)
            {
                dr["DeliveryFee"] = shipmentModel.DeliveryFee.AmountString;
            }
            else
            {
                dr["DeliveryFee"] = "0.00";
            }
            if (shipmentModel.PeracFee != null)
            {
                dr["PeracFee"] = shipmentModel.PeracFee.AmountString;
            }
            else
            {
                dr["PeracFee"] = "0.00";
            }
            dr["CratingFee"] = "0.00";
            dr["SubTotal"] = shipmentModel.ShipmentSubTotalString;
            dr["VatAmount"] = shipmentModel.ShipmentVatAmountString;
            dr["Total"] = shipmentModel.ShipmentTotalString;
            dr["PreparedBy"] = shipmentModel.AcceptedBy.FullName;
            dr["AirwayBill"] = shipmentModel.AirwayBillNo;
            dr["Commodity"] = shipmentModel.CommodityType.CommodityTypeName;
            shipmentPrint.Tables["Shipment"].Rows.Add(dr);

            string dimension = "";
            foreach (var item in shipmentModel.PackageDimensions)
            {
                dimension = item.LengthString + " x " + item.WidthString + " x " + item.HeightString;
                dr = shipmentPrint.Tables["Packages"].NewRow();
                if (shipmentPrint.Tables["Packages"].Rows.Count == 0)
                {
                    dr["Quantity"] = shipmentModel.Quantity;
                    dr["ActualWeight"] = shipmentModel.Weight;
                }
                else
                {
                    dr["Quantity"] = "";
                    dr["ActualWeight"] = "";
                }
                dr["Dimension"] = dimension;
                dr["EVM"] = item.EvmString;
                dr["ChargeableWeight"] = item.WeightChargeString;
                shipmentPrint.Tables["Packages"].Rows.Add(dr);
            }

            #endregion

            try
            {
                PrinterSettings printer = new PrinterSettings();
                ReportDocument report = new ReportDocument();
                report.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\ShipmentPrintForm.rpt");
                report.SetDataSource(shipmentPrint);
                report.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                report.PrintOptions.PrinterName = printer.PrinterName;
                report.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    logs.ErrorLogs(LogPath, "Acceptance-PrintAwb", ex.Message);
                else
                    logs.ErrorLogs(LogPath, "Acceptance-PrintAwb", ex.InnerException.Message);
            }

            btnReset.Enabled = true;
            btnPayment.Enabled = true;
        }

        private void ProceedToPayment()
        {
            PaymentDetailsViewModel newPayment = new PaymentDetailsViewModel();
            newPayment.AwbSoa = txtAirwayBill.Text;
            newPayment.AmountPaid = decimal.Parse(txtSumTotal.Text);

            this.Hide();
            Panel mainPanel = (Panel) this.ParentForm.Controls.Find("panelMainContent", false)[0];
            UcPayment paymentTab = (UcPayment) mainPanel.Controls.Find("UcPayment", false)[0];
            paymentTab.NewPayment = newPayment;
            paymentTab.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearSummaryData();
            ResetAll();
        }

        #endregion

        #region ControlNavigation

        private void lstCommodityType_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void lstCommodityType_Validated(object sender, EventArgs e)
        {
            CommodityTypeSelected();
        }

        private void lstCommodityType_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    CommodityTypeSelected();
            //}
        }

        private void CommodityTypeSelected()
        {
            commodityType = new CommodityType();
            if (lstCommodityType.Items.Count > 0)
            {
                if (lstCommodityType.SelectedIndex>=0)
                {
                    Guid commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
                    commodityType = commodityTypes.Find(x => x.CommodityTypeId == commodityTypeId);
                    shipment.CommodityTypeId = commodityTypeId;
                    shipment.CommodityType = commodityTypes.Find(x => x.CommodityTypeId == commodityTypeId);

                    var _commodities =
                        commodities.Where(x => x.CommodityTypeId == commodityTypeId).OrderBy(x => x.CommodityName).ToList();
                    lstCommodity.DataSource = _commodities;
                    lstCommodity.DisplayMember = "CommodityName";
                    lstCommodity.ValueMember = "CommodityId";
                }
                else
                {
                    MessageBox.Show("No Commodity Type Selected", "Data Error", MessageBoxButtons.OK);
                    lstCommodityType.Focus();
                }
                
            }

            btnAddPackage.Enabled = true;

            gridPackage.ReadOnly = false;

            txtQuantity.Enabled = true;
            txtWeight.Enabled = true;
            txtLength.Enabled = true;
            txtWidth.Enabled = true;
            txtHeight.Enabled = true;
            btnAddPackage.Enabled = true;
            RefreshGridPackages();
            RefreshOptions();
            lstCommodity.Focus();
        }
        
        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                shipment.Weight = Convert.ToDecimal(txtWeight.Text.Trim());
                txtLength.Focus();
            }
        }

        private void txtLength_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWidth.Focus();
            }
        }

        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHeight.Focus();
            }
        }

        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddPackage.Focus();
            }
        }

        private void dateAcceptedDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAirwayBill.Focus();
            }
        }

        private void txtAirwayBill_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsNumericOnly(8, 8, txtAirwayBill.Text.Trim()))
                {
                    btnSearchShipment.Enabled = true;
                    lstCommodityType.Focus();
                    EnableForm();
                    lstCommodityType.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Invalid AirwayBill No.", "Data Error", MessageBoxButtons.OK);
                    txtAirwayBill.Focus();
                }

            }
        }

        private void txtDeclaredValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void chkNonVatable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        #endregion

        private void EnableForm()
        {
            lstCommodityType.Enabled = true;
            lstCommodity.Enabled = true;
            lstServiceType.Enabled = true;
            lstServiceMode.Enabled = true;
            lstShipMode.Enabled = true;
            txtQuantity.Enabled = true;
            txtWeight.Enabled = true;
            txtLength.Enabled = true;
            txtWidth.Enabled = true;
            txtHeight.Enabled = true;
            lstCrating.Enabled = true;
            chkPackaging.Enabled = true;
            chkDraining.Enabled = true;
            btnAddPackage.Enabled = true;
            gridPackage.Enabled = true;
            lstGoodsDescription.Enabled = true;
            lstPaymentMode.Enabled = true;
            txtDeclaredValue.Enabled = true;
            txtHandlingFee.Enabled = true;
            txtQuarantineFee.Enabled = true;
            txtRfa.Enabled = true;
            chkNonVatable.Enabled = false;
            txtNotes.Enabled = true;
        }

        private void DisableForm()
        {
            lstCommodityType.Enabled = false;
            lstCommodity.Enabled = false;
            lstServiceType.Enabled = false;
            lstServiceMode.Enabled = false;
            lstShipMode.Enabled = false;
            txtQuantity.Enabled = false;
            txtWeight.Enabled = false;
            txtLength.Enabled = false;
            txtWidth.Enabled = false;
            txtHeight.Enabled = false;
            lstCrating.Enabled = false;
            chkPackaging.Enabled = false;
            chkDraining.Enabled = false;
            btnAddPackage.Enabled = false;
            gridPackage.Enabled = false;
            lstGoodsDescription.Enabled = false;
            lstPaymentMode.Enabled = false;
            txtDeclaredValue.Enabled = false;
            txtHandlingFee.Enabled = false;
            txtQuarantineFee.Enabled = false;
            txtRfa.Enabled = false;
            btnCompute.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            chkNonVatable.Enabled = false;
            txtNotes.Enabled = false;
        }

        private void btnSearchShipment_Click(object sender, EventArgs e)
        {
            var _shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo.Equals(txtAirwayBill.Text.ToString()));
            if (_shipment != null)
            {
                shipment = shipmentService.EntityToModel(_shipment.FirstOrDefault());
                commodityType = commodityTypes.Find(x => x.CommodityTypeId == shipment.CommodityTypeId);

                PopulateForm();
                RefreshGridPackages();
                PopulateSummary();

                txtLength.Enabled = false;
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                btnAddPackage.Enabled = false;
                gridPackage.Enabled = false;

                btnReset.Enabled = true;
                btnCompute.Enabled = false;
                btnSave.Enabled = false;
                btnPrint.Enabled = true;
            }
        }

        private void ClearSummaryData()
        {
            txtSumChargeableWeight.Text = "0.00";
            txtSumWeightCharge.Text = "0.00";
            txtSumAwbFee.Text = "0.00";
            txtSumValuation.Text = "0.00";
            txtSumDeliveryFee.Text = "0.00";
            txtSumFreightCollect.Text = "0.00";
            txtSumPeracFee.Text = "0.00";
            txtSumFuelSurcharge.Text = "0.00";
            txtSumDangerousFee.Text = "0.00";
            txtSumCratingFee.Text = "0.00";
            txtSumDrainingFee.Text = "0.00";
            txtSumPackagingFee.Text = "0.00";
            txtSumHandlingFee.Text = "0.00";
            txtSumQuarantineFee.Text = "0.00";
            txtSumDiscount.Text = "0.00";
            txtSumInsurance.Text = "0.00";
            txtSumSubTotal.Text = "0.00";
            txtSumVatAmount.Text = "0.00";
            txtSumTotal.Text = "0.00";

        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (shipment.PaymentMode.PaymentModeCode.Equals("PP"))
            {
                btnPayment.Enabled = false;
                ProceedToPayment();
            }
        }

        private void lstServiceType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            shipment.ServiceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
            shipment.ServiceType = serviceTypes.FirstOrDefault(x => x.ServiceTypeId == shipment.ServiceTypeId);
            RefreshGridPackages();
            RefreshOptions();
        }

        private void lstServiceMode_Validated(object sender, EventArgs e)
        {
            shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
            shipment.ServiceMode = serviceModes.FirstOrDefault(x => x.ServiceModeId == shipment.ServiceTypeId);
            RefreshGridPackages();
            RefreshOptions();
        }

        private void txtWeight_Validated(object sender, EventArgs e)
        {
            RefreshGridPackages();
        }

        private void lstPaymentMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void lstPaymentMode_Validated(object sender, EventArgs e)
        {

            shipment.PaymentModeId = Guid.Parse(lstPaymentMode.SelectedValue.ToString());
            shipment.PaymentMode = paymentModes.FirstOrDefault(x => x.PaymentModeId == shipment.PaymentModeId);
        }
    
        private void lstShipMode_Validated(object sender, EventArgs e)
        {

        }

        private void lstServiceType_Validated(object sender, EventArgs e)
        {
            shipment.ShipModeId = Guid.Parse(lstShipMode.SelectedValue.ToString());
            shipment.ShipMode = shipModes.FirstOrDefault(x => x.ShipModeId == shipment.ShipModeId);
        }

        private void lstGoodsDescription_SelectionChangeCommitted(object sender, EventArgs e)
        {
            shipment.GoodsDescriptionId = Guid.Parse(lstGoodsDescription.SelectedValue.ToString());
            shipment.GoodsDescription =
                goodsDescriptions.FirstOrDefault(x => x.GoodsDescriptionId == shipment.GoodsDescriptionId);
        }

        private void RefreshOptions()
        {
            Guid commodityTypeId = new Guid();
            Guid commodityId = new Guid();
            Guid serviceTypeId = new Guid();
            Guid serviceModeId = new Guid();

            if (lstCommodityType.SelectedValue != null)
                commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
            //if (lstCommodity.SelectedValue != null)
            //    commodityId = Guid.Parse(lstCommoditySelectedValue.ToString());
            if (lstServiceType.SelectedValue != null)
                serviceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
            if (lstServiceMode.SelectedValue != null)
                serviceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());

            var matrix =
                rateMatrixService.FilterActiveBy(
                    x =>
                        x.CommodityTypeId == commodityTypeId && x.ServiceTypeId == serviceTypeId &&
                        x.ServiceModeId == serviceModeId).FirstOrDefault();

            if (matrix != null)
            {
                //shipment. = matrix.DeliveryFee;
                //shipment.DangerousFee = matrix.DangerousFee;
            }
        }

        private void lstCommodityType_Enter(object sender, EventArgs e)
        {
            commodityTypeCollection = new AutoCompleteStringCollection();
            foreach (
                var item in commodityTypes.OrderBy(x => x.CommodityTypeName).Select(x => x.CommodityTypeName).ToList())
            {
                commodityTypeCollection.Add(item);
            }
            lstCommodityType.AutoCompleteCustomSource = commodityTypeCollection;
        }

        private void lstCommodity_Enter(object sender, EventArgs e)
        {
            if (lstCommodityType.SelectedIndex > 0)
            {
                var commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
                commodityCollection = new AutoCompleteStringCollection();
                foreach (
                    var item in
                        commodities.Where(x => x.CommodityTypeId == commodityTypeId)
                            .OrderBy(x => x.CommodityName)
                            .Select(x => x.CommodityName)
                            .ToList())
                {
                    commodityCollection.Add(item);
                }
                lstCommodity.AutoCompleteCustomSource = commodityCollection;
            }

        }

        private void lstServiceType_Enter(object sender, EventArgs e)
        {
            serviceTypeCollection = new AutoCompleteStringCollection();
            foreach (var item in serviceTypes.OrderBy(x => x.ServiceTypeName).Select(x => x.ServiceTypeName).ToList())
            {
                serviceTypeCollection.Add(item);
            }
            lstServiceType.AutoCompleteCustomSource = serviceTypeCollection;
        }

        private void lstServiceMode_Enter(object sender, EventArgs e)
        {
            serviceModeCollection = new AutoCompleteStringCollection();
            foreach (var item in serviceModes.OrderBy(x => x.ServiceModeName).Select(x => x.ServiceModeName).ToList())
            {
                serviceModeCollection.Add(item);
            }
            lstServiceMode.AutoCompleteCustomSource = serviceModeCollection;
        }

        private void lstShipMode_Enter(object sender, EventArgs e)
        {
            shipModeCollection = new AutoCompleteStringCollection();
            foreach (var item in shipModes.OrderBy(x => x.ShipModeName).Select(x => x.ShipModeName).ToList())
            {
                shipModeCollection.Add(item);
            }
            lstShipMode.AutoCompleteCustomSource = shipModeCollection;
        }

        private void lstGoodsDescription_Enter(object sender, EventArgs e)
        {
            goodsDescCollection = new AutoCompleteStringCollection();
            foreach (
                var item in
                    goodsDescriptions.OrderBy(x => x.GoodsDescriptionName).Select(x => x.GoodsDescriptionName).ToList())
            {
                goodsDescCollection.Add(item);
            }
            lstGoodsDescription.AutoCompleteCustomSource = goodsDescCollection;
        }

        private void lstPaymentMode_Enter(object sender, EventArgs e)
        {
            paymentModeCollection = new AutoCompleteStringCollection();
            foreach (var item in paymentModes.OrderBy(x => x.PaymentModeName).Select(x => x.PaymentModeName).ToList())
            {
                paymentModeCollection.Add(item);
            }
            lstPaymentMode.AutoCompleteCustomSource = paymentModeCollection;
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
        
    }

}
