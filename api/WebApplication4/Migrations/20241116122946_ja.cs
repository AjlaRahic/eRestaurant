using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class ja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Grad_GradId",
                table: "Korisnik");

            migrationBuilder.AlterColumn<int>(
                name: "GradId",
                table: "Korisnik",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Grad_GradId",
                table: "Korisnik",
                column: "GradId",
                principalTable: "Grad",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Grad_GradId",
                table: "Korisnik");

            migrationBuilder.AlterColumn<int>(
                name: "GradId",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Grad_GradId",
                table: "Korisnik",
                column: "GradId",
                principalTable: "Grad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
