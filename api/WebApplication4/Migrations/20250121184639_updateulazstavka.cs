using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class updateulazstavka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UlazStavka_MeniStavka_MeniId",
                table: "UlazStavka");

            migrationBuilder.DropForeignKey(
                name: "FK_UlazUSkladiste_Uposlenik_UposlenikId",
                table: "UlazUSkladiste");

            migrationBuilder.DropIndex(
                name: "IX_UlazUSkladiste_UposlenikId",
                table: "UlazUSkladiste");

            migrationBuilder.DropIndex(
                name: "IX_UlazStavka_MeniId",
                table: "UlazStavka");

            migrationBuilder.DropColumn(
                name: "UposlenikId",
                table: "UlazUSkladiste");

            migrationBuilder.DropColumn(
                name: "MeniId",
                table: "UlazStavka");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UposlenikId",
                table: "UlazUSkladiste",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeniId",
                table: "UlazStavka",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UlazUSkladiste_UposlenikId",
                table: "UlazUSkladiste",
                column: "UposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavka_MeniId",
                table: "UlazStavka",
                column: "MeniId");

            migrationBuilder.AddForeignKey(
                name: "FK_UlazStavka_MeniStavka_MeniId",
                table: "UlazStavka",
                column: "MeniId",
                principalTable: "MeniStavka",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UlazUSkladiste_Uposlenik_UposlenikId",
                table: "UlazUSkladiste",
                column: "UposlenikId",
                principalTable: "Uposlenik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
