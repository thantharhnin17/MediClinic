using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DoctorVisit
{
    [Key]
    public int VisitID { get; set; }

    [Required]
    [ForeignKey("Doctor")]
    public int DoctorID { get; set; }

    [Required]
    public DateTime VisitDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    public Doctor Doctor { get; set; }
}
