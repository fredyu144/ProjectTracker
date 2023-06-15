using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProjectTrackerAPI.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    Project = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.RecordId);
                });
            migrationBuilder.Sql(@"
                Insert Into Projects(Name,Company,Project,Role) 
                Values
                    ('Ankur','IBM','Payroll','Software Engineer'),
                    ('Akash','IBM','Chat Bot','Software Engineer'),
                    ('Priya','HP','VR Gaming','Project Manager'),
                    ('Asha','Microsoft','Payroll','Solution Architect'),
                    ('Nandini','HP','Payroll','Software Engineer'),
                    ('Piyush','Microsoft','Payroll','Delivery Manager'),
                    ('Ankur','HP','Chat Bot','Lead Engineer'),
                    ('Akash','HP','VR Gaming','Software Engineer'),
                    ('Priya','IBM','Payroll','Solution Architect'),
                    ('Asha','HP','Chat Bot','Project Manager'),
                    ('Nandini','IBM','VR Gaming','Lead Engineer'),
                    ('Piyush','Microsoft','Chat Bot','Delivery Manager');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
