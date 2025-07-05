using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Stream.Areas.Identity.Data;

#nullable disable

namespace Stream.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        private const string AdminRoleId = "26f87a0c-1d8d-4e59-9fdf-111111111111";
        private const string AdminUserId = "95cb5bf2-3d4a-4c5a-8f53-222222222222";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { AdminRoleId, "Admin", "ADMIN", Guid.NewGuid().ToString() });

            var user = new StreamUser
            {
                Id = AdminUserId,
                UserName = "admin@stream.com",
                NormalizedUserName = "ADMIN@STREAM.COM",
                Email = "admin@stream.com",
                NormalizedEmail = "ADMIN@STREAM.COM",
                EmailConfirmed = true,
                SecurityStamp = "00000000-0000-0000-0000-000000000002",
                ConcurrencyStamp = "00000000-0000-0000-0000-000000000003",
                PasswordHash = "AQAAAAIAAYagAAAAELiRGCG2l9VmOAXoJ1V8LojRxurx8WcN6iK3PaY5PQExampleHash==",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
                    "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                    "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount"
                },
                values: new object[]
                {
                    user.Id, user.UserName, user.NormalizedUserName, user.Email, user.NormalizedEmail,
                    user.EmailConfirmed, user.PasswordHash, user.SecurityStamp, user.ConcurrencyStamp,
                    user.PhoneNumber, user.PhoneNumberConfirmed, user.TwoFactorEnabled, user.LockoutEnd, user.LockoutEnabled, user.AccessFailedCount
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { AdminUserId, AdminRoleId });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { AdminUserId, AdminRoleId });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: AdminUserId);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: AdminRoleId);
        }
    }
}
