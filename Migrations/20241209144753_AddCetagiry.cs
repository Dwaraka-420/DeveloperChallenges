using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeveloperChallenges.Migrations
{
    /// <inheritdoc />
    public partial class AddCetagiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_challengeReplays_challenges_ChallengeId",
                table: "challengeReplays");

            migrationBuilder.DropIndex(
                name: "IX_challengeReplays_ChallengeId",
                table: "challengeReplays");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "challenges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "challenges");

            migrationBuilder.CreateIndex(
                name: "IX_challengeReplays_ChallengeId",
                table: "challengeReplays",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_challengeReplays_challenges_ChallengeId",
                table: "challengeReplays",
                column: "ChallengeId",
                principalTable: "challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
