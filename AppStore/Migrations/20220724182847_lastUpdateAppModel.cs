using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppStore.Migrations
{
    public partial class lastUpdateAppModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppModel",
                table: "AppModel");

            migrationBuilder.RenameTable(
                name: "AppModel",
                newName: "AppList");

            migrationBuilder.AlterColumn<int>(
                name: "Ratings",
                table: "AppList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "No_of_Downloads",
                table: "AppList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppList",
                table: "AppList",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppList",
                table: "AppList");

            migrationBuilder.RenameTable(
                name: "AppList",
                newName: "AppModel");

            migrationBuilder.AlterColumn<int>(
                name: "Ratings",
                table: "AppModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "No_of_Downloads",
                table: "AppModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppModel",
                table: "AppModel",
                column: "Id");
        }
    }
}
