using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class DefaultNickName5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Identity_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "Left(newId(),8)",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "user_Left(newId(),8)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "user_Left(newId(),8)",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "Left(newId(),8)");

            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Identity",
                columns: table => new
                {
                    IdentityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => x.IdentityId);
                });

            migrationBuilder.InsertData(
                table: "Identity",
                columns: new[] { "IdentityId", "IdentityValue" },
                values: new object[,]
                {
                    { 1, "无" },
                    { 2, "学生" },
                    { 3, "老师" },
                    { 4, "管理员" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Identity_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId",
                principalTable: "Identity",
                principalColumn: "IdentityId");
        }
    }
}
