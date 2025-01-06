using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YoutubeUploadTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class MappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserYoutubeSubscriptionEntity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    YoutubeChannelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserYoutubeSubscriptionEntity", x => new { x.UserId, x.YoutubeChannelId });
                    table.ForeignKey(
                        name: "FK_UserYoutubeSubscriptionEntity_Channels_YoutubeChannelId",
                        column: x => x.YoutubeChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserYoutubeSubscriptionEntity_UserId",
                table: "UserYoutubeSubscriptionEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserYoutubeSubscriptionEntity_YoutubeChannelId",
                table: "UserYoutubeSubscriptionEntity",
                column: "YoutubeChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserYoutubeSubscriptionEntity");
        }
    }
}
