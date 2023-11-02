using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class Extend_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_c_AccountInformationChange_c_Users_UserId",
                table: "c_AccountInformationChange");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Activitys_c_Users_UserId",
                table: "c_Activitys");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Collections_c_Users_UserId",
                table: "c_Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Comments_c_Users_UserId",
                table: "c_Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Enrolls_c_Users_UserId",
                table: "c_Enrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Fabulous_c_Users_UserId",
                table: "c_Fabulous");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Follows_c_Users_TargetId",
                table: "c_Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Follows_c_Users_UserId",
                table: "c_Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Opinions_c_Users_HandleId",
                table: "c_Opinions");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Opinions_c_Users_UserId",
                table: "c_Opinions");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Works_c_Users_UserId",
                table: "c_Works");

            migrationBuilder.DropTable(
                name: "c_Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Works",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Opinions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "HandleId",
                table: "c_Opinions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Follows",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "TargetId",
                table: "c_Follows",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Fabulous",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Enrolls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Collections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Authentications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Handle",
                table: "c_Authentications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_Activitys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "c_AccountInformationChange",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "c_AccountInformationChange",
                type: "nvarchar(max)",
                nullable: true,
                defaultValueSql: "NewID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldDefaultValueSql: "NewID()");

            migrationBuilder.AddColumn<int>(
                name: "AuthenticationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GetDate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GetDate()");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "HeadPortrait",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalSignature",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AuthenticationId",
                table: "AspNetUsers",
                column: "AuthenticationId",
                unique: true,
                filter: "[AuthenticationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_c_Authentications_AuthenticationId",
                table: "AspNetUsers",
                column: "AuthenticationId",
                principalTable: "c_Authentications",
                principalColumn: "AuthenticationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_c_Genders_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "c_Genders",
                principalColumn: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_c_Identitys_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId",
                principalTable: "c_Identitys",
                principalColumn: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_AccountInformationChange_AspNetUsers_UserId",
                table: "c_AccountInformationChange",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Activitys_AspNetUsers_UserId",
                table: "c_Activitys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Collections_AspNetUsers_UserId",
                table: "c_Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Comments_AspNetUsers_UserId",
                table: "c_Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Enrolls_AspNetUsers_UserId",
                table: "c_Enrolls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Fabulous_AspNetUsers_UserId",
                table: "c_Fabulous",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Follows_AspNetUsers_TargetId",
                table: "c_Follows",
                column: "TargetId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Follows_AspNetUsers_UserId",
                table: "c_Follows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Opinions_AspNetUsers_HandleId",
                table: "c_Opinions",
                column: "HandleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_c_Opinions_AspNetUsers_UserId",
                table: "c_Opinions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Works_AspNetUsers_UserId",
                table: "c_Works",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_c_Authentications_AuthenticationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_c_Genders_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_c_Identitys_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_c_AccountInformationChange_AspNetUsers_UserId",
                table: "c_AccountInformationChange");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Activitys_AspNetUsers_UserId",
                table: "c_Activitys");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Collections_AspNetUsers_UserId",
                table: "c_Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Comments_AspNetUsers_UserId",
                table: "c_Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Enrolls_AspNetUsers_UserId",
                table: "c_Enrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Fabulous_AspNetUsers_UserId",
                table: "c_Fabulous");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Follows_AspNetUsers_TargetId",
                table: "c_Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Follows_AspNetUsers_UserId",
                table: "c_Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Opinions_AspNetUsers_HandleId",
                table: "c_Opinions");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Opinions_AspNetUsers_UserId",
                table: "c_Opinions");

            migrationBuilder.DropForeignKey(
                name: "FK_c_Works_AspNetUsers_UserId",
                table: "c_Works");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AuthenticationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthenticationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HeadPortrait",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonalSignature",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Works",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Opinions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "HandleId",
                table: "c_Opinions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Follows",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetId",
                table: "c_Follows",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Fabulous",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Enrolls",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Collections",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Authentications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Handle",
                table: "c_Authentications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_Activitys",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "c_AccountInformationChange",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Code",
                table: "c_AccountInformationChange",
                type: "uniqueidentifier",
                nullable: true,
                defaultValueSql: "NewID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValueSql: "NewID()");

            migrationBuilder.CreateTable(
                name: "c_Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NewID()"),
                    AuthenticationId = table.Column<int>(type: "int", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IdentityId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "NewID()"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()"),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    HeadPortrait = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PersonalSignature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_c_Users_c_Authentications_AuthenticationId",
                        column: x => x.AuthenticationId,
                        principalTable: "c_Authentications",
                        principalColumn: "AuthenticationId");
                    table.ForeignKey(
                        name: "FK_c_Users_c_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "c_Genders",
                        principalColumn: "GenderId");
                    table.ForeignKey(
                        name: "FK_c_Users_c_Identitys_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "c_Identitys",
                        principalColumn: "IdentityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_c_Users_AuthenticationId",
                table: "c_Users",
                column: "AuthenticationId",
                unique: true,
                filter: "[AuthenticationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_c_Users_GenderId",
                table: "c_Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Users_IdentityId",
                table: "c_Users",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_AccountInformationChange_c_Users_UserId",
                table: "c_AccountInformationChange",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Activitys_c_Users_UserId",
                table: "c_Activitys",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Collections_c_Users_UserId",
                table: "c_Collections",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Comments_c_Users_UserId",
                table: "c_Comments",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Enrolls_c_Users_UserId",
                table: "c_Enrolls",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Fabulous_c_Users_UserId",
                table: "c_Fabulous",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Follows_c_Users_TargetId",
                table: "c_Follows",
                column: "TargetId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Follows_c_Users_UserId",
                table: "c_Follows",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Opinions_c_Users_HandleId",
                table: "c_Opinions",
                column: "HandleId",
                principalTable: "c_Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_c_Opinions_c_Users_UserId",
                table: "c_Opinions",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Works_c_Users_UserId",
                table: "c_Works",
                column: "UserId",
                principalTable: "c_Users",
                principalColumn: "UserId");
        }
    }
}
