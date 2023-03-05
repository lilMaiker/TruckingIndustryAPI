using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingIndustryAPI.Migrations
{
    public partial class newtablecars2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandTrailer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrailerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDateTechnicalInspection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxWeight = table.Column<double>(type: "float", nullable: false),
                    WithOpenSide = table.Column<bool>(type: "bit", nullable: false),
                    WithRefrigerator = table.Column<bool>(type: "bit", nullable: false),
                    WithTent = table.Column<bool>(type: "bit", nullable: false),
                    WithHydroboard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");


        }
    }
}
