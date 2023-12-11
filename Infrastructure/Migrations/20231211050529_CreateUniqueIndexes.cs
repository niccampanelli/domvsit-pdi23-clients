using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_client_Email",
                table: "client",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendant_token_Value",
                table: "attendant_token",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_client_Email",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_attendant_token_Value",
                table: "attendant_token");
        }
    }
}
