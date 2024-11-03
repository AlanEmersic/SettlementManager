using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SettlementManager.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CountryId",
                table: "Settlements",
                column: "CountryId");

            migrationBuilder.Sql(@"
                CREATE TRIGGER SetCountriesUpdatedAtOnUpdate
                ON Countries
                AFTER UPDATE
                AS
                BEGIN
                    UPDATE Countries
                    SET UpdatedAt = GETUTCDATE()
                    FROM Countries c
                    INNER JOIN inserted i ON c.Id = i.Id;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER SetSettlementUpdatedAtOnUpdate
                ON Settlements
                AFTER UPDATE
                AS
                BEGIN
                    UPDATE Settlements
                    SET UpdatedAt = GETUTCDATE()
                    FROM Settlements s
                    INNER JOIN inserted i ON s.Id = i.Id;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS SetCountriesUpdatedAtOnUpdate;");

            migrationBuilder.Sql("DROP TRIGGER IF EXISTS SetSettlementUpdatedAtOnUpdate;");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}