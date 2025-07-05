using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Stream.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { "3813f1fa-d858-40f8-bab8-6096f87ce55e", "Admin", "ADMIN", "c387b3b5-0210-4fd4-94f1-f61572d9405b" },
                    { "1ddb9194-e946-4977-9c12-91bd40b8e564", "User", "USER", "602d7bfc-7f4b-4f94-9a3a-e9141d25e4c6" }
                });

            // Insert admin user
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
                    "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                    "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount"
                },
                values: new object[]
                {
                    "724cc0e3-47d9-4b29-8cf8-9d7c4c9a7277",
                    "admin@stream.com",
                    "ADMIN@STREAM.COM",
                    "admin@stream.com",
                    "ADMIN@STREAM.COM",
                    false,
                    "AQAAAAEAACcQAAAAEAAAACCq4y53vamPjABEVFhd1L+uT7jl/TvmjoPyer9h17rS1rbSUjkxT1QQ3R8jNgb9P0U=",
                    "2f919a1c-c522-4cf3-96ff-98e4a54f1e36",
                    "4cd43a21-5f2b-4fa7-ab2e-97678e4c8d99",
                    false,
                    false,
                    true,
                    0
                });

            // Assign admin role to admin user
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "724cc0e3-47d9-4b29-8cf8-9d7c4c9a7277", "3813f1fa-d858-40f8-bab8-6096f87ce55e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "724cc0e3-47d9-4b29-8cf8-9d7c4c9a7277", "3813f1fa-d858-40f8-bab8-6096f87ce55e" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "724cc0e3-47d9-4b29-8cf8-9d7c4c9a7277");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3813f1fa-d858-40f8-bab8-6096f87ce55e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ddb9194-e946-4977-9c12-91bd40b8e564");
        }

        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
        }
    }
}
