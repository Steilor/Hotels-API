using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelss.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HotelLogoUrlAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Hotels");
        }
    }
}
