using Microsoft.EntityFrameworkCore.Migrations;

namespace StockApplication.Persistence.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c83b0b7b-7729-42e5-b7cc-7ee823124f38", 0, "3dba2016-db48-4afd-8f56-c61ecb01bb1c", "josh@mail.com", true, false, false, null, null, null, null, "J123osh", null, null, true, "90f33f05-582e-4c8b-a85f-367a17d99a61", false, "josh@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "27afc50e-1486-4479-bff0-797a11b98aac", 0, "a8371c83-22b6-49ee-a619-60bf12614245", "mary@mail.com", true, false, false, null, null, null, null, "M123ary", null, null, true, "bcc8d104-416c-4546-b721-f93a5694a4ad", false, "mary@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "43f447fc-b688-45cc-a6d0-e649a8b176a9", 0, "39c62e48-1ce6-4cf6-a779-e2e0634b7c9d", "bot@mail.com", true, false, false, null, null, null, null, "PasswordBot", null, null, true, "1f317e91-311d-4a5b-917c-ac3331d9122b", false, "Bot" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27afc50e-1486-4479-bff0-797a11b98aac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43f447fc-b688-45cc-a6d0-e649a8b176a9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c83b0b7b-7729-42e5-b7cc-7ee823124f38");
        }
    }
}
