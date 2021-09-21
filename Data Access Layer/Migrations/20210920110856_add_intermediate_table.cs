using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class add_intermediate_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(7032),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(5121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(6507),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(4505));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 634, DateTimeKind.Utc).AddTicks(5570),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 402, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 631, DateTimeKind.Utc).AddTicks(33),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 398, DateTimeKind.Utc).AddTicks(5917));

            migrationBuilder.CreateTable(
                name: "ScheduledJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscribeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscribes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_CourseId",
                table: "Subscribes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_UserId",
                table: "Subscribes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledJobs");

            migrationBuilder.DropTable(
                name: "Subscribes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(5121),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 413, DateTimeKind.Utc).AddTicks(4505),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(6507));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 402, DateTimeKind.Utc).AddTicks(3786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 634, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 6, 58, 55, 398, DateTimeKind.Utc).AddTicks(5917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 631, DateTimeKind.Utc).AddTicks(33));

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    SubscribedCoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscribedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => new { x.SubscribedCoursesId, x.SubscribedUsersId });
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_SubscribedUsersId",
                        column: x => x.SubscribedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_SubscribedCoursesId",
                        column: x => x.SubscribedCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_SubscribedUsersId",
                table: "UserCourses",
                column: "SubscribedUsersId");
        }
    }
}
