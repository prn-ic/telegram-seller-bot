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
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    telegram_bot_id = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.InsertData(
                table: "telegram_bots",
                columns: new[] { "id", "description", "name", "telegram_bot_link" },
                values: new object[,]
                {
                    { new Guid("035b033a-a042-4f10-af8d-faea50fbfcf3"), "some description, none more", "Some service #2", "https://google.com" },
                    { new Guid("1d6154e8-f1e5-435a-93e2-244a0e27d0ad"), "some description, none more", "Some service #1", "https://google.com" },
                    { new Guid("2423888f-e190-4c6f-8c32-27f5fdf3c174"), "some description, none more", "Some service #5", "https://google.com" },
                    { new Guid("395b8881-01ea-4a54-aab1-6e8029afa1de"), "some description, none more", "Some service #4", "https://google.com" },
                    { new Guid("64c6f5e9-d535-4eca-a786-ec769b5b70b3"), "some description, none more", "Some service #3", "https://google.com" },
                    { new Guid("74297ae7-11bd-4db4-81af-3a25738f16e9"), "some description, none more", "Some service #7", "https://google.com" },
                    { new Guid("8dfac16d-e70e-4add-afab-b62f82dc9263"), "some description, none more", "Some service #6", "https://google.com" },
                    { new Guid("afd6aa9a-9582-4af3-beb6-f04725809d46"), "some description, none more", "Some service #8", "https://google.com" }
                });

            migrationBuilder.InsertData(
                table: "telegram_bot_duration_availabilities",
                columns: new[] { "id", "cost", "duration_id", "telegram_bot_id" },
                values: new object[,]
                {
                    { 1, 100m, 168, new Guid("1d6154e8-f1e5-435a-93e2-244a0e27d0ad") },
                    { 2, 200m, 4464, new Guid("1d6154e8-f1e5-435a-93e2-244a0e27d0ad") },
                    { 3, 3100m, 8760, new Guid("1d6154e8-f1e5-435a-93e2-244a0e27d0ad") },
                    { 4, 100m, 168, new Guid("035b033a-a042-4f10-af8d-faea50fbfcf3") },
                    { 5, 200m, 4464, new Guid("035b033a-a042-4f10-af8d-faea50fbfcf3") },
                    { 6, 3100m, 8760, new Guid("035b033a-a042-4f10-af8d-faea50fbfcf3") },
                    { 7, 100m, 168, new Guid("64c6f5e9-d535-4eca-a786-ec769b5b70b3") },
                    { 8, 200m, 4464, new Guid("64c6f5e9-d535-4eca-a786-ec769b5b70b3") },
                    { 9, 3100m, 8760, new Guid("64c6f5e9-d535-4eca-a786-ec769b5b70b3") },
                    { 10, 100m, 168, new Guid("395b8881-01ea-4a54-aab1-6e8029afa1de") },
                    { 11, 200m, 4464, new Guid("395b8881-01ea-4a54-aab1-6e8029afa1de") },
                    { 12, 3100m, 8760, new Guid("395b8881-01ea-4a54-aab1-6e8029afa1de") },
                    { 13, 100m, 168, new Guid("2423888f-e190-4c6f-8c32-27f5fdf3c174") },
                    { 14, 200m, 4464, new Guid("2423888f-e190-4c6f-8c32-27f5fdf3c174") },
                    { 15, 3100m, 8760, new Guid("2423888f-e190-4c6f-8c32-27f5fdf3c174") },
                    { 16, 100m, 168, new Guid("8dfac16d-e70e-4add-afab-b62f82dc9263") },
                    { 17, 200m, 4464, new Guid("8dfac16d-e70e-4add-afab-b62f82dc9263") },
                    { 18, 3100m, 8760, new Guid("8dfac16d-e70e-4add-afab-b62f82dc9263") },
                    { 19, 100m, 168, new Guid("74297ae7-11bd-4db4-81af-3a25738f16e9") },
                    { 20, 200m, 4464, new Guid("74297ae7-11bd-4db4-81af-3a25738f16e9") },
                    { 21, 3100m, 8760, new Guid("74297ae7-11bd-4db4-81af-3a25738f16e9") },
                    { 22, 100m, 168, new Guid("afd6aa9a-9582-4af3-beb6-f04725809d46") },
                    { 23, 200m, 4464, new Guid("afd6aa9a-9582-4af3-beb6-f04725809d46") },
                    { 24, 3100m, 8760, new Guid("afd6aa9a-9582-4af3-beb6-f04725809d46") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_subscription_histories_id",
                table: "subscription_histories",
                column: "id",
                unique: true);

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
                name: "ix_subscription_statuses_id",
                table: "subscription_statuses",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_duration_id",
                table: "subscriptions",
                column: "duration_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_id",
                table: "subscriptions",
                column: "id",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_telegram_bot_durations_id",
                table: "telegram_bot_durations",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_telegram_bots_id",
                table: "telegram_bots",
                column: "id",
                unique: true);
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
