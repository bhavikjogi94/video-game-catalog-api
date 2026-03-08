using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameCatalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ── Genres ────────────────────────────────────────────────────────
            migrationBuilder.InsertData(
                table: "Genres",
                columns: ["Id", "Name"],
                values: new object[,]
                {
                    { 1, "Action-Adventure" },
                    { 2, "RPG" },
                    { 3, "First-Person Shooter" },
                    { 4, "Racing" },
                    { 5, "Sandbox" },
                    { 6, "Action RPG" }
                });

            // ── Platforms ─────────────────────────────────────────────────────
            migrationBuilder.InsertData(
                table: "Platforms",
                columns: ["Id", "Name", "Manufacturer"],
                values: new object[,]
                {
                    { 1, "Nintendo Switch", "Nintendo" },
                    { 2, "PlayStation 4",   "Sony" },
                    { 3, "PC",              "Multiple" },
                    { 4, "Xbox Series X",   "Microsoft" },
                    { 5, "PlayStation 5",   "Sony" }
                });

            // ── VideoGames ────────────────────────────────────────────────────
            // Demonstrates FK relationships: each game references a GenreId and PlatformId
            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: ["Id", "Title", "Developer", "ReleaseYear", "Description", "GenreId", "PlatformId"],
                values: new object[,]
                {
                    { 1,  "The Legend of Zelda: Breath of the Wild", "Nintendo",           2017, "Open-world adventure set in Hyrule.",                 1, 1 },
                    { 2,  "God of War",                              "Santa Monica Studio", 2018, "Kratos and Atreus journey through Norse mythology.",  1, 2 },
                    { 3,  "The Witcher 3: Wild Hunt",                "CD Projekt Red",      2015, "Massive open-world RPG following Geralt of Rivia.",   2, 3 },
                    { 4,  "Halo Infinite",                           "343 Industries",      2021, "Master Chief returns in an open-world Halo story.",   3, 4 },
                    { 5,  "Red Dead Redemption 2",                   "Rockstar Games",      2018, "An outlaw epic set across a vast American landscape.", 1, 2 },
                    { 6,  "Elden Ring",                              "FromSoftware",        2022, "Open-world action RPG co-created with George R.R. Martin.", 6, 3 },
                    { 7,  "Minecraft",                               "Mojang",              2011, "The iconic sandbox survival and building game.",       5, 3 },
                    { 8,  "Cyberpunk 2077",                          "CD Projekt Red",      2020, "Futuristic RPG set in the sprawling Night City.",     2, 3 },
                    { 9,  "Gran Turismo 7",                          "Polyphony Digital",   2022, "Premier racing simulation for PlayStation.",           4, 5 },
                    { 10, "Forza Horizon 5",                         "Playground Games",    2021, "Open-world racing set across a vibrant Mexico.",      4, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete in reverse FK order: games first, then genres and platforms
            migrationBuilder.DeleteData(table: "VideoGames", keyColumn: "Id", keyValues: [1,2,3,4,5,6,7,8,9,10]);
            migrationBuilder.DeleteData(table: "Genres",    keyColumn: "Id", keyValues: [1,2,3,4,5,6]);
            migrationBuilder.DeleteData(table: "Platforms", keyColumn: "Id", keyValues: [1,2,3,4,5]);
        }
    }
}
