using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediClinic.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert example data
            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "PatientID", "VisitID", "AppointmentID", "AppointmentDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 9, 10, 10, 0, 0) },
                    { 2, 2, 2, new DateTime(2024, 9, 11, 11, 0, 0) },
                    { 3, 3, 3, new DateTime(2024, 9, 12, 12, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete example data
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumns: new[] { "PatientID", "VisitID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumns: new[] { "PatientID", "VisitID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumns: new[] { "PatientID", "VisitID" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
