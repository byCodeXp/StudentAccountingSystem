using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class some_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 570, DateTimeKind.Utc).AddTicks(1750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 277, DateTimeKind.Utc).AddTicks(6184));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 566, DateTimeKind.Utc).AddTicks(4891),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 270, DateTimeKind.Utc).AddTicks(7094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(6466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(5622),
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 570, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 270, DateTimeKind.Utc).AddTicks(7094),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 566, DateTimeKind.Utc).AddTicks(4891));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "UserCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8698),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(6466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "UserCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 35, 18, 289, DateTimeKind.Utc).AddTicks(8001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(5622));

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
    }
}
