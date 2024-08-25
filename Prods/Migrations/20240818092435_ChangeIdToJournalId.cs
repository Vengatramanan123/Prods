using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prods.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdToJournalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Journals",
                newName: "JournalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalId",
                table: "Journals",
                newName: "Id");
        }
    }
}
