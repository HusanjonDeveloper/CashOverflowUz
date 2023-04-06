using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashOverflowUz.Migrations
{
	/// <inheritdoc />
	public partial class CreateLocations : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Locations",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Country = table.Column<int>(type: "int", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Locations", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Locations");
		}
	}
}
