using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace narwhalLang.Migrations
{
    /// <inheritdoc />
    public partial class columnFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompilerModel",
                table: "Nodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "GlobalSupportedLibraryName",
                table: "Nodes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalSupportedLibraryName",
                table: "Nodes");

            migrationBuilder.AlterColumn<bool>(
                name: "CompilerModel",
                table: "Nodes",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
