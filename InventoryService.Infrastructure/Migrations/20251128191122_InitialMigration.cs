using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LengthMm = table.Column<int>(type: "int", nullable: true),
                    WidthMm = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MaterialPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    PriceWithoutVAT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PriceWithVAT = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MaterialPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_MaterialPrices_M_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "M_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_MaterialStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    QuantityReserved = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MaterialStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_MaterialStocks_M_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "M_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_MaterialStocks_M_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "M_Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ReferenceDocument = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductionOrderId = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PerformedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_StockTransactions_M_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "M_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_StockTransactions_M_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "M_Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_MaterialPrices_MaterialId",
                table: "T_MaterialPrices",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_T_MaterialStocks_MaterialId",
                table: "T_MaterialStocks",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_T_MaterialStocks_WarehouseId",
                table: "T_MaterialStocks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_T_StockTransactions_MaterialId",
                table: "T_StockTransactions",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_T_StockTransactions_WarehouseId",
                table: "T_StockTransactions",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MaterialPrices");

            migrationBuilder.DropTable(
                name: "T_MaterialStocks");

            migrationBuilder.DropTable(
                name: "T_StockTransactions");

            migrationBuilder.DropTable(
                name: "M_Materials");

            migrationBuilder.DropTable(
                name: "M_Warehouses");
        }
    }
}
