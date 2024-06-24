﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShare.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDocumetoFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "Books");
        }
    }
}
