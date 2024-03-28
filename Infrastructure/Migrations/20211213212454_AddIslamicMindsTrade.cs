using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddIslamicMindsTrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsWithdrawActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDepositActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(4732));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5008));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5009));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5014));

            migrationBuilder.InsertData(
                table: "InvestmentPlans",
                columns: new[] { "Id", "Created", "CreatedBy", "CurrentProfitPercent", "LastModified", "LastModifiedBy", "MaxProfitPercent", "MinProfitPercent", "Name" },
                values: new object[] { 7, new DateTime(2021, 12, 13, 21, 24, 54, 190, DateTimeKind.Utc).AddTicks(5015), 0, 0.0, null, 0, 1000.0, 0.0, "IslamicMindsTradePlan" });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 203, DateTimeKind.Utc).AddTicks(4237));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 203, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 203, DateTimeKind.Utc).AddTicks(4251));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 12, 13, 21, 24, 54, 203, DateTimeKind.Utc).AddTicks(4252));

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDepositActive", "IsWithdrawActive", "LastModified", "LastModifiedBy" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, true, null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DeleteData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4451));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4455));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.UpdateData(
                table: "InvestmentPlans",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 152, DateTimeKind.Utc).AddTicks(4461));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 166, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 166, DateTimeKind.Utc).AddTicks(2314));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 166, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 10, 30, 8, 21, 52, 166, DateTimeKind.Utc).AddTicks(2317));
        }
    }
}
