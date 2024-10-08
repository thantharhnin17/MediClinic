﻿using MediClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace MediClinic.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorVisit> DoctorVisits { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<DoctorVisit>().ToTable("DoctorVisit");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");

            modelBuilder.Entity<DoctorVisit>()
                .HasKey(c => new { c.DoctorID});

            modelBuilder.Entity<Appointment>()
                .HasKey(c => new { c.PatientID, c.VisitID });
        }
    }
}