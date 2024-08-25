using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prods.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JournalId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_JournalId",
                table: "Products",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Journals_JournalId",
                table: "Products",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "JournalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Journals_JournalId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_JournalId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Products");
        }
    }
}
