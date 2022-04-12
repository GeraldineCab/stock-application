using Microsoft.EntityFrameworkCore.Migrations;

namespace StockApplication.Persistence.Migrations
{
    public partial class AddNormalizedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "56baecc4-3422-494a-aedf-0057920f0b77", 0, "21980c1b-bfa7-48b6-9c2f-5e01ccd5858f", "josh@mail.com", true, false, false, null, null, "josh@mail.com", null, "J123osh", null, null, true, "6f58114e-5b2f-45fa-8b87-b09c4e4fe19c", false, "josh@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fd8cab70-ef33-4ed4-8f5d-5e0e7e693237", 0, "b639be6b-ef30-4c03-a156-e9c19a3eb3f6", "mary@mail.com", true, false, false, null, null, "mary@mail.com", null, "M123ary", null, null, true, "ed5a8cc9-e6a9-465e-91bf-a9aa1f228f48", false, "mary@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBot", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89bad659-9698-497d-8c35-f5c064e94011", 0, "3e0bab24-5014-4332-ab9b-e6e8dac03689", "bot@mail.com", true, false, false, null, null, null, null, "PasswordBot", null, null, true, "3491b750-9676-4f28-84bc-d03b169db3bd", false, "Bot" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56baecc4-3422-494a-aedf-0057920f0b77");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89bad659-9698-497d-8c35-f5c064e94011");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd8cab70-ef33-4ed4-8f5d-5e0e7e693237");

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
    }
}
