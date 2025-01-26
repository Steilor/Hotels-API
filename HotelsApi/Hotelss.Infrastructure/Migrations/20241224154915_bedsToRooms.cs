using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelss.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bedsToRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Beds",
                table: "Rooms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Rooms");
        }
    }
}
