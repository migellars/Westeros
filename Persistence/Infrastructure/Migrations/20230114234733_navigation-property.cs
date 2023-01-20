using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class navigationproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Tutorials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutorials_AuthorId",
                table: "Tutorials",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutorials_Author_AuthorId",
                table: "Tutorials",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutorials_Author_AuthorId",
                table: "Tutorials");

            migrationBuilder.DropIndex(
                name: "IX_Tutorials_AuthorId",
                table: "Tutorials");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tutorials");
        }
    }
}
