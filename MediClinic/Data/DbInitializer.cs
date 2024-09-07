using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediClinic.Models;

namespace MediClinic.Data
{
    public class DbInitializer
    {
        public static void Initialize(ClinicContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Patients.Any())
            {
                return;   // DB has been seeded
            }

            var patients = new Patient[]
            {
                new Patient
                {FullName = "John Doe",DateOfBirth = new DateTime(1985, 6, 15),PhoneNo = "123-456-7890",Address = "123 Main St, Anytown, USA",Gender = Gender.Male},
                new Patient
                {FullName = "Jane Smith",DateOfBirth = new DateTime(1990, 8, 25),PhoneNo = "987-654-3210",Address = "456 Oak Ave, Somewhere, USA",Gender = Gender.Female},
                new Patient
                {FullName = "Alex Taylor",DateOfBirth = new DateTime(1995, 3, 10),PhoneNo = "555-555-5555",Address = "789 Pine Rd, Anywhere, USA",Gender = Gender.Other}
            };
            foreach (Patient p in patients)
            {
                context.Patients.Add(p);
            }
            context.SaveChanges();

            var doctors = new Doctor[]
            {
                new Doctor
                {
                    FullName = "Dr. Alice Johnson",
                    PhoneNo = "111-222-3333",
                    Address = "101 Maple St, Big City, USA",
                    Specialization = "Cancer"
                },
                new Doctor
                {
                    FullName = "Dr. Bob Williams",
                    PhoneNo = "444-555-6666",
                    Address = "202 Birch St, Metropolis, USA",
                    Specialization = "Heart"
                },
                new Doctor
                {
                    FullName = "Dr. Carol Davis",
                    PhoneNo = "777-888-9999",
                    Address = "303 Cedar St, Smalltown, USA",
                    Specialization = "OG"
                }
            };
            foreach (Doctor d in doctors)
            {
                context.Doctors.Add(d);
            }
            context.SaveChanges();

            var doctorVisits = new DoctorVisit[]
            {
               new DoctorVisit
                {
                    DoctorID = doctors.Single(d => d.FullName == "Dr. Alice Johnson").DoctorID,
                    VisitDate = new DateTime(2024, 9, 10),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 0, 0)
                },
                new DoctorVisit
                {
                    DoctorID = doctors.Single(d => d.FullName == "Dr. Bob Williams").DoctorID,
                    VisitDate = new DateTime(2024, 9, 11),
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0)
                },
                new DoctorVisit
                {
                    DoctorID = doctors.Single(d => d.FullName == "Dr. Carol Davis").DoctorID,
                    VisitDate = new DateTime(2024, 9, 12),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(15, 0, 0)
                }
            };
            foreach (DoctorVisit dv in doctorVisits)
            {
                context.DoctorVisits.Add(dv);
            }
            context.SaveChanges();

            
        }
    }
}
