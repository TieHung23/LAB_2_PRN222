using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0002-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0003-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0004-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0005-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0006-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0007-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0008-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0009-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-000a-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                columns: new[] { "Email", "HashedPassword" },
                values: new object[] { "admin@evdms.com", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0002-0000-0000-000000000000"),
                columns: new[] { "Email", "HashedPassword" },
                values: new object[] { "evmstaff@evdms.com", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "UserName");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0002-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0003-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0004-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0005-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0006-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0007-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0008-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0009-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000002-000a-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                columns: new[] { "HashedPassword", "UserName" },
                values: new object[] { "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.", "admin" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0002-0000-0000-000000000000"),
                columns: new[] { "HashedPassword", "UserName" },
                values: new object[] { "$2a$11$rDTmn7YPiwBqtZssLgFtquuGBlLPLTFKa2YzLrr9j7.rsMCdSulW.", "evmstaff" });
        }
    }
}
