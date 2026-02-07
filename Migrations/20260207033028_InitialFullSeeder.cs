using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class InitialFullSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruangans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Gedung = table.Column<string>(type: "TEXT", nullable: false),
                    Kapasitas = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruangans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peminjamans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RuanganId = table.Column<int>(type: "INTEGER", nullable: false),
                    TanggalPinjam = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TanggalSelesai = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjamans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Ruangans_RuanganId",
                        column: x => x.RuanganId,
                        principalTable: "Ruangans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ruangans",
                columns: new[] { "Id", "Gedung", "IsDeleted", "Kapasitas", "Nama" },
                values: new object[,]
                {
                    { 1, "D4", false, 30, "C.102" },
                    { 2, "D3", false, 40, "HH.201" },
                    { 3, "Pasca Sarjana", false, 20, "PS.2.10" },
                    { 4, "SAW", false, 15, "SAW.10.11" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nama", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin", "admin123", "Admin" },
                    { 2, "yere@example.com", "Yere", "yere123", "Mahasiswa" }
                });

            migrationBuilder.InsertData(
                table: "Peminjamans",
                columns: new[] { "Id", "RuanganId", "Status", "TanggalPinjam", "TanggalSelesai", "UserId" },
                values: new object[] { 1, 1, "Approved", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_RuanganId",
                table: "Peminjamans",
                column: "RuanganId");

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_UserId",
                table: "Peminjamans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjamans");

            migrationBuilder.DropTable(
                name: "Ruangans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
