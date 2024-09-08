using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediClinic.Migrations
{
    /// <inheritdoc />
    public partial class DropUserAndRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
            migrationBuilder.DropTable(
                name: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
