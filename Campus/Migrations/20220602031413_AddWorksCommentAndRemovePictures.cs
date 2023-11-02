using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class AddWorksCommentAndRemovePictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "WorksId",
                table: "c_Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_c_Comments_WorksId",
                table: "c_Comments",
                column: "WorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_c_Comments_c_Works_WorksId",
                table: "c_Comments",
                column: "WorksId",
                principalTable: "c_Works",
                principalColumn: "WorksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_c_Comments_c_Works_WorksId",
                table: "c_Comments");

            migrationBuilder.DropIndex(
                name: "IX_c_Comments_WorksId",
                table: "c_Comments");

            migrationBuilder.AlterColumn<int>(
                name: "WorksId",
                table: "c_Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "c_Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorksId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Pictures", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_c_Pictures_c_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "c_Works",
                        principalColumn: "WorksId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_c_Pictures_WorksId",
                table: "c_Pictures",
                column: "WorksId");
        }
    }
}
