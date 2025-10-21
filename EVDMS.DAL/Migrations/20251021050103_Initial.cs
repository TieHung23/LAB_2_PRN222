using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EVDMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripsion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PercentDiscount = table.Column<int>(type: "int", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VersionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteriorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WarrantyPeriod = table.Column<int>(type: "int", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    VehicleConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleConfigs_VehicleConfigId",
                        column: x => x.VehicleConfigId,
                        principalTable: "VehicleConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSale = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_VehicleModels_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestDrives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestDrives_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDrives_VehicleModels_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Payed = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAtTick = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedAt", "CreatedAtTick", "CreatedById", "Email", "FullName", "IdCardNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("00000003-0001-0000-0000-000000000000"), "1 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "customer1@email.com", "Customer 1", "123456789011", "987654321" },
                    { new Guid("00000003-0002-0000-0000-000000000000"), "2 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "customer2@email.com", "Customer 2", "123456789012", "987654322" },
                    { new Guid("00000003-0003-0000-0000-000000000000"), "3 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "customer3@email.com", "Customer 3", "123456789013", "987654323" },
                    { new Guid("00000003-0004-0000-0000-000000000000"), "4 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "customer4@email.com", "Customer 4", "123456789014", "987654324" },
                    { new Guid("00000003-0005-0000-0000-000000000000"), "5 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "customer5@email.com", "Customer 5", "123456789015", "987654325" }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "Address", "Code", "CreatedAt", "CreatedAtTick", "CreatedById", "Email", "IsActive", "IsDeleted", "Name", "PhoneNumber", "Region", "UpdatedAt", "UpdatedAtTick", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("00000001-0001-0000-0000-000000000000"), "1 Street", "DLR001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "dealer1@email.com", true, false, "Auto-1", "123456781", "North", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000001-0002-0000-0000-000000000000"), "2 Street", "DLR002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "dealer2@email.com", true, false, "Auto-2", "123456782", "North", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000001-0003-0000-0000-000000000000"), "3 Street", "DLR003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "dealer3@email.com", true, false, "Auto-3", "123456783", "North", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000001-0004-0000-0000-000000000000"), "4 Street", "DLR004", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "dealer4@email.com", true, false, "Auto-4", "123456784", "North", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000001-0005-0000-0000-000000000000"), "5 Street", "DLR005", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "dealer5@email.com", true, false, "Auto-5", "123456785", "North", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedAtTick", "CreatedById", "IsActive", "IsDeleted", "Name", "PercentDiscount" },
                values: new object[,]
                {
                    { new Guid("00000007-0001-0000-0000-000000000000"), "SALE1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Sale 1", 2 },
                    { new Guid("00000007-0002-0000-0000-000000000000"), "SALE2", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Sale 2", 4 },
                    { new Guid("00000007-0003-0000-0000-000000000000"), "SALE3", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Sale 3", 6 },
                    { new Guid("00000007-0004-0000-0000-000000000000"), "SALE4", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Sale 4", 8 },
                    { new Guid("00000007-0005-0000-0000-000000000000"), "SALE5", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Sale 5", 10 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedById", "Description", "Name", "UpdatedAt", "UpdatedAtTick", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Staff member of a dealership.", "Dealer Staff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Manager of a dealership.", "Dealer Manager", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Electric Vehicle Manufacturer staff.", "EVM Staff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "System Administrator.", "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "VehicleConfigs",
                columns: new[] { "Id", "BasePrice", "Color", "CreatedAt", "CreatedAtTick", "CreatedById", "InteriorType", "IsDeleted", "UpdatedAt", "UpdatedAtTick", "UpdatedById", "VersionName", "WarrantyPeriod" },
                values: new object[,]
                {
                    { new Guid("00000004-0001-0000-0000-000000000000"), 36000m, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Cloth", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard", 36 },
                    { new Guid("00000004-0002-0000-0000-000000000000"), 37000m, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Cloth", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard", 36 },
                    { new Guid("00000004-0003-0000-0000-000000000000"), 38000m, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Cloth", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard", 36 },
                    { new Guid("00000004-0004-0000-0000-000000000000"), 39000m, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Cloth", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard", 36 },
                    { new Guid("00000004-0005-0000-0000-000000000000"), 40000m, "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Cloth", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard", 36 },
                    { new Guid("00000004-0006-0000-0000-000000000000"), 46000m, "Red", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Leather", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0007-0000-0000-000000000000"), 47000m, "Blue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Premium Fabric", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0008-0000-0000-000000000000"), 48000m, "Silver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Suede", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0009-0000-0000-000000000000"), 49000m, "Gray", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Leather", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000a-0000-0000-000000000000"), 50000m, "Black", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Premium Fabric", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000b-0000-0000-000000000000"), 51000m, "Red", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Suede", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000c-0000-0000-000000000000"), 52000m, "Blue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Leather", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000d-0000-0000-000000000000"), 53000m, "Silver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Premium Fabric", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000e-0000-0000-000000000000"), 54000m, "Gray", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Suede", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-000f-0000-0000-000000000000"), 55000m, "Black", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Leather", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0010-0000-0000-000000000000"), 56000m, "Red", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Premium Fabric", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0011-0000-0000-000000000000"), 57000m, "Blue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Suede", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0012-0000-0000-000000000000"), 58000m, "Silver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Leather", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 },
                    { new Guid("00000004-0013-0000-0000-000000000000"), 59000m, "Gray", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Premium Fabric", false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Plus", 48 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedById", "DealerId", "Email", "FullName", "HashedPassword", "IsActive", "IsDeleted", "RoleId", "UpdatedAt", "UpdatedAtTick", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("00000002-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "manager1", "Manager 1", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0002-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "staff1", "Staff 1", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0003-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "manager2", "Manager 2", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0004-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "staff2", "Staff 2", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0005-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "manager3", "Manager 3", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0006-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "staff3", "Staff 3", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0007-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "manager4", "Manager 4", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0008-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "staff4", "Staff 4", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0009-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "manager5", "Manager 5", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-000a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "staff5", "Staff 5", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000064-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), null, "admin@evdms.com", "System Admin", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000064-0002-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), null, "evmstaff@evdms.com", "EVM Staff Member", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "Brand", "CreatedAt", "CreatedAtTick", "CreatedById", "Description", "ImgUrl", "IsActive", "IsDeleted", "ModelName", "ReleaseYear", "VehicleConfigId", "VehicleType" },
                values: new object[,]
                {
                    { new Guid("00000005-0001-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Description", "https://digitalassets.tesla.com/tesla-contents/image/upload/h_1800,w_2880,q_auto:best,f_auto,dpr_2.0/v1/content/dam/tesla/CAR_ASSETS/MODEL_S/U004_Paint_S_desktop.png", true, false, "Model 1", 2022, new Guid("00000004-0001-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0002-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Description", "https://digitalassets.tesla.com/tesla-contents/image/upload/h_1800,w_2880,q_auto:best,f_auto,dpr_2.0/v1/content/dam/tesla/CAR_ASSETS/MODEL_S/U004_Paint_S_desktop.png", true, false, "Model 2", 2022, new Guid("00000004-0002-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0003-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Description", "https://digitalassets.tesla.com/tesla-contents/image/upload/h_1800,w_2880,q_auto:best,f_auto,dpr_2.0/v1/content/dam/tesla/CAR_ASSETS/MODEL_S/U004_Paint_S_desktop.png", true, false, "Model 3", 2022, new Guid("00000004-0003-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0004-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Description", "https://digitalassets.tesla.com/tesla-contents/image/upload/h_1800,w_2880,q_auto:best,f_auto,dpr_2.0/v1/content/dam/tesla/CAR_ASSETS/MODEL_S/U004_Paint_S_desktop.png", true, false, "Model 4", 2022, new Guid("00000004-0004-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0005-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "Description", "https://digitalassets.tesla.com/tesla-contents/image/upload/h_1800,w_2880,q_auto:best,f_auto,dpr_2.0/v1/content/dam/tesla/CAR_ASSETS/MODEL_S/U004_Paint_S_desktop.png", true, false, "Model 5", 2022, new Guid("00000004-0005-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0006-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://shop.vinfastauto.com/on/demandware.static/-/Sites-app_vinfast_vn-Library/default/dw159f3640/images/v8/img-exterior.png", true, false, "VF 8", 2023, new Guid("00000004-0006-0000-0000-000000000000"), "Hatchback" },
                    { new Guid("00000005-0007-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://shop.vinfastauto.com/on/demandware.static/-/Sites-app_vinfast_vn-Library/default/dw053c0704/images/v9/img-exterior-v2.png", true, false, "VF 9", 2023, new Guid("00000004-0007-0000-0000-000000000000"), "Crossover" },
                    { new Guid("00000005-0008-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "VF e34", 2023, new Guid("00000004-0008-0000-0000-000000000000"), "SUV" },
                    { new Guid("00000005-0009-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "VF 5", 2023, new Guid("00000004-0009-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-000a-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "VF 6", 2023, new Guid("00000004-000a-0000-0000-000000000000"), "Hatchback" },
                    { new Guid("00000005-000b-0000-0000-000000000000"), "VinFast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from VinFast", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "VF 7", 2023, new Guid("00000004-000b-0000-0000-000000000000"), "Crossover" },
                    { new Guid("00000005-000c-0000-0000-000000000000"), "BMW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from BMW", "https://www.bmw.vn/content/dam/bmw/common/all-models/i-series/ix/2021/navigation/bmw-i-series-ix-modelfinder.png", true, false, "iX", 2023, new Guid("00000004-000c-0000-0000-000000000000"), "SUV" },
                    { new Guid("00000005-000d-0000-0000-000000000000"), "BMW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from BMW", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "i4", 2023, new Guid("00000004-000d-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-000e-0000-0000-000000000000"), "BMW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from BMW", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "i7", 2023, new Guid("00000004-000e-0000-0000-000000000000"), "Hatchback" },
                    { new Guid("00000005-000f-0000-0000-000000000000"), "BMW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from BMW", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "iX3", 2023, new Guid("00000004-000f-0000-0000-000000000000"), "Crossover" },
                    { new Guid("00000005-0010-0000-0000-000000000000"), "BMW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from BMW", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "iX1", 2023, new Guid("00000004-0010-0000-0000-000000000000"), "SUV" },
                    { new Guid("00000005-0011-0000-0000-000000000000"), "Hyundai", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from Hyundai", "https://media.hatvan.com/uploads/2021/05/hyundai-ioniq-5-ev-2022-1620958197.png", true, false, "Ioniq 5", 2023, new Guid("00000004-0011-0000-0000-000000000000"), "Sedan" },
                    { new Guid("00000005-0012-0000-0000-000000000000"), "Hyundai", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from Hyundai", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "Ioniq 6", 2023, new Guid("00000004-0012-0000-0000-000000000000"), "Hatchback" },
                    { new Guid("00000005-0013-0000-0000-000000000000"), "Hyundai", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), "A new electric car from Hyundai", "https://via.placeholder.com/600x400.png?text=No+Image", true, false, "Kona Electric", 2023, new Guid("00000004-0013-0000-0000-000000000000"), "Crossover" }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedById", "DealerId", "Description", "IsSale", "UpdatedAt", "UpdatedAtTick", "UpdatedById", "VehicleModelId" },
                values: new object[,]
                {
                    { new Guid("00000006-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "New arrival", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0001-0000-0000-000000000000") },
                    { new Guid("00000006-0002-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "New arrival", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0002-0000-0000-000000000000") },
                    { new Guid("00000006-0003-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "New arrival", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0003-0000-0000-000000000000") },
                    { new Guid("00000006-0004-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "New arrival", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0004-0000-0000-000000000000") },
                    { new Guid("00000006-0005-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "New arrival", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0005-0000-0000-000000000000") },
                    { new Guid("00000006-0006-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0006-0000-0000-000000000000") },
                    { new Guid("00000006-0007-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0006-0000-0000-000000000000") },
                    { new Guid("00000006-0008-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0006-0000-0000-000000000000") },
                    { new Guid("00000006-0009-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0006-0000-0000-000000000000") },
                    { new Guid("00000006-000a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0006-0000-0000-000000000000") },
                    { new Guid("00000006-000b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0007-0000-0000-000000000000") },
                    { new Guid("00000006-000c-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0007-0000-0000-000000000000") },
                    { new Guid("00000006-000d-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0007-0000-0000-000000000000") },
                    { new Guid("00000006-000e-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0007-0000-0000-000000000000") },
                    { new Guid("00000006-000f-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0007-0000-0000-000000000000") },
                    { new Guid("00000006-0010-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0008-0000-0000-000000000000") },
                    { new Guid("00000006-0011-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0008-0000-0000-000000000000") },
                    { new Guid("00000006-0012-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0008-0000-0000-000000000000") },
                    { new Guid("00000006-0013-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0008-0000-0000-000000000000") },
                    { new Guid("00000006-0014-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0008-0000-0000-000000000000") },
                    { new Guid("00000006-0015-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0009-0000-0000-000000000000") },
                    { new Guid("00000006-0016-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0009-0000-0000-000000000000") },
                    { new Guid("00000006-0017-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0009-0000-0000-000000000000") },
                    { new Guid("00000006-0018-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0009-0000-0000-000000000000") },
                    { new Guid("00000006-0019-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0009-0000-0000-000000000000") },
                    { new Guid("00000006-001a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000a-0000-0000-000000000000") },
                    { new Guid("00000006-001b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000a-0000-0000-000000000000") },
                    { new Guid("00000006-001c-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000a-0000-0000-000000000000") },
                    { new Guid("00000006-001d-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000a-0000-0000-000000000000") },
                    { new Guid("00000006-001e-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000a-0000-0000-000000000000") },
                    { new Guid("00000006-001f-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000b-0000-0000-000000000000") },
                    { new Guid("00000006-0020-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000b-0000-0000-000000000000") },
                    { new Guid("00000006-0021-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000b-0000-0000-000000000000") },
                    { new Guid("00000006-0022-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000b-0000-0000-000000000000") },
                    { new Guid("00000006-0023-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000b-0000-0000-000000000000") },
                    { new Guid("00000006-0024-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000c-0000-0000-000000000000") },
                    { new Guid("00000006-0025-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000c-0000-0000-000000000000") },
                    { new Guid("00000006-0026-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000c-0000-0000-000000000000") },
                    { new Guid("00000006-0027-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000c-0000-0000-000000000000") },
                    { new Guid("00000006-0028-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000c-0000-0000-000000000000") },
                    { new Guid("00000006-0029-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000d-0000-0000-000000000000") },
                    { new Guid("00000006-002a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000d-0000-0000-000000000000") },
                    { new Guid("00000006-002b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000d-0000-0000-000000000000") },
                    { new Guid("00000006-002c-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000d-0000-0000-000000000000") },
                    { new Guid("00000006-002d-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000d-0000-0000-000000000000") },
                    { new Guid("00000006-002e-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000e-0000-0000-000000000000") },
                    { new Guid("00000006-002f-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000e-0000-0000-000000000000") },
                    { new Guid("00000006-0030-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000e-0000-0000-000000000000") },
                    { new Guid("00000006-0031-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000e-0000-0000-000000000000") },
                    { new Guid("00000006-0032-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000e-0000-0000-000000000000") },
                    { new Guid("00000006-0033-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000f-0000-0000-000000000000") },
                    { new Guid("00000006-0034-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000f-0000-0000-000000000000") },
                    { new Guid("00000006-0035-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000f-0000-0000-000000000000") },
                    { new Guid("00000006-0036-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000f-0000-0000-000000000000") },
                    { new Guid("00000006-0037-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-000f-0000-0000-000000000000") },
                    { new Guid("00000006-0038-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0010-0000-0000-000000000000") },
                    { new Guid("00000006-0039-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0010-0000-0000-000000000000") },
                    { new Guid("00000006-003a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0010-0000-0000-000000000000") },
                    { new Guid("00000006-003b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0010-0000-0000-000000000000") },
                    { new Guid("00000006-003c-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0010-0000-0000-000000000000") },
                    { new Guid("00000006-003d-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0011-0000-0000-000000000000") },
                    { new Guid("00000006-003e-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0011-0000-0000-000000000000") },
                    { new Guid("00000006-003f-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0011-0000-0000-000000000000") },
                    { new Guid("00000006-0040-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0011-0000-0000-000000000000") },
                    { new Guid("00000006-0041-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0011-0000-0000-000000000000") },
                    { new Guid("00000006-0042-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0012-0000-0000-000000000000") },
                    { new Guid("00000006-0043-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0012-0000-0000-000000000000") },
                    { new Guid("00000006-0044-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0012-0000-0000-000000000000") },
                    { new Guid("00000006-0045-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0012-0000-0000-000000000000") },
                    { new Guid("00000006-0046-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0012-0000-0000-000000000000") },
                    { new Guid("00000006-0047-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0001-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0013-0000-0000-000000000000") },
                    { new Guid("00000006-0048-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0002-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0013-0000-0000-000000000000") },
                    { new Guid("00000006-0049-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0003-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0013-0000-0000-000000000000") },
                    { new Guid("00000006-004a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0004-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0013-0000-0000-000000000000") },
                    { new Guid("00000006-004b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0005-0000-0000-000000000000"), "Available for test drive", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000005-0013-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "TestDrives",
                columns: new[] { "Id", "CustomerId", "IsActive", "IsDeleted", "IsSuccess", "ScheduledDateTime", "VehicleModelId" },
                values: new object[,]
                {
                    { new Guid("0000000a-0001-0000-0000-000000000000"), new Guid("00000003-0001-0000-0000-000000000000"), true, false, false, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0001-0000-0000-000000000000") },
                    { new Guid("0000000a-0002-0000-0000-000000000000"), new Guid("00000003-0002-0000-0000-000000000000"), true, false, false, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0002-0000-0000-000000000000") },
                    { new Guid("0000000a-0003-0000-0000-000000000000"), new Guid("00000003-0003-0000-0000-000000000000"), true, false, false, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0003-0000-0000-000000000000") },
                    { new Guid("0000000a-0004-0000-0000-000000000000"), new Guid("00000003-0004-0000-0000-000000000000"), true, false, false, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0004-0000-0000-000000000000") },
                    { new Guid("0000000a-0005-0000-0000-000000000000"), new Guid("00000003-0005-0000-0000-000000000000"), true, false, false, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0005-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AccountId", "CreatedAt", "CreatedAtTick", "CreatedById", "CustomerId", "InventoryId", "PromotionId" },
                values: new object[,]
                {
                    { new Guid("00000008-0001-0000-0000-000000000000"), new Guid("00000002-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0001-0000-0000-000000000000"), new Guid("00000003-0001-0000-0000-000000000000"), new Guid("00000006-0001-0000-0000-000000000000"), new Guid("00000007-0001-0000-0000-000000000000") },
                    { new Guid("00000008-0002-0000-0000-000000000000"), new Guid("00000002-0003-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0003-0000-0000-000000000000"), new Guid("00000003-0002-0000-0000-000000000000"), new Guid("00000006-0002-0000-0000-000000000000"), new Guid("00000007-0002-0000-0000-000000000000") },
                    { new Guid("00000008-0003-0000-0000-000000000000"), new Guid("00000002-0005-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0005-0000-0000-000000000000"), new Guid("00000003-0003-0000-0000-000000000000"), new Guid("00000006-0003-0000-0000-000000000000"), new Guid("00000007-0003-0000-0000-000000000000") },
                    { new Guid("00000008-0004-0000-0000-000000000000"), new Guid("00000002-0007-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0007-0000-0000-000000000000"), new Guid("00000003-0004-0000-0000-000000000000"), new Guid("00000006-0004-0000-0000-000000000000"), new Guid("00000007-0004-0000-0000-000000000000") },
                    { new Guid("00000008-0005-0000-0000-000000000000"), new Guid("00000002-0009-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0009-0000-0000-000000000000"), new Guid("00000003-0005-0000-0000-000000000000"), new Guid("00000006-0005-0000-0000-000000000000"), new Guid("00000007-0005-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "BasePrice", "CreatedAt", "CreatedAtTick", "CreatedById", "DiscountPrice", "EndDate", "FinalPrice", "IsSuccess", "OrderId", "Payed", "PaymentMethod", "StartDate", "UpdatedAt", "UpdatedAtTick", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("00000009-0001-0000-0000-000000000000"), 36000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0001-0000-0000-000000000000"), 720m, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), 35280m, false, new Guid("00000008-0001-0000-0000-000000000000"), 1000m, "Card", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0001-0000-0000-000000000000") },
                    { new Guid("00000009-0002-0000-0000-000000000000"), 37000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0003-0000-0000-000000000000"), 1480m, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), 35520m, false, new Guid("00000008-0002-0000-0000-000000000000"), 1000m, "Card", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0003-0000-0000-000000000000") },
                    { new Guid("00000009-0003-0000-0000-000000000000"), 38000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0005-0000-0000-000000000000"), 2280m, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), 35720m, false, new Guid("00000008-0003-0000-0000-000000000000"), 1000m, "Card", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0005-0000-0000-000000000000") },
                    { new Guid("00000009-0004-0000-0000-000000000000"), 39000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0007-0000-0000-000000000000"), 3120m, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), 35880m, false, new Guid("00000008-0004-0000-0000-000000000000"), 1000m, "Card", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0007-0000-0000-000000000000") },
                    { new Guid("00000009-0005-0000-0000-000000000000"), 40000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0009-0000-0000-000000000000"), 4000m, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), 36000m, false, new Guid("00000008-0005-0000-0000-000000000000"), 1000m, "Card", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 638396640000000000L, new Guid("00000002-0009-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DealerId",
                table: "Accounts",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedbacks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_DealerId",
                table: "Inventories",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_VehicleModelId",
                table: "Inventories",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InventoryId",
                table: "Orders",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromotionId",
                table: "Orders",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_CustomerId",
                table: "TestDrives",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_VehicleModelId",
                table: "TestDrives",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleConfigId",
                table: "VehicleModels",
                column: "VehicleConfigId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TestDrives");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "VehicleConfigs");
        }
    }
}
