using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediClinic.Migrations
{
    public partial class DropBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the Booking table
            migrationBuilder.DropTable(
                name: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate the Booking table in case of a rollback
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => new { x.PatientID, x.VisitID });
                    table.ForeignKey(
                        name: "FK_Booking_DoctorVisit_VisitID",
                        column: x => x.VisitID,
                        principalTable: "DoctorVisit",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_VisitID",
                table: "Booking",
                column: "VisitID");
        }
    }
}
