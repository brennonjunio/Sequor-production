using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sequor_production.Migrations
{
    public partial class migrationPrimary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    materialCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    materialDescription = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.materialCode);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    productCode = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cycleTime = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productCode);
                });

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    order = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    materialCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    cycleTime = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterial",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MaterialCode = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterial", x => new { x.ProductCode, x.MaterialCode });
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    initialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "ProductMaterial");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
