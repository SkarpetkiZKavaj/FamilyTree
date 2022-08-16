using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FTEntities.Migrations
{
    /// <inheritdoc />
    public partial class TreeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Descriptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonTree",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "integer", nullable: false),
                    TreesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTree", x => new { x.MembersId, x.TreesId });
                    table.ForeignKey(
                        name: "FK_PersonTree_Persons_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonTree_Trees_TreesId",
                        column: x => x.TreesId,
                        principalTable: "Trees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonTree_TreesId",
                table: "PersonTree",
                column: "TreesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonTree");

            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Descriptions");
        }
    }
}
