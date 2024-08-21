using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class deletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageItinerary_TravelPackages_TravelPackageId",
                table: "TravelPackageItinerary");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageItinerary_TravelPackages_TravelPackageId",
                table: "TravelPackageItinerary",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageItinerary_TravelPackages_TravelPackageId",
                table: "TravelPackageItinerary");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageItinerary_TravelPackages_TravelPackageId",
                table: "TravelPackageItinerary",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
