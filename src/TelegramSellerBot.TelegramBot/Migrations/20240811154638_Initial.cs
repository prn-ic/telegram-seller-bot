using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TelegramSellerBot.TelegramBot.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscription_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telegram_bot_durations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telegram_bot_durations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telegram_bots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    telegram_bot_link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telegram_bots", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    telegram_user_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    duration_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscriptions_subscription_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "subscription_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscriptions_telegram_bot_durations_duration_id",
                        column: x => x.duration_id,
                        principalTable: "telegram_bot_durations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscriptions_telegram_bots_service_id",
                        column: x => x.service_id,
                        principalTable: "telegram_bots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telegram_bot_duration_availabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    telegram_bot_id = table.Column<Guid>(type: "uuid", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    duration_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telegram_bot_duration_availabilities", x => x.id);
                    table.ForeignKey(
                        name: "fk_telegram_bot_duration_availabilities_telegram_bot_durations",
                        column: x => x.duration_id,
                        principalTable: "telegram_bot_durations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_telegram_bot_duration_availabilities_telegram_bots_telegram",
                        column: x => x.telegram_bot_id,
                        principalTable: "telegram_bots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    subscription_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    telegram_bot_id = table.Column<Guid>(type: "uuid", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscription_histories_subscription_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "subscription_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscription_histories_subscriptions_subscription_id",
                        column: x => x.subscription_id,
                        principalTable: "subscriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscription_histories_telegram_bots_telegram_bot_id",
                        column: x => x.telegram_bot_id,
                        principalTable: "telegram_bots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "subscription_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 0, "inactive" },
                    { 1, "active" },
                    { 2, "stoppedbyend" }
                });

            migrationBuilder.InsertData(
                table: "telegram_bot_durations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 168, "week" },
                    { 336, "twoweek" },
                    { 744, "month" },
                    { 2232, "threemonth" },
                    { 4464, "halfyear" },
                    { 8760, "year" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_subscription_histories_status_id",
                table: "subscription_histories",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_histories_subscription_id",
                table: "subscription_histories",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_histories_telegram_bot_id",
                table: "subscription_histories",
                column: "telegram_bot_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_duration_id",
                table: "subscriptions",
                column: "duration_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_service_id",
                table: "subscriptions",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_status_id",
                table: "subscriptions",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_telegram_bot_duration_availabilities_duration_id",
                table: "telegram_bot_duration_availabilities",
                column: "duration_id");

            migrationBuilder.CreateIndex(
                name: "ix_telegram_bot_duration_availabilities_telegram_bot_id",
                table: "telegram_bot_duration_availabilities",
                column: "telegram_bot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscription_histories");

            migrationBuilder.DropTable(
                name: "telegram_bot_duration_availabilities");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "subscription_statuses");

            migrationBuilder.DropTable(
                name: "telegram_bot_durations");

            migrationBuilder.DropTable(
                name: "telegram_bots");
        }
    }
}
