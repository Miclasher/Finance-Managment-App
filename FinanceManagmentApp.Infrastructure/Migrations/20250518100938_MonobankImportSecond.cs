using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagmentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MonobankImportSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mcc_TransactionTypes_TransactionTypeId",
                table: "Mcc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mcc",
                table: "Mcc");

            migrationBuilder.DropIndex(
                name: "IX_Mcc_TransactionTypeId",
                table: "Mcc");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Mcc");

            migrationBuilder.RenameTable(
                name: "Mcc",
                newName: "Mccs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mccs",
                table: "Mccs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MccTransactionType",
                columns: table => new
                {
                    MccsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MccTransactionType", x => new { x.MccsId, x.TransactionTypesId });
                    table.ForeignKey(
                        name: "FK_MccTransactionType_Mccs_MccsId",
                        column: x => x.MccsId,
                        principalTable: "Mccs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MccTransactionType_TransactionTypes_TransactionTypesId",
                        column: x => x.TransactionTypesId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsExpense = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypeTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MccTransactionTypeTemplate",
                columns: table => new
                {
                    MccsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTypeTemplatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MccTransactionTypeTemplate", x => new { x.MccsId, x.TransactionTypeTemplatesId });
                    table.ForeignKey(
                        name: "FK_MccTransactionTypeTemplate_Mccs_MccsId",
                        column: x => x.MccsId,
                        principalTable: "Mccs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MccTransactionTypeTemplate_TransactionTypeTemplates_TransactionTypeTemplatesId",
                        column: x => x.TransactionTypeTemplatesId,
                        principalTable: "TransactionTypeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MccTransactionType_TransactionTypesId",
                table: "MccTransactionType",
                column: "TransactionTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_MccTransactionTypeTemplate_TransactionTypeTemplatesId",
                table: "MccTransactionTypeTemplate",
                column: "TransactionTypeTemplatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MccTransactionType");

            migrationBuilder.DropTable(
                name: "MccTransactionTypeTemplate");

            migrationBuilder.DropTable(
                name: "TransactionTypeTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mccs",
                table: "Mccs");

            migrationBuilder.RenameTable(
                name: "Mccs",
                newName: "Mcc");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionTypeId",
                table: "Mcc",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mcc",
                table: "Mcc",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mcc_TransactionTypeId",
                table: "Mcc",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mcc_TransactionTypes_TransactionTypeId",
                table: "Mcc",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id");
        }
    }
}
