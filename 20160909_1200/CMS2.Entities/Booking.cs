﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Booking : BaseEntity
    {
        [Key]
        public Guid BookingId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Booking No")]
        public string BookingNo { get; set; }
        [Required]
        public DateTime DateBooked { get; set; }
        public Guid BookedById { get; set; }
        [ForeignKey("BookedById")]
        public virtual Employee BookedBy { get; set; }
        public Guid ConsigneeId { get; set; }
        [ForeignKey("ConsigneeId")]
        public virtual Client Consignee { get; set; }
        public Guid ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        public virtual Client Shipper { get; set; }
        [Required]
        [MaxLength(250)]
        public string OriginAddress1 { get; set; }
        [MaxLength(250)]
        public string OriginAddress2 { get; set; }
        [MaxLength(250)]
        public string OriginStreet { get; set; }
        [MaxLength(150)]
        public string OriginBarangay { get; set; }
        [Required]
        [MaxLength(250)]
        public string DestinationAddress1 { get; set; }
        [MaxLength(250)]
        public string DestinationAddress2 { get; set; }
        [MaxLength(250)]
        public string DestinationStreet { get; set; }
        [MaxLength(150)]
        public string DestinationBarangay { get; set; }
        [MaxLength(400)]
        public string Remarks { get; set; }
        public Guid? BookingRemarkId { get; set; }
        [ForeignKey("BookingRemarkId")]
        public virtual BookingRemark BookingRemark { get; set; }
        public Guid BookingStatusId { get; set; }
        [ForeignKey("BookingStatusId")]
        public virtual BookingStatus BookingStatus { get; set; }
        [DefaultValue(0)]
        public bool HasDailyBooking { get; set; }

        [NotMapped]
        public string DateBookedString { get { return DateBooked.ToString("MMM dd, yyyy"); } }

        public Guid? PreviousBookingId { get; set; }
        [ForeignKey("PreviousBookingId")]
        public virtual Booking PreviousBooking { get; set; }

        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public virtual City OriginCity { get; set; }
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }
        public Guid AssignedToAreaId { get; set; }
        [ForeignKey("AssignedToAreaId")]
        public virtual RevenueUnit AssignedToArea { get; set; }
    }
}
