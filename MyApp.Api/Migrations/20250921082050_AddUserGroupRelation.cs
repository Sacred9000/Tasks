using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGroupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "UserGroups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId1",
                table: "UserGroups",
                column: "GroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId1",
                table: "UserGroups",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId1",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_GroupId1",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "UserGroups");
        }
    }
}
