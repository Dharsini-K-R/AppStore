using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppStore.Migrations
{
    public partial class ReUpdateAppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppList",
                table: "AppList");

            migrationBuilder.RenameTable(
                name: "AppList",
                newName: "AppModel");

            migrationBuilder.AddColumn<int>(
                name: "No_of_Downloads",
                table: "AppModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ratings",
                table: "AppModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppModel",
                table: "AppModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppModel",
                table: "AppModel");

            migrationBuilder.DropColumn(
                name: "No_of_Downloads",
                table: "AppModel");

            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "AppModel");

            migrationBuilder.RenameTable(
                name: "AppModel",
                newName: "AppList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppList",
                table: "AppList",
                column: "Id");
        }
    }
}
