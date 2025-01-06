using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YoutubeUploadTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class Thumbnails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail240",
                table: "YoutubeChannels",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail800",
                table: "YoutubeChannels",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail88",
                table: "YoutubeChannels",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail240",
                table: "YoutubeChannels");

            migrationBuilder.DropColumn(
                name: "Thumbnail800",
                table: "YoutubeChannels");

            migrationBuilder.DropColumn(
                name: "Thumbnail88",
                table: "YoutubeChannels");
        }
    }
}
