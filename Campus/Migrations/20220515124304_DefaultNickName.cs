using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class DefaultNickName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_c_Identitys_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_c_Identitys",
                table: "c_Identitys");

            migrationBuilder.RenameTable(
                name: "c_Identitys",
                newName: "Identity");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "user_31BE16",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityValue",
                table: "Identity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Identity",
                table: "Identity",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Identity_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId",
                principalTable: "Identity",
                principalColumn: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Identity_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Identity",
                table: "Identity");

            migrationBuilder.RenameTable(
                name: "Identity",
                newName: "c_Identitys");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "user_31BE16");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityValue",
                table: "c_Identitys",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_c_Identitys",
                table: "c_Identitys",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_c_Identitys_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId",
                principalTable: "c_Identitys",
                principalColumn: "IdentityId");
        }
    }
}
