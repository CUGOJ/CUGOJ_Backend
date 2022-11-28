using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CUGOJ.Backend.Base.UpdateSqlSnapshot.Migrations.v3initial
{
    public partial class v3initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOrganizationLinks",
                table: "UserOrganizationLinks");

            migrationBuilder.RenameTable(
                name: "UserOrganizationLinks",
                newName: "user_organization_link");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrganizationLinks_user_id",
                table: "user_organization_link",
                newName: "IX_user_organization_link_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrganizationLinks_organization_id_role",
                table: "user_organization_link",
                newName: "IX_user_organization_link_organization_id_role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_organization_link",
                table: "user_organization_link",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_organization_link",
                table: "user_organization_link");

            migrationBuilder.RenameTable(
                name: "user_organization_link",
                newName: "UserOrganizationLinks");

            migrationBuilder.RenameIndex(
                name: "IX_user_organization_link_user_id",
                table: "UserOrganizationLinks",
                newName: "IX_UserOrganizationLinks_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_organization_link_organization_id_role",
                table: "UserOrganizationLinks",
                newName: "IX_UserOrganizationLinks_organization_id_role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOrganizationLinks",
                table: "UserOrganizationLinks",
                column: "id");
        }
    }
}
