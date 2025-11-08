using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace narwhalLang.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsResource = table.Column<bool>(type: "boolean", nullable: false),
                    IsDataSource = table.Column<bool>(type: "boolean", nullable: false),
                    CompilerModel = table.Column<bool>(type: "boolean", nullable: false),
                    VisualName = table.Column<string>(type: "text", nullable: false),
                    VisualType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NarwhalNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentPropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PropertyName = table.Column<string>(type: "text", nullable: false),
                    PropertyRuntimeValueType = table.Column<string>(type: "text", nullable: false),
                    PropertyDefinition = table.Column<string>(type: "text", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsNestedProperty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesReadOnly",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NarwhalNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentReadOnlyPropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PropertyName = table.Column<string>(type: "text", nullable: false),
                    PropertyRuntimeValueType = table.Column<string>(type: "text", nullable: false),
                    PropertyDefinition = table.Column<string>(type: "text", nullable: false),
                    IsNestedProperty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesReadOnly", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PropertiesReadOnly");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
