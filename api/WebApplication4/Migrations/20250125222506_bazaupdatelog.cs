using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class bazaupdatelog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiNalog_korisnikId",
                table: "LogKretanjePoSistemu");

            migrationBuilder.RenameColumn(
                name: "korisnikId",
                table: "LogKretanjePoSistemu",
                newName: "KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_LogKretanjePoSistemu_korisnikId",
                table: "LogKretanjePoSistemu",
                newName: "IX_LogKretanjePoSistemu_KorisnikId");

            migrationBuilder.AlterColumn<string>(
                name: "QueryPath",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostData",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAdresa",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiNalog_KorisnikId",
                table: "LogKretanjePoSistemu",
                column: "KorisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiNalog_KorisnikId",
                table: "LogKretanjePoSistemu");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "LogKretanjePoSistemu",
                newName: "korisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_LogKretanjePoSistemu_KorisnikId",
                table: "LogKretanjePoSistemu",
                newName: "IX_LogKretanjePoSistemu_korisnikId");

            migrationBuilder.AlterColumn<string>(
                name: "QueryPath",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostData",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAdresa",
                table: "LogKretanjePoSistemu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LogKretanjePoSistemu_KorisnickiNalog_korisnikId",
                table: "LogKretanjePoSistemu",
                column: "korisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
