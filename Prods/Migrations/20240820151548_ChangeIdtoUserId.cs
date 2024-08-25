using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prods.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdtoUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LoginSignups",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LoginSignups",
                newName: "Id");
        }
    }
}
