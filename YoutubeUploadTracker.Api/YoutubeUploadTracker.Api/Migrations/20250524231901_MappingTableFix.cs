using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YoutubeUploadTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class MappingTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserYoutubeSubscriptionEntity_Channels_YoutubeChannelId",
                table: "UserYoutubeSubscriptionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserYoutubeSubscriptionEntity",
                table: "UserYoutubeSubscriptionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Channels",
                table: "Channels");

            migrationBuilder.RenameTable(
                name: "UserYoutubeSubscriptionEntity",
                newName: "YoutubeSubscriptionEntities");

            migrationBuilder.RenameTable(
                name: "Channels",
                newName: "YoutubeChannels");

            migrationBuilder.RenameIndex(
                name: "IX_UserYoutubeSubscriptionEntity_YoutubeChannelId",
                table: "YoutubeSubscriptionEntities",
                newName: "IX_YoutubeSubscriptionEntities_YoutubeChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_UserYoutubeSubscriptionEntity_UserId",
                table: "YoutubeSubscriptionEntities",
                newName: "IX_YoutubeSubscriptionEntities_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeSubscriptionEntities",
                table: "YoutubeSubscriptionEntities",
                columns: new[] { "UserId", "YoutubeChannelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeChannels",
                table: "YoutubeChannels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeSubscriptionEntities_YoutubeChannels_YoutubeChannelId",
                table: "YoutubeSubscriptionEntities",
                column: "YoutubeChannelId",
                principalTable: "YoutubeChannels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeSubscriptionEntities_YoutubeChannels_YoutubeChannelId",
                table: "YoutubeSubscriptionEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeSubscriptionEntities",
                table: "YoutubeSubscriptionEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeChannels",
                table: "YoutubeChannels");

            migrationBuilder.RenameTable(
                name: "YoutubeSubscriptionEntities",
                newName: "UserYoutubeSubscriptionEntity");

            migrationBuilder.RenameTable(
                name: "YoutubeChannels",
                newName: "Channels");

            migrationBuilder.RenameIndex(
                name: "IX_YoutubeSubscriptionEntities_YoutubeChannelId",
                table: "UserYoutubeSubscriptionEntity",
                newName: "IX_UserYoutubeSubscriptionEntity_YoutubeChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_YoutubeSubscriptionEntities_UserId",
                table: "UserYoutubeSubscriptionEntity",
                newName: "IX_UserYoutubeSubscriptionEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserYoutubeSubscriptionEntity",
                table: "UserYoutubeSubscriptionEntity",
                columns: new[] { "UserId", "YoutubeChannelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channels",
                table: "Channels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserYoutubeSubscriptionEntity_Channels_YoutubeChannelId",
                table: "UserYoutubeSubscriptionEntity",
                column: "YoutubeChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
