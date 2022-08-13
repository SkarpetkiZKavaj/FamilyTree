using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FTEntities.Migrations
{
    /// <inheritdoc />
    public partial class OwnerProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Persons");
        }
    }
}
