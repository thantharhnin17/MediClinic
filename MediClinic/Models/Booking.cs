using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key]
    public int BookingID { get; set; }

    [Required]
    [ForeignKey("Patient")]
    public int PatientID { get; set; }

    [Required]
    [ForeignKey("DoctorVisit")]
    public int VisitID { get; set; }

    [Required]
    public DateTime BookingDate { get; set; } = DateTime.Now;

    public Patient Patient { get; set; }
    public DoctorVisit DoctorVisit { get; set; }
}
