using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageDepartureLocations_DeparatureLocations_DepartureLocationId",
                table: "TravelPackageDepartureLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageItineraries_Itineraries_ItineraryId",
                table: "TravelPackageItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageTags_Tags_TagId",
                table: "TravelPackageTags");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageDepartureLocations_DeparatureLocations_DepartureLocationId",
                table: "TravelPackageDepartureLocations",
                column: "DepartureLocationId",
                principalTable: "DeparatureLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageItineraries_Itineraries_ItineraryId",
                table: "TravelPackageItineraries",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageTags_Tags_TagId",
                table: "TravelPackageTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageDepartureLocations_DeparatureLocations_DepartureLocationId",
                table: "TravelPackageDepartureLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageItineraries_Itineraries_ItineraryId",
                table: "TravelPackageItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackageTags_Tags_TagId",
                table: "TravelPackageTags");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageDepartureLocations_DeparatureLocations_DepartureLocationId",
                table: "TravelPackageDepartureLocations",
                column: "DepartureLocationId",
                principalTable: "DeparatureLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageItineraries_Itineraries_ItineraryId",
                table: "TravelPackageItineraries",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackageTags_Tags_TagId",
                table: "TravelPackageTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
