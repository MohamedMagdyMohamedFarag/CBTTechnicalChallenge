using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBTTechnicalChallenge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageCode);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ICNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LanguagePreference = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PrivacyPolicyAccepted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ICNumber);
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguagePreference",
                        column: x => x.LanguagePreference,
                        principalTable: "Languages",
                        principalColumn: "LanguageCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtpVerifications",
                columns: table => new
                {
                    OtpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OTPCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserICNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpVerifications", x => x.OtpID);
                    table.ForeignKey(
                        name: "FK_OtpVerifications_Users_UserICNumber_UserPhoneNumber_UserEmail",
                        columns: x => new { x.UserICNumber, x.UserPhoneNumber, x.UserEmail },
                        principalTable: "Users",
                        principalColumns: new[] { "ICNumber", "PhoneNumber", "Email" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageCode", "CreatedDate", "IsActive", "IsDefault", "LanguageName", "UpdatedDate" },
                values: new object[,]
                {
                        { "EN", new DateTime(2025, 1, 29, 16, 54, 31, 12, DateTimeKind.Utc).AddTicks(3797), true, true, "English", null },
                        { "MY", new DateTime(2025, 1, 29, 16, 54, 31, 12, DateTimeKind.Utc).AddTicks(3999), true, false, "Malay", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtpVerifications_UserICNumber_UserPhoneNumber_UserEmail",
                table: "OtpVerifications",
                columns: new[] { "UserICNumber", "UserPhoneNumber", "UserEmail" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ICNumber",
                table: "Users",
                column: "ICNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguagePreference",
                table: "Users",
                column: "LanguagePreference");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpVerifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
