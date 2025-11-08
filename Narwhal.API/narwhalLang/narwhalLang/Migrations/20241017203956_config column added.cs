using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace narwhalLang.Migrations
{
    /// <inheritdoc />
    public partial class configcolumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfigForDockerProvider",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfigForDockerProvider",
                table: "Properties");
        }
    }
}
