using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClanChat.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClanId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Clans_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FromUserId = table.Column<int>(type: "integer", nullable: false),
                    ToClanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Clans_ToClanId",
                        column: x => x.ToClanId,
                        principalTable: "Clans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clans",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "King Gizzard" },
                    { 2, "Lizard Wizard" },
                    { 3, "Sigma Males" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClanId", "Name" },
                values: new object[,]
                {
                    { 1, null, "User1" },
                    { 2, null, "User2" },
                    { 3, null, "User3" },
                    { 4, null, "User4" },
                    { 5, null, "User5" },
                    { 6, null, "User6" },
                    { 7, null, "User7" },
                    { 8, null, "User8" },
                    { 9, null, "User9" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClanId", "Name" },
                values: new object[,]
                {
                    { 10, 1, "User10" },
                    { 11, 1, "User11" },
                    { 12, 1, "User12" },
                    { 13, 1, "User13" },
                    { 14, 1, "User14" },
                    { 15, 1, "User15" },
                    { 16, 1, "User16" },
                    { 17, 1, "User17" },
                    { 18, 1, "User18" },
                    { 19, 1, "User19" },
                    { 20, 2, "User20" },
                    { 21, 2, "User21" },
                    { 22, 2, "User22" },
                    { 23, 2, "User23" },
                    { 24, 2, "User24" },
                    { 25, 2, "User25" },
                    { 26, 2, "User26" },
                    { 27, 2, "User27" },
                    { 28, 2, "User28" },
                    { 29, 2, "User29" },
                    { 30, 3, "User30" },
                    { 31, 3, "User31" },
                    { 32, 3, "User32" },
                    { 33, 3, "User33" },
                    { 34, 3, "User34" },
                    { 35, 3, "User35" },
                    { 36, 3, "User36" },
                    { 37, 3, "User37" },
                    { 38, 3, "User38" },
                    { 39, 3, "User39" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "FromUserId", "Timestamp", "ToClanId" },
                values: new object[,]
                {
                    { 1, "Default message #1", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 32, 56, 854, DateTimeKind.Unspecified).AddTicks(167), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 2, "Default message #2", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 32, 57, 854, DateTimeKind.Unspecified).AddTicks(193), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 3, "Default message #3", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 32, 58, 854, DateTimeKind.Unspecified).AddTicks(196), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 4, "Default message #4", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 32, 59, 854, DateTimeKind.Unspecified).AddTicks(198), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 5, "Default message #5", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 0, 854, DateTimeKind.Unspecified).AddTicks(200), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 6, "Default message #6", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 1, 854, DateTimeKind.Unspecified).AddTicks(204), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 7, "Default message #7", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 2, 854, DateTimeKind.Unspecified).AddTicks(206), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 8, "Default message #8", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 3, 854, DateTimeKind.Unspecified).AddTicks(208), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 9, "Default message #9", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 4, 854, DateTimeKind.Unspecified).AddTicks(210), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 10, "Default message #10", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 5, 854, DateTimeKind.Unspecified).AddTicks(213), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 11, "Default message #11", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 6, 854, DateTimeKind.Unspecified).AddTicks(215), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 12, "Default message #12", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 7, 854, DateTimeKind.Unspecified).AddTicks(217), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 13, "Default message #13", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 8, 854, DateTimeKind.Unspecified).AddTicks(219), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 14, "Default message #14", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 9, 854, DateTimeKind.Unspecified).AddTicks(221), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 15, "Default message #15", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 10, 854, DateTimeKind.Unspecified).AddTicks(223), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 16, "Default message #16", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 11, 854, DateTimeKind.Unspecified).AddTicks(225), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 17, "Default message #17", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 12, 854, DateTimeKind.Unspecified).AddTicks(227), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 18, "Default message #18", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 13, 854, DateTimeKind.Unspecified).AddTicks(229), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 19, "Default message #19", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 14, 854, DateTimeKind.Unspecified).AddTicks(231), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 20, "Default message #20", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 15, 854, DateTimeKind.Unspecified).AddTicks(233), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 21, "Default message #21", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 16, 854, DateTimeKind.Unspecified).AddTicks(235), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 22, "Default message #22", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 17, 854, DateTimeKind.Unspecified).AddTicks(237), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 23, "Default message #23", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 18, 854, DateTimeKind.Unspecified).AddTicks(239), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 24, "Default message #24", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 19, 854, DateTimeKind.Unspecified).AddTicks(241), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 25, "Default message #25", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 20, 854, DateTimeKind.Unspecified).AddTicks(243), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 26, "Default message #26", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 21, 854, DateTimeKind.Unspecified).AddTicks(298), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 27, "Default message #27", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 22, 854, DateTimeKind.Unspecified).AddTicks(301), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 28, "Default message #28", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 23, 854, DateTimeKind.Unspecified).AddTicks(303), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 29, "Default message #29", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 24, 854, DateTimeKind.Unspecified).AddTicks(305), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 30, "Default message #30", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 25, 854, DateTimeKind.Unspecified).AddTicks(307), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 31, "Default message #31", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 26, 854, DateTimeKind.Unspecified).AddTicks(309), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 32, "Default message #32", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 27, 854, DateTimeKind.Unspecified).AddTicks(311), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 33, "Default message #33", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 28, 854, DateTimeKind.Unspecified).AddTicks(313), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 34, "Default message #34", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 29, 854, DateTimeKind.Unspecified).AddTicks(316), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 35, "Default message #35", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 30, 854, DateTimeKind.Unspecified).AddTicks(318), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 36, "Default message #36", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 31, 854, DateTimeKind.Unspecified).AddTicks(320), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 37, "Default message #37", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 32, 854, DateTimeKind.Unspecified).AddTicks(322), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 38, "Default message #38", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 33, 854, DateTimeKind.Unspecified).AddTicks(324), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 39, "Default message #39", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 34, 854, DateTimeKind.Unspecified).AddTicks(326), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 40, "Default message #40", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 35, 854, DateTimeKind.Unspecified).AddTicks(328), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 41, "Default message #41", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 36, 854, DateTimeKind.Unspecified).AddTicks(330), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 42, "Default message #42", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 37, 854, DateTimeKind.Unspecified).AddTicks(332), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 43, "Default message #43", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 38, 854, DateTimeKind.Unspecified).AddTicks(334), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 44, "Default message #44", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 39, 854, DateTimeKind.Unspecified).AddTicks(336), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 45, "Default message #45", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 40, 854, DateTimeKind.Unspecified).AddTicks(338), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 46, "Default message #46", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 41, 854, DateTimeKind.Unspecified).AddTicks(340), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 47, "Default message #47", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 42, 854, DateTimeKind.Unspecified).AddTicks(342), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 48, "Default message #48", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 43, 854, DateTimeKind.Unspecified).AddTicks(344), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 49, "Default message #49", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 44, 854, DateTimeKind.Unspecified).AddTicks(346), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 50, "Default message #50", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 45, 854, DateTimeKind.Unspecified).AddTicks(347), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 51, "Default message #51", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 46, 854, DateTimeKind.Unspecified).AddTicks(349), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 52, "Default message #52", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 47, 854, DateTimeKind.Unspecified).AddTicks(351), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 53, "Default message #53", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 48, 854, DateTimeKind.Unspecified).AddTicks(353), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 54, "Default message #54", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 49, 854, DateTimeKind.Unspecified).AddTicks(355), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 55, "Default message #55", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 50, 854, DateTimeKind.Unspecified).AddTicks(357), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 56, "Default message #56", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 51, 854, DateTimeKind.Unspecified).AddTicks(359), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 57, "Default message #57", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 52, 854, DateTimeKind.Unspecified).AddTicks(361), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 58, "Default message #58", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 53, 854, DateTimeKind.Unspecified).AddTicks(363), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 59, "Default message #59", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 54, 854, DateTimeKind.Unspecified).AddTicks(365), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 60, "Default message #60", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 55, 854, DateTimeKind.Unspecified).AddTicks(367), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 61, "Default message #61", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 56, 854, DateTimeKind.Unspecified).AddTicks(369), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 62, "Default message #62", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 57, 854, DateTimeKind.Unspecified).AddTicks(371), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 63, "Default message #63", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 58, 854, DateTimeKind.Unspecified).AddTicks(372), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 64, "Default message #64", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 33, 59, 854, DateTimeKind.Unspecified).AddTicks(374), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 65, "Default message #65", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 0, 854, DateTimeKind.Unspecified).AddTicks(376), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 66, "Default message #66", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 1, 854, DateTimeKind.Unspecified).AddTicks(379), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 67, "Default message #67", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 2, 854, DateTimeKind.Unspecified).AddTicks(381), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 68, "Default message #68", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 3, 854, DateTimeKind.Unspecified).AddTicks(383), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 69, "Default message #69", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 4, 854, DateTimeKind.Unspecified).AddTicks(385), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 70, "Default message #70", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 5, 854, DateTimeKind.Unspecified).AddTicks(387), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 71, "Default message #71", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 6, 854, DateTimeKind.Unspecified).AddTicks(389), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 72, "Default message #72", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 7, 854, DateTimeKind.Unspecified).AddTicks(390), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 73, "Default message #73", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 8, 854, DateTimeKind.Unspecified).AddTicks(392), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 74, "Default message #74", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 9, 854, DateTimeKind.Unspecified).AddTicks(394), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 75, "Default message #75", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 10, 854, DateTimeKind.Unspecified).AddTicks(434), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 76, "Default message #76", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 11, 854, DateTimeKind.Unspecified).AddTicks(437), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 77, "Default message #77", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 12, 854, DateTimeKind.Unspecified).AddTicks(439), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 78, "Default message #78", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 13, 854, DateTimeKind.Unspecified).AddTicks(440), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 79, "Default message #79", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 14, 854, DateTimeKind.Unspecified).AddTicks(442), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 80, "Default message #80", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 15, 854, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 81, "Default message #81", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 16, 854, DateTimeKind.Unspecified).AddTicks(446), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 82, "Default message #82", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 17, 854, DateTimeKind.Unspecified).AddTicks(448), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 83, "Default message #83", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 18, 854, DateTimeKind.Unspecified).AddTicks(450), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 84, "Default message #84", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 19, 854, DateTimeKind.Unspecified).AddTicks(452), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 85, "Default message #85", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 20, 854, DateTimeKind.Unspecified).AddTicks(454), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 86, "Default message #86", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 21, 854, DateTimeKind.Unspecified).AddTicks(456), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 87, "Default message #87", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 22, 854, DateTimeKind.Unspecified).AddTicks(458), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 88, "Default message #88", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 23, 854, DateTimeKind.Unspecified).AddTicks(460), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 89, "Default message #89", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 24, 854, DateTimeKind.Unspecified).AddTicks(462), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 90, "Default message #90", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 25, 854, DateTimeKind.Unspecified).AddTicks(463), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 91, "Default message #91", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 26, 854, DateTimeKind.Unspecified).AddTicks(465), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 92, "Default message #92", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 27, 854, DateTimeKind.Unspecified).AddTicks(467), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 93, "Default message #93", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 28, 854, DateTimeKind.Unspecified).AddTicks(469), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 94, "Default message #94", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 29, 854, DateTimeKind.Unspecified).AddTicks(471), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 95, "Default message #95", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 30, 854, DateTimeKind.Unspecified).AddTicks(473), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 96, "Default message #96", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 31, 854, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 97, "Default message #97", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 32, 854, DateTimeKind.Unspecified).AddTicks(477), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 98, "Default message #98", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 33, 854, DateTimeKind.Unspecified).AddTicks(479), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 99, "Default message #99", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 34, 854, DateTimeKind.Unspecified).AddTicks(481), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 100, "Default message #100", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 35, 854, DateTimeKind.Unspecified).AddTicks(483), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 101, "Default message #101", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 36, 854, DateTimeKind.Unspecified).AddTicks(485), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 102, "Default message #102", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 37, 854, DateTimeKind.Unspecified).AddTicks(487), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 103, "Default message #103", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 38, 854, DateTimeKind.Unspecified).AddTicks(488), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 104, "Default message #104", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 39, 854, DateTimeKind.Unspecified).AddTicks(490), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 105, "Default message #105", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 40, 854, DateTimeKind.Unspecified).AddTicks(492), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 106, "Default message #106", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 41, 854, DateTimeKind.Unspecified).AddTicks(494), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 107, "Default message #107", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 42, 854, DateTimeKind.Unspecified).AddTicks(496), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 108, "Default message #108", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 43, 854, DateTimeKind.Unspecified).AddTicks(498), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 109, "Default message #109", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 44, 854, DateTimeKind.Unspecified).AddTicks(500), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 110, "Default message #110", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 45, 854, DateTimeKind.Unspecified).AddTicks(502), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 111, "Default message #111", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 46, 854, DateTimeKind.Unspecified).AddTicks(504), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 112, "Default message #112", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 47, 854, DateTimeKind.Unspecified).AddTicks(505), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 113, "Default message #113", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 48, 854, DateTimeKind.Unspecified).AddTicks(507), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 114, "Default message #114", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 49, 854, DateTimeKind.Unspecified).AddTicks(509), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 115, "Default message #115", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 50, 854, DateTimeKind.Unspecified).AddTicks(511), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 116, "Default message #116", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 51, 854, DateTimeKind.Unspecified).AddTicks(513), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 117, "Default message #117", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 52, 854, DateTimeKind.Unspecified).AddTicks(515), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 118, "Default message #118", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 53, 854, DateTimeKind.Unspecified).AddTicks(517), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 119, "Default message #119", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 54, 854, DateTimeKind.Unspecified).AddTicks(519), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 120, "Default message #120", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 55, 854, DateTimeKind.Unspecified).AddTicks(520), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 121, "Default message #121", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 56, 854, DateTimeKind.Unspecified).AddTicks(522), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 122, "Default message #122", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 57, 854, DateTimeKind.Unspecified).AddTicks(524), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 123, "Default message #123", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 58, 854, DateTimeKind.Unspecified).AddTicks(526), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 124, "Default message #124", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 34, 59, 854, DateTimeKind.Unspecified).AddTicks(528), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 125, "Default message #125", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 0, 854, DateTimeKind.Unspecified).AddTicks(530), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 126, "Default message #126", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 1, 854, DateTimeKind.Unspecified).AddTicks(532), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 127, "Default message #127", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 2, 854, DateTimeKind.Unspecified).AddTicks(534), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 128, "Default message #128", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 3, 854, DateTimeKind.Unspecified).AddTicks(536), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 129, "Default message #129", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 4, 854, DateTimeKind.Unspecified).AddTicks(537), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 130, "Default message #130", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 5, 854, DateTimeKind.Unspecified).AddTicks(578), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 131, "Default message #131", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 6, 854, DateTimeKind.Unspecified).AddTicks(581), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 132, "Default message #132", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 7, 854, DateTimeKind.Unspecified).AddTicks(583), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 133, "Default message #133", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 8, 854, DateTimeKind.Unspecified).AddTicks(585), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 134, "Default message #134", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 9, 854, DateTimeKind.Unspecified).AddTicks(587), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 135, "Default message #135", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 10, 854, DateTimeKind.Unspecified).AddTicks(588), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 136, "Default message #136", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 11, 854, DateTimeKind.Unspecified).AddTicks(590), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 137, "Default message #137", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 12, 854, DateTimeKind.Unspecified).AddTicks(592), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 138, "Default message #138", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 13, 854, DateTimeKind.Unspecified).AddTicks(594), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 139, "Default message #139", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 14, 854, DateTimeKind.Unspecified).AddTicks(596), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 140, "Default message #140", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 15, 854, DateTimeKind.Unspecified).AddTicks(598), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 141, "Default message #141", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 16, 854, DateTimeKind.Unspecified).AddTicks(600), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 142, "Default message #142", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 17, 854, DateTimeKind.Unspecified).AddTicks(602), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 143, "Default message #143", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 18, 854, DateTimeKind.Unspecified).AddTicks(604), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 144, "Default message #144", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 19, 854, DateTimeKind.Unspecified).AddTicks(606), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 145, "Default message #145", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 20, 854, DateTimeKind.Unspecified).AddTicks(607), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 146, "Default message #146", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 21, 854, DateTimeKind.Unspecified).AddTicks(609), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 147, "Default message #147", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 22, 854, DateTimeKind.Unspecified).AddTicks(611), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 148, "Default message #148", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 23, 854, DateTimeKind.Unspecified).AddTicks(613), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 149, "Default message #149", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 24, 854, DateTimeKind.Unspecified).AddTicks(615), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 150, "Default message #150", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 25, 854, DateTimeKind.Unspecified).AddTicks(617), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 151, "Default message #151", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 26, 854, DateTimeKind.Unspecified).AddTicks(619), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 152, "Default message #152", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 27, 854, DateTimeKind.Unspecified).AddTicks(621), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 153, "Default message #153", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 28, 854, DateTimeKind.Unspecified).AddTicks(623), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 154, "Default message #154", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 29, 854, DateTimeKind.Unspecified).AddTicks(625), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 155, "Default message #155", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 30, 854, DateTimeKind.Unspecified).AddTicks(626), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 156, "Default message #156", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 31, 854, DateTimeKind.Unspecified).AddTicks(628), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 157, "Default message #157", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 32, 854, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 158, "Default message #158", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 33, 854, DateTimeKind.Unspecified).AddTicks(632), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 159, "Default message #159", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 34, 854, DateTimeKind.Unspecified).AddTicks(634), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 160, "Default message #160", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 35, 854, DateTimeKind.Unspecified).AddTicks(636), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 161, "Default message #161", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 36, 854, DateTimeKind.Unspecified).AddTicks(638), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 162, "Default message #162", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 37, 854, DateTimeKind.Unspecified).AddTicks(640), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 163, "Default message #163", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 38, 854, DateTimeKind.Unspecified).AddTicks(641), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 164, "Default message #164", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 39, 854, DateTimeKind.Unspecified).AddTicks(643), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 165, "Default message #165", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 40, 854, DateTimeKind.Unspecified).AddTicks(645), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 166, "Default message #166", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 41, 854, DateTimeKind.Unspecified).AddTicks(647), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 167, "Default message #167", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 42, 854, DateTimeKind.Unspecified).AddTicks(649), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 168, "Default message #168", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 43, 854, DateTimeKind.Unspecified).AddTicks(651), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 169, "Default message #169", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 44, 854, DateTimeKind.Unspecified).AddTicks(653), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 170, "Default message #170", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 45, 854, DateTimeKind.Unspecified).AddTicks(655), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 171, "Default message #171", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 46, 854, DateTimeKind.Unspecified).AddTicks(657), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 172, "Default message #172", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 47, 854, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 173, "Default message #173", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 48, 854, DateTimeKind.Unspecified).AddTicks(660), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 174, "Default message #174", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 49, 854, DateTimeKind.Unspecified).AddTicks(662), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 175, "Default message #175", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 50, 854, DateTimeKind.Unspecified).AddTicks(664), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 176, "Default message #176", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 51, 854, DateTimeKind.Unspecified).AddTicks(666), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 177, "Default message #177", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 52, 854, DateTimeKind.Unspecified).AddTicks(668), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 178, "Default message #178", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 53, 854, DateTimeKind.Unspecified).AddTicks(670), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 179, "Default message #179", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 54, 854, DateTimeKind.Unspecified).AddTicks(672), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 180, "Default message #180", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 55, 854, DateTimeKind.Unspecified).AddTicks(710), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 181, "Default message #181", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 56, 854, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 182, "Default message #182", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 57, 854, DateTimeKind.Unspecified).AddTicks(714), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 183, "Default message #183", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 58, 854, DateTimeKind.Unspecified).AddTicks(716), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 184, "Default message #184", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 35, 59, 854, DateTimeKind.Unspecified).AddTicks(718), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 185, "Default message #185", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 0, 854, DateTimeKind.Unspecified).AddTicks(720), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 186, "Default message #186", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 1, 854, DateTimeKind.Unspecified).AddTicks(722), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 187, "Default message #187", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 2, 854, DateTimeKind.Unspecified).AddTicks(724), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 188, "Default message #188", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 3, 854, DateTimeKind.Unspecified).AddTicks(726), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 189, "Default message #189", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 4, 854, DateTimeKind.Unspecified).AddTicks(728), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 190, "Default message #190", 10, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 5, 854, DateTimeKind.Unspecified).AddTicks(730), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 191, "Default message #191", 21, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 6, 854, DateTimeKind.Unspecified).AddTicks(732), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 192, "Default message #192", 12, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 7, 854, DateTimeKind.Unspecified).AddTicks(734), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 193, "Default message #193", 23, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 8, 854, DateTimeKind.Unspecified).AddTicks(736), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 194, "Default message #194", 14, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 9, 854, DateTimeKind.Unspecified).AddTicks(738), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 195, "Default message #195", 25, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 10, 854, DateTimeKind.Unspecified).AddTicks(740), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 196, "Default message #196", 16, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 11, 854, DateTimeKind.Unspecified).AddTicks(742), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 197, "Default message #197", 27, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 12, 854, DateTimeKind.Unspecified).AddTicks(744), new TimeSpan(0, 3, 0, 0, 0)), 2 },
                    { 198, "Default message #198", 18, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 13, 854, DateTimeKind.Unspecified).AddTicks(746), new TimeSpan(0, 3, 0, 0, 0)), 1 },
                    { 199, "Default message #199", 29, new DateTimeOffset(new DateTime(2023, 3, 2, 10, 36, 14, 854, DateTimeKind.Unspecified).AddTicks(748), new TimeSpan(0, 3, 0, 0, 0)), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clans_Name",
                table: "Clans",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToClanId",
                table: "Messages",
                column: "ToClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClanId",
                table: "Users",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clans");
        }
    }
}
