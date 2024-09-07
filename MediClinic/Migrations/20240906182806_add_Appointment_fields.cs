using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediClinic.Migrations
{
    /// <inheritdoc />
    public partial class add_Appointment_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Appointment",
               columns: table => new
               {
                   PatientID = table.Column<int>(type: "int", nullable: false),
                   VisitID = table.Column<int>(type: "int", nullable: false),
                   AppointmentID = table.Column<int>(type: "int", nullable: false),
                   AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Appointment", x => new { x.PatientID, x.VisitID });
                   table.ForeignKey(
                       name: "FK_Appointment_DoctorVisit_VisitID",
                       column: x => x.VisitID,
                       principalTable: "DoctorVisit",
                       principalColumn: "DoctorID",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_Appointment_Patient_PatientID",
                       column: x => x.PatientID,
                       principalTable: "Patient",
                       principalColumn: "PatientID",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_VisitID",
                table: "Appointment",
                column: "VisitID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");
        }
    }
}
