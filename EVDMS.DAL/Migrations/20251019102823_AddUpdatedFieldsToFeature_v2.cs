using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedFieldsToFeature_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_VehicleModels_VehicleModelId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_VehicleConfigId",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_Features_VehicleModelId",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "VehicleModelId",
                table: "Features",
                newName: "UpdatedById");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Inventories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripsion",
                table: "Features",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Features",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAtTick",
                table: "Features",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FeatureVehicleModel",
                columns: table => new
                {
                    FeaturesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleModelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureVehicleModel", x => new { x.FeaturesId, x.VehicleModelsId });
                    table.ForeignKey(
                        name: "FK_FeatureVehicleModel_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureVehicleModel_VehicleModels_VehicleModelsId",
                        column: x => x.VehicleModelsId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleConfigId",
                table: "VehicleModels",
                column: "VehicleConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureVehicleModel_VehicleModelsId",
                table: "FeatureVehicleModel",
                column: "VehicleModelsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureVehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_VehicleConfigId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "UpdatedAtTick",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Features",
                newName: "VehicleModelId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descripsion",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleConfigId",
                table: "VehicleModels",
                column: "VehicleConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_VehicleModelId",
                table: "Features",
                column: "VehicleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_VehicleModels_VehicleModelId",
                table: "Features",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id");
        }
    }
}
