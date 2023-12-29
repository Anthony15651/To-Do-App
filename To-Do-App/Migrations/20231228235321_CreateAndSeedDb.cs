using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace To_Do_App.Migrations
{
    /// <inheritdoc />
    public partial class CreateAndSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    priorityLevel = table.Column<int>(type: "int", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "category", "description", "dueDate", "priorityLevel", "taskName" },
                values: new object[,]
                {
                    { 1, 0, "Create basic CRUD functionality so ToDo list is usable", new DateTime(1, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, "Finish Basic Functionality" },
                    { 2, 0, "Add additional user-friendly features to enhance ToDo App", new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, "Add Additional Features" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
