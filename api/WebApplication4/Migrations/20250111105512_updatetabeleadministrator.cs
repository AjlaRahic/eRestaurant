using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class updatetabeleadministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GradId",
                table: "Administrator",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_GradId",
                table: "Administrator",
                column: "GradId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Grad_GradId",
                table: "Administrator",
                column: "GradId",
                principalTable: "Grad",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Grad_GradId",
                table: "Administrator");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_GradId",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "GradId",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Administrator");
        }
    }
}
