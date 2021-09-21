using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class jons_to_list_strings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledJobs");

            migrationBuilder.AddColumn<string>(
                name: "Jobs",
                table: "Subscribes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 336, DateTimeKind.Utc).AddTicks(7965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(4588));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 336, DateTimeKind.Utc).AddTicks(6664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 292, DateTimeKind.Utc).AddTicks(1107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 629, DateTimeKind.Utc).AddTicks(2058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 281, DateTimeKind.Utc).AddTicks(4043),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 625, DateTimeKind.Utc).AddTicks(6146));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jobs",
                table: "Subscribes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(4588),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 336, DateTimeKind.Utc).AddTicks(7965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 639, DateTimeKind.Utc).AddTicks(3955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 336, DateTimeKind.Utc).AddTicks(6664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 629, DateTimeKind.Utc).AddTicks(2058),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 292, DateTimeKind.Utc).AddTicks(1107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 20, 11, 18, 29, 625, DateTimeKind.Utc).AddTicks(6146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 20, 21, 12, 3, 281, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.CreateTable(
                name: "ScheduledJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscribeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledJobs_Subscribes_SubscribeId",
                        column: x => x.SubscribeId,
                        principalTable: "Subscribes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_SubscribeId",
                table: "ScheduledJobs",
                column: "SubscribeId");
        }
    }
}
