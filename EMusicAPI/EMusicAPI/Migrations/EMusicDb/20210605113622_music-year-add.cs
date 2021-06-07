using Microsoft.EntityFrameworkCore.Migrations;

namespace EMusicAPI.Migrations.EMusicDb
{
    public partial class musicyearadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Musics");
        }
    }
}
