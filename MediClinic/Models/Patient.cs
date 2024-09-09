using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public enum Gender
{
    Male,
    Female,
    Other
}

public class Patient
{
    [Key]
    public int PatientID { get; set; }

    [StringLength(255)]
    public string FullName { get; set; }

    public DateTime DateOfBirth { get; set; }

    [StringLength(20)]
    public string PhoneNo { get; set; }

    public string Address { get; set; }

    public Gender Gender { get; set; }
}
