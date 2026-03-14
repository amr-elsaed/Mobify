using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductOfferII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers");

            migrationBuilder.DropIndex(
                name: "IX_ProductOffers_ProductId",
                table: "ProductOffers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductOffers");

            migrationBuilder.DropColumn(
                name: "EndOfOffer",
                table: "ProductOffers");

            migrationBuilder.DropColumn(
                name: "OrigPrice",
                table: "ProductOffers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductOffers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfOffer",
                table: "ProductOffers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrigPrice",
                table: "ProductOffers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffers_ProductId",
                table: "ProductOffers",
                column: "ProductId",
                unique: true);
        }
    }
}
