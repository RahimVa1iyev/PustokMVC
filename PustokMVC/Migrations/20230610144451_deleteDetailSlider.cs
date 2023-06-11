using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokMVC.Migrations
{
    public partial class deleteDetailSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_DetailSliders_DetailSliderId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "DetailSliders");

            migrationBuilder.DropIndex(
                name: "IX_Images_DetailSliderId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DetailSliderId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetailSliderId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DetailSliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailSliders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_DetailSliderId",
                table: "Images",
                column: "DetailSliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_DetailSliders_DetailSliderId",
                table: "Images",
                column: "DetailSliderId",
                principalTable: "DetailSliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
