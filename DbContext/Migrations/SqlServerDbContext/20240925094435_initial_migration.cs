using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    AddressDbMAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.AttractionId);
                    table.ForeignKey(
                        name: "FK_Attractions_Addresses_AddressDbMAddressId",
                        column: x => x.AddressDbMAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttractionDbMAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserDbMUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Attractions_AttractionDbMAttractionId",
                        column: x => x.AttractionDbMAttractionId,
                        principalTable: "Attractions",
                        principalColumn: "AttractionId");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserDbMUserId",
                        column: x => x.UserDbMUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_AddressDbMAddressId",
                table: "Attractions",
                column: "AddressDbMAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AttractionDbMAttractionId",
                table: "Comments",
                column: "AttractionDbMAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserDbMUserId",
                table: "Comments",
                column: "UserDbMUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
