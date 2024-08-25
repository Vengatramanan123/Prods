using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prods.Migrations
{
    /// <inheritdoc />
    public partial class AddJournalKeyToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JournalId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_JournalId",
                table: "Orders",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Journals_JournalId",
                table: "Orders",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Journals_JournalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_JournalId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Orders");
        }
    }
}
