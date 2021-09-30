using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class changerelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscribes_AspNetUsers_UserId",
                table: "UserSubscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscribes_Courses_CourseId",
                table: "UserSubscribes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscribes",
                table: "UserSubscribes");

            migrationBuilder.RenameTable(
                name: "UserSubscribes",
                newName: "UserCourses");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubscribes_UserId",
                table: "UserCourses",
                newName: "IX_UserCourses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubscribes_CourseId",
                table: "UserCourses",
                newName: "IX_UserCourses_CourseId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 180, DateTimeKind.Utc).AddTicks(8353),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 786, DateTimeKind.Utc).AddTicks(2165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 180, DateTimeKind.Utc).AddTicks(7419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 786, DateTimeKind.Utc).AddTicks(1182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 152, DateTimeKind.Utc).AddTicks(7724),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 775, DateTimeKind.Utc).AddTicks(5646));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 139, DateTimeKind.Utc).AddTicks(7138),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 766, DateTimeKind.Utc).AddTicks(3470));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_UserId",
                table: "UserCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Courses_CourseId",
                table: "UserCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_UserId",
                table: "UserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Courses_CourseId",
                table: "UserCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses");

            migrationBuilder.RenameTable(
                name: "UserCourses",
                newName: "UserSubscribes");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourses_UserId",
                table: "UserSubscribes",
                newName: "IX_UserSubscribes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserSubscribes",
                newName: "IX_UserSubscribes_CourseId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 786, DateTimeKind.Utc).AddTicks(2165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 180, DateTimeKind.Utc).AddTicks(8353));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 786, DateTimeKind.Utc).AddTicks(1182),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 180, DateTimeKind.Utc).AddTicks(7419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 775, DateTimeKind.Utc).AddTicks(5646),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 152, DateTimeKind.Utc).AddTicks(7724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 25, 15, 54, 23, 766, DateTimeKind.Utc).AddTicks(3470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 29, 12, 30, 29, 139, DateTimeKind.Utc).AddTicks(7138));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscribes",
                table: "UserSubscribes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscribes_AspNetUsers_UserId",
                table: "UserSubscribes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscribes_Courses_CourseId",
                table: "UserSubscribes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
