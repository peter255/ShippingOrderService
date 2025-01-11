using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingOrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingOrderItems_ShippingOrders_ShippingOrderId",
                        column: x => x.ShippingOrderId,
                        principalTable: "ShippingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrderItems_ShippingOrderId",
                table: "ShippingOrderItems",
                column: "ShippingOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingOrderItems");

            migrationBuilder.DropTable(
                name: "ShippingOrders");
        }
    }
}
