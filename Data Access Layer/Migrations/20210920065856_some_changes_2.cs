using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class some_changes_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_AspNetUsers_SubscribedUsersId",
                table: "CourseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Courses_SubscribedCoursesId",
                table: "CourseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUser",
                table: "CourseUser");

            migrationBuilder.RenameTable(
                name: "CourseUser",
                newName: "UserCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUser_SubscribedUsersId",
                table: "UserCourses",
                newName: "IX_UserCourses_SubscribedUsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(5121),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(6466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(4505),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(5622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 402, DateTimeKind.Utc).AddTicks(3786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 570, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 398, DateTimeKind.Utc).AddTicks(5917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 566, DateTimeKind.Utc).AddTicks(4891));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses",
                columns: new[] { "SubscribedCoursesId", "SubscribedUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_SubscribedUsersId",
                table: "UserCourses",
                column: "SubscribedUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Courses_SubscribedCoursesId",
                table: "UserCourses",
                column: "SubscribedCoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_SubscribedUsersId",
                table: "UserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Courses_SubscribedCoursesId",
                table: "UserCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses");

            migrationBuilder.RenameTable(
                name: "UserCourses",
                newName: "CourseUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourses_SubscribedUsersId",
                table: "CourseUser",
                newName: "IX_CourseUser_SubscribedUsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(6466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(5121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 580, DateTimeKind.Utc).AddTicks(5622),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(4505));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 570, DateTimeKind.Utc).AddTicks(1750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 402, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 38, 45, 566, DateTimeKind.Utc).AddTicks(4891),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 398, DateTimeKind.Utc).AddTicks(5917));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUser",
                table: "CourseUser",
                columns: new[] { "SubscribedCoursesId", "SubscribedUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_AspNetUsers_SubscribedUsersId",
                table: "CourseUser",
                column: "SubscribedUsersId",
                principalTable: "AspNetUsers",
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
