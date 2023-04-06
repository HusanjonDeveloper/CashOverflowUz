using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashOverflowUz.Migrations
{
	/// <inheritdoc />
	public partial class CreateJobsTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Jobs",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
					level = table.Column<int>(type: "int", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Jobs", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Jobs");
		}
	}
}
