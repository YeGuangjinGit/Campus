using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_Authentications",
                columns: table => new
                {
                    AuthenticationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    IsPass = table.Column<bool>(type: "bit", nullable: true),
                    Handle = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Authentications", x => x.AuthenticationId);
                });

            migrationBuilder.CreateTable(
                name: "c_Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderValue = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "c_Identitys",
                columns: table => new
                {
                    IdentityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityValue = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Identitys", x => x.IdentityId);
                });

            migrationBuilder.CreateTable(
                name: "c_SpecialColumns",
                columns: table => new
                {
                    SpecialColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialColumnValue = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_SpecialColumns", x => x.SpecialColumnId);
                });

            migrationBuilder.CreateTable(
                name: "c_Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NewID()"),
                    UserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    HeadPortrait = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PersonalSignature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()"),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetDate()"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "NewID()"),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IdentityId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AuthenticationId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "c_AccountInformationChange",
                columns: table => new
                {
                    AccountChangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "NewID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_AccountInformationChange", x => x.AccountChangeId);
                    table.ForeignKey(
                        name: "FK_c_AccountInformationChange_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Activitys",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActivityLocale = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    ActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Activitys", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_c_Activitys_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Parentid = table.Column<int>(type: "int", nullable: false),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    Like = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_c_Comments_c_Comments_Parentid",
                        column: x => x.Parentid,
                        principalTable: "c_Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_c_Comments_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Follows",
                columns: table => new
                {
                    FollowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Follows", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_c_Follows_c_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_c_Follows_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Opinions",
                columns: table => new
                {
                    OpinionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpinionTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OpinionContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    HandleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Opinions", x => x.OpinionId);
                    table.ForeignKey(
                        name: "FK_c_Opinions_c_Users_HandleId",
                        column: x => x.HandleId,
                        principalTable: "c_Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_c_Opinions_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Works",
                columns: table => new
                {
                    WorksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    SpecialColumnId = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Works", x => x.WorksId);
                    table.ForeignKey(
                        name: "FK_c_Works_c_SpecialColumns_SpecialColumnId",
                        column: x => x.SpecialColumnId,
                        principalTable: "c_SpecialColumns",
                        principalColumn: "SpecialColumnId");
                    table.ForeignKey(
                        name: "FK_c_Works_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Enrolls",
                columns: table => new
                {
                    EnrolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Participate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Enrolls", x => x.EnrolId);
                    table.ForeignKey(
                        name: "FK_c_Enrolls_c_Activitys_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "c_Activitys",
                        principalColumn: "ActivityId");
                    table.ForeignKey(
                        name: "FK_c_Enrolls_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "c_Collections",
                columns: table => new
                {
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Collections", x => x.CollectionId);
                    table.ForeignKey(
                        name: "FK_c_Collections_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_c_Collections_c_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "c_Works",
                        principalColumn: "WorksId");
                });

            migrationBuilder.CreateTable(
                name: "c_Fabulous",
                columns: table => new
                {
                    FabulousId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Fabulous", x => x.FabulousId);
                    table.ForeignKey(
                        name: "FK_c_Fabulous_c_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "c_Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_c_Fabulous_c_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "c_Works",
                        principalColumn: "WorksId");
                });

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

            migrationBuilder.InsertData(
                table: "c_Genders",
                columns: new[] { "GenderId", "GenderValue" },
                values: new object[,]
                {
                    { 1, "保密" },
                    { 2, "男" },
                    { 3, "女" }
                });

            migrationBuilder.InsertData(
                table: "c_Identitys",
                columns: new[] { "IdentityId", "IdentityValue" },
                values: new object[,]
                {
                    { 1, "无" },
                    { 2, "学生" },
                    { 3, "老师" },
                    { 4, "管理员" }
                });

            migrationBuilder.InsertData(
                table: "c_SpecialColumns",
                columns: new[] { "SpecialColumnId", "SpecialColumnValue" },
                values: new object[,]
                {
                    { 1, "校园天地" },
                    { 2, "学生论坛" },
                    { 3, "社团活动" },
                    { 4, "校园热卖" },
                    { 5, "校园防疫" },
                    { 6, "校园活动" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_c_AccountInformationChange_UserId",
                table: "c_AccountInformationChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Activitys_UserId",
                table: "c_Activitys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Collections_UserId",
                table: "c_Collections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Collections_WorksId",
                table: "c_Collections",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Comments_Parentid",
                table: "c_Comments",
                column: "Parentid");

            migrationBuilder.CreateIndex(
                name: "IX_c_Comments_UserId",
                table: "c_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Enrolls_ActivityId",
                table: "c_Enrolls",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Enrolls_UserId",
                table: "c_Enrolls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Fabulous_UserId",
                table: "c_Fabulous",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Fabulous_WorksId",
                table: "c_Fabulous",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Follows_TargetId",
                table: "c_Follows",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Follows_UserId",
                table: "c_Follows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Opinions_HandleId",
                table: "c_Opinions",
                column: "HandleId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Opinions_UserId",
                table: "c_Opinions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Pictures_WorksId",
                table: "c_Pictures",
                column: "WorksId");

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

            migrationBuilder.CreateIndex(
                name: "IX_c_Works_SpecialColumnId",
                table: "c_Works",
                column: "SpecialColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_c_Works_UserId",
                table: "c_Works",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_AccountInformationChange");

            migrationBuilder.DropTable(
                name: "c_Collections");

            migrationBuilder.DropTable(
                name: "c_Comments");

            migrationBuilder.DropTable(
                name: "c_Enrolls");

            migrationBuilder.DropTable(
                name: "c_Fabulous");

            migrationBuilder.DropTable(
                name: "c_Follows");

            migrationBuilder.DropTable(
                name: "c_Opinions");

            migrationBuilder.DropTable(
                name: "c_Pictures");

            migrationBuilder.DropTable(
                name: "c_Activitys");

            migrationBuilder.DropTable(
                name: "c_Works");

            migrationBuilder.DropTable(
                name: "c_SpecialColumns");

            migrationBuilder.DropTable(
                name: "c_Users");

            migrationBuilder.DropTable(
                name: "c_Authentications");

            migrationBuilder.DropTable(
                name: "c_Genders");

            migrationBuilder.DropTable(
                name: "c_Identitys");
        }
    }
}
