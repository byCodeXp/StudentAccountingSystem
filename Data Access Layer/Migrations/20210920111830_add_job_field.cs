using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class add_job_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SubscribeId",
                table: "ScheduledJobs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(4588),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(7032));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(3955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(6507));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 629, DateTimeKind.Utc).AddTicks(2058),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 634, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 625, DateTimeKind.Utc).AddTicks(6146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 631, DateTimeKind.Utc).AddTicks(33));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_SubscribeId",
                table: "ScheduledJobs",
                column: "SubscribeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledJobs_Subscribes_SubscribeId",
                table: "ScheduledJobs",
                column: "SubscribeId",
                principalTable: "Subscribes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledJobs_Subscribes_SubscribeId",
                table: "ScheduledJobs");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledJobs_SubscribeId",
                table: "ScheduledJobs");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscribeId",
                table: "ScheduledJobs",
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
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(7032),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(4588));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 644, DateTimeKind.Utc).AddTicks(6507),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 634, DateTimeKind.Utc).AddTicks(5570),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 629, DateTimeKind.Utc).AddTicks(2058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 8, 55, 631, DateTimeKind.Utc).AddTicks(33),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 625, DateTimeKind.Utc).AddTicks(6146));
        }
    }
}
