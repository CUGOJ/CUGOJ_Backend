using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CUGOJ.Backend.Base.UpdateSqlSnapshot.Migrations.v4initial
{
    public partial class v4initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "organization",
                newName: "role");

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "organization",
                type: "int",
                nullable: false,
                comment: "父组织中的身份",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "organization_type",
                table: "organization",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "组织类型");

            migrationBuilder.AddColumn<string>(
                name: "signature",
                table: "organization",
                type: "nvarchar(max)",
                nullable: true,
                comment: "个性签名");

            migrationBuilder.CreateTable(
                name: "authorize",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grantee_type = table.Column<int>(type: "int", nullable: false, comment: "被授权对象种类"),
                    grantee_id = table.Column<long>(type: "bigint", nullable: false, comment: "被授权对象Id"),
                    role = table.Column<int>(type: "int", nullable: false, comment: "被授权对象的"),
                    resource_type = table.Column<int>(type: "int", nullable: false, comment: "资源类型"),
                    resource_id = table.Column<long>(type: "bigint", nullable: false, comment: "资源Id"),
                    action = table.Column<long>(type: "bigint", nullable: false, comment: "授权的权限集合,按位表示"),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "最后更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorize", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_authorize_grantee_id_resource_id",
                table: "authorize",
                columns: new[] { "grantee_id", "resource_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorize");

            migrationBuilder.DropColumn(
                name: "organization_type",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "signature",
                table: "organization");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "organization",
                newName: "Role");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "organization",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "父组织中的身份");
        }
    }
}
