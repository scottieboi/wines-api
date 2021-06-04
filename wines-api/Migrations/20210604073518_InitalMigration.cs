using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WinesApi.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "box",
                columns: table => new
                {
                    boxno = table.Column<int>(type: "integer", nullable: false),
                    size = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("box_pkey", x => x.boxno);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vineyard",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vineyard = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vineyard", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winetype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    winetype = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winetype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winelist",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vintage = table.Column<short>(type: "smallint", nullable: true),
                    winename = table.Column<string>(type: "text", nullable: true),
                    winetypeid = table.Column<int>(type: "integer", nullable: true),
                    percentalcohol = table.Column<float>(type: "real", nullable: true),
                    pricepaid = table.Column<decimal>(type: "numeric(7,2)", precision: 7, scale: 2, nullable: true),
                    yearbought = table.Column<short>(type: "smallint", nullable: true),
                    bottlesize = table.Column<short>(type: "smallint", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<short>(type: "smallint", nullable: true),
                    drinkrangefrom = table.Column<short>(type: "smallint", nullable: true),
                    drinkrangeto = table.Column<short>(type: "smallint", nullable: true),
                    regionid = table.Column<int>(type: "integer", nullable: true),
                    vineyardid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winelist", x => x.id);
                    table.ForeignKey(
                        name: "winelist_regionid_fkey",
                        column: x => x.regionid,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "winelist_vineyardid_fkey",
                        column: x => x.vineyardid,
                        principalTable: "vineyard",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "winelist_winetypeid_fkey",
                        column: x => x.winetypeid,
                        principalTable: "winetype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wineid = table.Column<int>(type: "integer", nullable: true),
                    no = table.Column<int>(type: "integer", nullable: true),
                    box = table.Column<int>(type: "integer", nullable: true),
                    cellarversion = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                    table.ForeignKey(
                        name: "location_box_fkey",
                        column: x => x.box,
                        principalTable: "box",
                        principalColumn: "boxno",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "location_wineid_fkey",
                        column: x => x.wineid,
                        principalTable: "winelist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "location_box_fk",
                table: "location",
                column: "box");

            migrationBuilder.CreateIndex(
                name: "location_wineid_fk",
                table: "location",
                column: "wineid");

            migrationBuilder.CreateIndex(
                name: "winelist_regionid_fk",
                table: "winelist",
                column: "regionid");

            migrationBuilder.CreateIndex(
                name: "winelist_vineyardid_fk",
                table: "winelist",
                column: "vineyardid");

            migrationBuilder.CreateIndex(
                name: "winelist_winetypeid_fkey",
                table: "winelist",
                column: "winetypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "box");

            migrationBuilder.DropTable(
                name: "winelist");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropTable(
                name: "vineyard");

            migrationBuilder.DropTable(
                name: "winetype");
        }
    }
}
