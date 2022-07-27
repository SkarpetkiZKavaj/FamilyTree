using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class RenamedProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Persons",
                newName: "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Persons",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Persons",
                newName: "age");
        }
    }
}
