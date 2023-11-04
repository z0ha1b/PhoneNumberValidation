using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneValidator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "PhoneNumbers",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "PhoneNumbers",
                newName: "DefaultCountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "PhoneNumbers",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "DefaultCountryCode",
                table: "PhoneNumbers",
                newName: "CountryCode");
        }
    }
}
