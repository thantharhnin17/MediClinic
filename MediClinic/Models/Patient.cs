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

    [Required]
    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(20)]
    public string PhoneNo { get; set; }

    public string Address { get; set; }

    [Required]
    public Gender Gender { get; set; }
}
