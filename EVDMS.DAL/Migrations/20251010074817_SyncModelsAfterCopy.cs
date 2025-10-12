using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelsAfterCopy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0002-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "admin_hash");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0002-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "evm_hash");
        }
    }
}
