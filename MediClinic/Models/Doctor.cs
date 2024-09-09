using System;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int DoctorID { get; set; }

    [StringLength(255)]
    public string FullName { get; set; }

    [StringLength(20)]
    public string PhoneNo { get; set; }

    public string Address { get; set; }

    [StringLength(255)]
    public string Specialization { get; set; }

    public ICollection<DoctorVisit> DoctorVisits { get; set; }
}
