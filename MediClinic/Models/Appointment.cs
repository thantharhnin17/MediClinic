using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    public int AppointmentID { get; set; }

    [ForeignKey("Patient")]
    public int PatientID { get; set; }

    [ForeignKey("DoctorVisit")]
    public int VisitID { get; set; }

    public DateTime AppointmentDate { get; set; } = DateTime.Now;

    public Patient Patient { get; set; }
    public DoctorVisit DoctorVisit { get; set; }
}