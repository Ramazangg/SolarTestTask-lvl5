using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarTestTask_lvl5.DbMigratot.Migrations
{
    /// <inheritdoc />
    public partial class addphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "User");
        }
    }
}
