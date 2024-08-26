using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageInTravelPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TravelPackages");
        }
    }
}
