using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowingErrors.Migrations
{
    /// <inheritdoc />
    public partial class BugindexUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bug_ProjectId",
                table: "Bug");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_ProjectId_UserId",
                table: "Bug",
                columns: new[] { "ProjectId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bug_ProjectId_UserId",
                table: "Bug");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_ProjectId",
                table: "Bug",
                column: "ProjectId");
        }
    }
}
