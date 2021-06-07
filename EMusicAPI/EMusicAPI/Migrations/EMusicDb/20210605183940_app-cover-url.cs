using Microsoft.EntityFrameworkCore.Migrations;

namespace EMusicAPI.Migrations.EMusicDb
{
    public partial class appcoverurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Musics");
        }
    }
}
