using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class add_foreign_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses");

            migrationBuilder.DropIndex(
                name: "IX_UsersCourses_UserId",
                table: "UsersCourses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersCourses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersCourses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "UsersCourses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 583, DateTimeKind.Utc).AddTicks(1261),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 646, DateTimeKind.Utc).AddTicks(7630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 583, DateTimeKind.Utc).AddTicks(355),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 646, DateTimeKind.Utc).AddTicks(6976));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 562, DateTimeKind.Utc).AddTicks(6289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 619, DateTimeKind.Utc).AddTicks(8233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 554, DateTimeKind.Utc).AddTicks(5158),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 614, DateTimeKind.Utc).AddTicks(2465));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "UsersCourses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersCourses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersCourses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 646, DateTimeKind.Utc).AddTicks(7630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 583, DateTimeKind.Utc).AddTicks(1261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 646, DateTimeKind.Utc).AddTicks(6976),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 583, DateTimeKind.Utc).AddTicks(355));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 619, DateTimeKind.Utc).AddTicks(8233),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 562, DateTimeKind.Utc).AddTicks(6289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 30, 7, 32, 48, 614, DateTimeKind.Utc).AddTicks(2465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 30, 8, 33, 50, 554, DateTimeKind.Utc).AddTicks(5158));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCourses",
                table: "UsersCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCourses_UserId",
                table: "UsersCourses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_AspNetUsers_UserId",
                table: "UsersCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCourses_Courses_CourseId",
                table: "UsersCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
