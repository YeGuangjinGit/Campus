using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus.Migrations
{
    public partial class updateChangeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "c_AccountInformationChange",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "c_AccountInformationChange");
        }
    }
}
