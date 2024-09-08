using System;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int DoctorID { get; set; }

    [Required]
    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [StringLength(20)]
    public string PhoneNo { get; set; }

    public string Address { get; set; }

    [Required]
    [StringLength(255)]
    public string Specialization { get; set; }

    public ICollection<DoctorVisit> DoctorVisits { get; set; }
}
