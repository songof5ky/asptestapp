using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestApp.Migrations
{
    public partial class azx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErHstr_Errors_errid",
                table: "ErHstr");

            migrationBuilder.RenameColumn(
                name: "errid",
                table: "ErHstr",
                newName: "errId");

            migrationBuilder.RenameIndex(
                name: "IX_ErHstr_errid",
                table: "ErHstr",
                newName: "IX_ErHstr_errId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErHstr_Errors_errId",
                table: "ErHstr",
                column: "errId",
                principalTable: "Errors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErHstr_Errors_errId",
                table: "ErHstr");

            migrationBuilder.RenameColumn(
                name: "errId",
                table: "ErHstr",
                newName: "errid");

            migrationBuilder.RenameIndex(
                name: "IX_ErHstr_errId",
                table: "ErHstr",
                newName: "IX_ErHstr_errid");

            migrationBuilder.AddForeignKey(
                name: "FK_ErHstr_Errors_errid",
                table: "ErHstr",
                column: "errid",
                principalTable: "Errors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
