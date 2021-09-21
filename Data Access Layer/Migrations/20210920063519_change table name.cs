using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class changetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCourse_Courses_CoursesId",
                table: "CategoryCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Courses_SubscribedCoursesId",
                table: "CourseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "UserCourses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 277, DateTimeKind.Utc).AddTicks(6184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 752, DateTimeKind.Utc).AddTicks(22));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 270, DateTimeKind.Utc).AddTicks(7094),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 744, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "UserCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8698),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 774, DateTimeKind.Utc).AddTicks(274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "UserCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 773, DateTimeKind.Utc).AddTicks(8824));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCourse_UserCourses_CoursesId",
                table: "CategoryCourse",
                column: "CoursesId",
                principalTable: "UserCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_UserCourses_SubscribedCoursesId",
                table: "CourseUser",
                column: "SubscribedCoursesId",
                principalTable: "UserCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCourse_UserCourses_CoursesId",
                table: "CategoryCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_UserCourses_SubscribedCoursesId",
                table: "CourseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses");

            migrationBuilder.RenameTable(
                name: "UserCourses",
                newName: "Courses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 752, DateTimeKind.Utc).AddTicks(22),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 277, DateTimeKind.Utc).AddTicks(6184));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 744, DateTimeKind.Utc).AddTicks(1454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 270, DateTimeKind.Utc).AddTicks(7094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 774, DateTimeKind.Utc).AddTicks(274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 12, 13, 28, 15, 773, DateTimeKind.Utc).AddTicks(8824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCourse_Courses_CoursesId",
                table: "CategoryCourse",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_Courses_SubscribedCoursesId",
                table: "CourseUser",
                column: "SubscribedCoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
