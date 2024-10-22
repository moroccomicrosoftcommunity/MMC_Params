using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParamsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Cities_CityId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_CityId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Participants");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Participants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ParticipantEvent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ParticipantEvent");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CityId",
                table: "Participants",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Cities_CityId",
                table: "Participants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
