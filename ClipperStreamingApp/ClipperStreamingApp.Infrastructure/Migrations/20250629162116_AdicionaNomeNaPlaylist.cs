using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClipperStreamingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaNomeNaPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Playlists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Playlists");
        }
    }
}
