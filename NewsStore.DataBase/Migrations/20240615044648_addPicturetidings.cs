using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsStore.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class addPicturetidings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Tidings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Tidings");
        }
    }
}
