using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_aplikace.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzivatele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezdivka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Heslo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DatumRegistrace = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzivatele", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uzivatele_Prezdivka",
                table: "Uzivatele",
                column: "Prezdivka",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uzivatele");
        }
    }
}
