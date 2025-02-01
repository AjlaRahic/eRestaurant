using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DnevniMeni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DnevniMeni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DostavnoVozilo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DostavnoVozilo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusNarudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNarudzbe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restorani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadnoVrijemeRadnimDanima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadnoVrijemeVikendom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restorani_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeniStavka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Izdvojeno = table.Column<bool>(type: "bit", nullable: false),
                    SnizenaCijena = table.Column<float>(type: "real", nullable: false),
                    Ocjena = table.Column<float>(type: "real", nullable: false),
                    KategorijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeniStavka_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_KorisnickiNalog_Id",
                        column: x => x.Id,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickiNalogId = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnik_KorisnickiNalog_Id",
                        column: x => x.Id,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogKretanjePoSistemu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikId = table.Column<int>(type: "int", nullable: false),
                    QueryPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isException = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogKretanjePoSistemu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogKretanjePoSistemu_KorisnickiNalog_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uposlenik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObavljeneNarudzbe = table.Column<int>(type: "int", nullable: false),
                    AktivneNarudzbe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uposlenik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uposlenik_KorisnickiNalog_Id",
                        column: x => x.Id,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KorpaStavke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeniId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    KorpaId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorpaStavke_MeniStavka_MeniId",
                        column: x => x.MeniId,
                        principalTable: "MeniStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ponuda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DostupnaKolicina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeniId = table.Column<int>(type: "int", nullable: false),
                    DnevniMeniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponuda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ponuda_DnevniMeni_DnevniMeniId",
                        column: x => x.DnevniMeniId,
                        principalTable: "DnevniMeni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ponuda_MeniStavka_MeniId",
                        column: x => x.MeniId,
                        principalTable: "MeniStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlineGostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnik_OnlineGostId",
                        column: x => x.OnlineGostId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OmiljenaStavka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    MeniStavkaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OmiljenaStavka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OmiljenaStavka_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OmiljenaStavka_MeniStavka_MeniStavkaID",
                        column: x => x.MeniStavkaID,
                        principalTable: "MeniStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false),
                    BrojStolova = table.Column<int>(type: "int", nullable: false),
                    Obavljena = table.Column<bool>(type: "bit", nullable: false),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    KategorijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dostava",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestoranId = table.Column<int>(type: "int", nullable: false),
                    UposlenikId = table.Column<int>(type: "int", nullable: false),
                    DostavnoVoziloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dostava_DostavnoVozilo_DostavnoVoziloId",
                        column: x => x.DostavnoVoziloId,
                        principalTable: "DostavnoVozilo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dostava_Restorani_RestoranId",
                        column: x => x.RestoranId,
                        principalTable: "Restorani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dostava_Uposlenik_UposlenikId",
                        column: x => x.UposlenikId,
                        principalTable: "Uposlenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineNarudzba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    DatumNarucivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Zakljucena = table.Column<bool>(type: "bit", nullable: false),
                    BrojStavki = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    StatusNarudzbeID = table.Column<int>(type: "int", nullable: true),
                    UposlenikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineNarudzba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OnlineNarudzba_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineNarudzba_StatusNarudzbe_StatusNarudzbeID",
                        column: x => x.StatusNarudzbeID,
                        principalTable: "StatusNarudzbe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OnlineNarudzba_Uposlenik_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UlazUSkladiste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    KolicinaUlaza = table.Column<float>(type: "real", nullable: false),
                    ImeDobavljaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPrijema = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UposlenikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlazUSkladiste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UlazUSkladiste_Uposlenik_UposlenikId",
                        column: x => x.UposlenikId,
                        principalTable: "Uposlenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UposlenikObavijesti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UposlenikId = table.Column<int>(type: "int", nullable: false),
                    ObavijestiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UposlenikObavijesti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UposlenikObavijesti_Obavijesti_ObavijestiId",
                        column: x => x.ObavijestiId,
                        principalTable: "Obavijesti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UposlenikObavijesti_Uposlenik_UposlenikId",
                        column: x => x.UposlenikId,
                        principalTable: "Uposlenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    MeniStavkaId = table.Column<int>(type: "int", nullable: false),
                    Iznos = table.Column<float>(type: "real", nullable: false),
                    OnlineNarudzbaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Narudzbe_MeniStavka_MeniStavkaId",
                        column: x => x.MeniStavkaId,
                        principalTable: "MeniStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzbe_OnlineNarudzba_OnlineNarudzbaId",
                        column: x => x.OnlineNarudzbaId,
                        principalTable: "OnlineNarudzba",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UlazStavka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UlazUSkaldisteId = table.Column<int>(type: "int", nullable: false),
                    UlazUSkladisteId = table.Column<int>(type: "int", nullable: false),
                    MeniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlazStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UlazStavka_MeniStavka_MeniId",
                        column: x => x.MeniId,
                        principalTable: "MeniStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlazStavka_UlazUSkladiste_UlazUSkaldisteId",
                        column: x => x.UlazUSkaldisteId,
                        principalTable: "UlazUSkladiste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnickiNalogId",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_DostavnoVoziloId",
                table: "Dostava",
                column: "DostavnoVoziloId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_RestoranId",
                table: "Dostava",
                column: "RestoranId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_UposlenikId",
                table: "Dostava",
                column: "UposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_GradId",
                table: "Korisnik",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaStavke_MeniId",
                table: "KorpaStavke",
                column: "MeniId");

            migrationBuilder.CreateIndex(
                name: "IX_LogKretanjePoSistemu_korisnikId",
                table: "LogKretanjePoSistemu",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MeniStavka_KategorijaId",
                table: "MeniStavka",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_MeniStavkaId",
                table: "Narudzbe",
                column: "MeniStavkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_OnlineNarudzbaId",
                table: "Narudzbe",
                column: "OnlineNarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_OnlineGostId",
                table: "Notifikacije",
                column: "OnlineGostId");

            migrationBuilder.CreateIndex(
                name: "IX_OmiljenaStavka_KorisnikID",
                table: "OmiljenaStavka",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_OmiljenaStavka_MeniStavkaID",
                table: "OmiljenaStavka",
                column: "MeniStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineNarudzba_KorisnikID",
                table: "OnlineNarudzba",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineNarudzba_StatusNarudzbeID",
                table: "OnlineNarudzba",
                column: "StatusNarudzbeID");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineNarudzba_UposlenikID",
                table: "OnlineNarudzba",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Ponuda_DnevniMeniId",
                table: "Ponuda",
                column: "DnevniMeniId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponuda_MeniId",
                table: "Ponuda",
                column: "MeniId");

            migrationBuilder.CreateIndex(
                name: "IX_Restorani_GradId",
                table: "Restorani",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KategorijaId",
                table: "Rezervacija",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikId",
                table: "Rezervacija",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavka_MeniId",
                table: "UlazStavka",
                column: "MeniId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazStavka_UlazUSkaldisteId",
                table: "UlazStavka",
                column: "UlazUSkaldisteId");

            migrationBuilder.CreateIndex(
                name: "IX_UlazUSkladiste_UposlenikId",
                table: "UlazUSkladiste",
                column: "UposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_UposlenikObavijesti_ObavijestiId",
                table: "UposlenikObavijesti",
                column: "ObavijestiId");

            migrationBuilder.CreateIndex(
                name: "IX_UposlenikObavijesti_UposlenikId",
                table: "UposlenikObavijesti",
                column: "UposlenikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");

            migrationBuilder.DropTable(
                name: "Dostava");

            migrationBuilder.DropTable(
                name: "KorpaStavke");

            migrationBuilder.DropTable(
                name: "LogKretanjePoSistemu");

            migrationBuilder.DropTable(
                name: "Narudzbe");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropTable(
                name: "OmiljenaStavka");

            migrationBuilder.DropTable(
                name: "Ponuda");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "UlazStavka");

            migrationBuilder.DropTable(
                name: "UposlenikObavijesti");

            migrationBuilder.DropTable(
                name: "DostavnoVozilo");

            migrationBuilder.DropTable(
                name: "Restorani");

            migrationBuilder.DropTable(
                name: "OnlineNarudzba");

            migrationBuilder.DropTable(
                name: "DnevniMeni");

            migrationBuilder.DropTable(
                name: "MeniStavka");

            migrationBuilder.DropTable(
                name: "UlazUSkladiste");

            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "StatusNarudzbe");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Uposlenik");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");
        }
    }
}
