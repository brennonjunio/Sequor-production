using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sequor_production.Migrations
{
    public partial class MigrationOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table =>
                    new
                    {
                        materialCode = table.Column<string>(type: "varchar(50)", nullable: false),
                        materialDescription = table.Column<string>(
                            type: "varchar(500)",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.materialCode);
                }
            );

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table =>
                    new
                    {
                        productCode = table.Column<string>(type: "varchar(50)", nullable: false),
                        productDescription = table.Column<string>(
                            type: "varchar(50)",
                            nullable: false
                        ),
                        image = table.Column<string>(type: "varchar(500)", nullable: false),
                        cycleTime = table.Column<decimal>(
                            type: "numeric(18,2)",
                            nullable: false,
                            defaultValue: 18.2m
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productCode);
                }
            );

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<long>(type: "BIGINT", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        email = table.Column<string>(type: "varchar(100)", nullable: false),
                        order = table.Column<string>(type: "varchar(50)", nullable: false),
                        date = table.Column<DateTime>(type: "Datetime2", nullable: false),
                        quantity = table.Column<decimal>(
                            type: "decimal(18,2)",
                            nullable: false,
                            defaultValue: 18.2m
                        ),
                        materialCode = table.Column<string>(type: "varchar(50)", nullable: false),
                        cycleTime = table.Column<decimal>(
                            type: "decimal(18,2)",
                            nullable: false,
                            defaultValue: 18.2m
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.id);
                    table.ForeignKey(
                        name: "FK_Production_Material_materialCode",
                        column: x => x.materialCode,
                        principalTable: "Material",
                        principalColumn: "materialCode",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductMaterial",
                columns: table =>
                    new
                    {
                        productCode = table.Column<string>(type: "varchar(50)", nullable: false),
                        materialCode = table.Column<string>(type: "varchar(50)", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ProductMaterial",
                        x => new { x.productCode, x.materialCode }
                    );
                    table.ForeignKey(
                        name: "FK_ProductMaterial_Product_productCode",
                        column: x => x.productCode,
                        principalTable: "Product",
                        principalColumn: "productCode",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ProductMaterial_Material_materialCode",
                        column: x => x.materialCode,
                        principalTable: "Material",
                        principalColumn: "materialCode",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "User",
                columns: table =>
                    new
                    {
                        email = table.Column<string>(type: "varchar(100)", nullable: false),
                        name = table.Column<string>(type: "varchar(50)", nullable: false),
                        initialDate = table.Column<DateTime>(type: "Datetime2", nullable: false),
                        endDate = table.Column<DateTime>(type: "Datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.email);
                }
            );

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table =>
                    new
                    {
                        order = table.Column<string>(type: "varchar(50)", nullable: false),
                        quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                        productCode = table.Column<string>(type: "varchar(50)", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order);
                    table.ForeignKey(
                        name: "FK_Order_Product_productCode",
                        column: x => x.productCode,
                        principalTable: "Product",
                        principalColumn: "productCode",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Order_productCode",
                table: "Order",
                column: "productCode"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Production_materialCode",
                table: "Production",
                column: "materialCode"
            );

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "materialCode", "materialDescription" },
                values: new object[,]
                {
                    { "M001", "Aço" },
                    { "M002", "Alumínio" },
                    { "M003", "Plástico" },
                    { "M004", "Couro" },
                    { "M005", "Tecido Sintético" },
                    { "M006", "Borracha" },
                    { "M007", "Vidro Temperado" },
                    { "M008", "Fibra de Carbono" },
                    { "M009", "Metal Cromado" },
                    { "M010", "Polímero" }
                }
            );

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "productCode", "productDescription", "image", "cycleTime" },
                values: new object[,]
                {
                    { "P001", "Moto Esportiva", "moto_esportiva.jpg", 50.0 },
                    { "P002", "Scooter", "scooter.jpg", 18.5 },
                    { "P003", "Bicicleta Elétrica", "bicicleta_eletrica.jpg", 45.0 },
                    { "P004", "Skate Elétrico", "skate_eletrico.jpg", 18.0 },
                    { "P005", "Capacete de Couro", "capacete.jpg", 20.9 },
                    { "P006", "Luvas de Motociclista", "luvas.jpg", 55.0 },
                    { "P007", "Óculos de Proteção", "oculos.jpg", 29.3 },
                    { "P008", "Jaqueta de Motociclista", "jaqueta.jpg", 50.2 },
                    { "P009", "Protetor de Joelho", "protetor_joelho.jpg", 30.5 },
                    { "P010", "Mochila para Motociclistas", "mochila.jpg", 40.0 }
                }
            );

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "order", "quantity", "productCode" },
                values: new object[,]
                {
                    { "ORD001", 10, "P001" },
                    { "ORD002", 5, "P002" },
                    { "ORD003", 8, "P003" },
                    { "ORD004", 15, "P001" },
                    { "ORD005", 7, "P002" },
                    { "ORD006", 12, "P003" },
                    { "ORD007", 6, "P001" },
                    { "ORD008", 9, "P002" },
                    { "ORD009", 3, "P003" },
                    { "ORD010", 20, "P001" },
                }
            );

            migrationBuilder.InsertData(
                table: "ProductMaterial",
                columns: new[] { "productCode", "materialCode" },
                values: new object[,]
                {
                    { "P001", "M001" },
                    { "P001", "M002" },
                    { "P002", "M003" },
                    { "P003", "M002" },
                    { "P003", "M003" },
                    { "P003", "M005" },
                    { "P004", "M003" },
                    { "P005", "M004" },
                    { "P006", "M006" },
                    { "P008", "M004" },
                    { "P009", "M006" },
                    { "P010", "M003" },
                    { "P010", "M005" }
                }
            );

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "email", "name", "initialDate", "endDate" },
                values: new object[,]
                {
                                   { "user1@example.com", "John Doe", new DateTime(2023, 1, 1), new DateTime(2023, 12, 31) },
                { "user2@example.com", "Alice Smith", new DateTime(2023, 2, 15), new DateTime(2023, 12, 31) },
                { "user3@example.com", "Bob Johnson", new DateTime(2023, 3, 10), new DateTime(2023, 6, 30) },
                { "user4@example.com", "Eva Brown", new DateTime(2023, 4, 5), new DateTime(2023, 12, 31) },
                { "user5@example.com", "Chris Wilson", new DateTime(2023, 5, 20), new DateTime(2023, 8, 15) },
                { "user6@example.com", "Sophie Davis", new DateTime(2023, 6, 3), new DateTime(2023, 7, 10) },
                { "user7@example.com", "Daniel White", new DateTime(2023, 7, 12), new DateTime(2023, 11, 28) },
                { "user8@example.com", "Emily Green", new DateTime(2023, 8, 30), new DateTime(2024, 1, 5) },
                { "user9@example.com", "Ryan Miller", new DateTime(2023, 9, 9), new DateTime(2023, 12, 31) },
                { "user10@example.com", "Olivia Taylor", new DateTime(2023, 10, 22), new DateTime(2023, 12, 15) },
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Order");

            migrationBuilder.DropTable(name: "Production");

            migrationBuilder.DropTable(name: "ProductMaterial");

            migrationBuilder.DropTable(name: "User");

            migrationBuilder.DropTable(name: "Product");

            migrationBuilder.DropTable(name: "Material");
        }
    }
}
