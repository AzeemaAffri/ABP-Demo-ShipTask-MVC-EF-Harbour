using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harbour.Migrations
{
    /// <inheritdoc />
    public partial class Added_ShipId_To_Fleet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShipId",
                table: "AppFleets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppFleets_ShipId",
                table: "AppFleets",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFleets_AppShips_ShipId",
                table: "AppFleets",
                column: "ShipId",
                principalTable: "AppShips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFleets_AppShips_ShipId",
                table: "AppFleets");

            migrationBuilder.DropIndex(
                name: "IX_AppFleets_ShipId",
                table: "AppFleets");

            migrationBuilder.DropColumn(
                name: "ShipId",
                table: "AppFleets");
        }
    }
}
