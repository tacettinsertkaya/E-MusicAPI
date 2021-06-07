using Microsoft.EntityFrameworkCore.Migrations;

namespace EMusicAPI.Migrations.EMusicDb
{
    public partial class initapp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isViewed",
                table: "UserMusic",
                newName: "IsViewed");

            migrationBuilder.RenameColumn(
                name: "isPurchashing",
                table: "UserMusic",
                newName: "IsPurchashing");

            migrationBuilder.RenameColumn(
                name: "isFavorite",
                table: "UserMusic",
                newName: "IsFavorite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsViewed",
                table: "UserMusic",
                newName: "isViewed");

            migrationBuilder.RenameColumn(
                name: "IsPurchashing",
                table: "UserMusic",
                newName: "isPurchashing");

            migrationBuilder.RenameColumn(
                name: "IsFavorite",
                table: "UserMusic",
                newName: "isFavorite");
        }
    }
}
