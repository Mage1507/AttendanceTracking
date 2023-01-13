using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTracking.Migrations
{
    /// <inheritdoc />
    public partial class migrationv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    departmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.departmentId);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    managerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    managerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.managerId);
                    table.ForeignKey(
                        name: "FK_managers_departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "departments",
                        principalColumn: "departmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_employees_managers_managerId",
                        column: x => x.managerId,
                        principalTable: "managers",
                        principalColumn: "managerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    checkInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    checkOutTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendances", x => x.id);
                    table.ForeignKey(
                        name: "FK_attendances_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendances_employeeId",
                table: "attendances",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_managerId",
                table: "employees",
                column: "managerId");

            migrationBuilder.CreateIndex(
                name: "IX_managers_departmentId",
                table: "managers",
                column: "departmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "managers");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
