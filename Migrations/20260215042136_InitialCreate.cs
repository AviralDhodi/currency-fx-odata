using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyFxOData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyFXRates",
                columns: table => new
                {
                    UniqueNameDate = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UniqueName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    CurrencyFrom = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CurrencyTo = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    RateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    CorporateDailyInvoiceRate = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    CorporateDailyInvoiceInverseRate = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    CorporateMonthEndRate = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    CorporateMonthEndInverseRate = table.Column<decimal>(type: "numeric(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyFXRates", x => x.UniqueNameDate);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyFXRates_RateDate",
                table: "CurrencyFXRates",
                column: "RateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyFXRates");
        }
    }
}
