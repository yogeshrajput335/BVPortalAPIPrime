using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVPortalApi.Migrations
{
    public partial class AddedJobIdInCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobId",
                table: "Candidates",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Openjobs_JobId",
                table: "Candidates",
                column: "JobId",
                principalTable: "Openjobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Openjobs_JobId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_JobId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Candidates");
        }
    }
}
