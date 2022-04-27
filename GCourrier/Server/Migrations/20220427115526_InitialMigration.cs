using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCourrier.Server.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Power = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agent_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "HR" },
                    { 3, "Marketing" },
                    { 4, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "LastName", "PhotoPath", "Power" },
                values: new object[,]
                {
                    { 1, 1, "abd@samobay.com", "Abd", "ElMonem", "images/abd.png", 1 },
                    { 2, 2, "said@amobay.com", "Said", "Sassi", "images/said.jpg", 1 },
                    { 3, 3, "aymen@samobay.com", "Aymen", "Aymen", "images/aymen.png", 1 },
                    { 4, 4, "idriss@samobay.com", "Idriss", "Laabidi", "images/idriss.png", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_DepartmentId",
                table: "Agent",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
