using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Commander.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Commands",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            HowTo = table.Column<string>(maxLength: 255, nullable: false),
            Line = table.Column<string>(maxLength: 255, nullable: false),
            Platform = table.Column<string>(maxLength: 255, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Commands", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Commands");
    }
  }
}
