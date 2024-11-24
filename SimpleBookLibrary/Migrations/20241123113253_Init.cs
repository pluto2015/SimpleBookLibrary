using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Borrowers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(500)", nullable: false),
                    Created = table.Column<long>(type: "INTEGER", nullable: false),
                    Updated = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Borrowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(500)", nullable: false),
                    Created = table.Column<long>(type: "INTEGER", nullable: false),
                    Updated = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Books",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Name = table.Column<string>(type: "varchar(500)", nullable: false),
                    Publisher = table.Column<string>(type: "varchar(500)", nullable: true),
                    Author = table.Column<string>(type: "varchar(500)", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    PurchaseDateTime = table.Column<long>(type: "INTEGER", nullable: true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<string>(type: "varchar(36)", nullable: true),
                    Remark = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Created = table.Column<long>(type: "INTEGER", nullable: false),
                    Updated = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Books_TB_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "TB_Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_BorrowHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    BookId = table.Column<string>(type: "varchar(36)", nullable: true),
                    BorrowDateTime = table.Column<long>(type: "INTEGER", nullable: false),
                    BorrowerId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ReturnDateTime = table.Column<long>(type: "INTEGER", nullable: true),
                    BorrowCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<long>(type: "INTEGER", nullable: false),
                    Updated = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BorrowHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_BorrowHistories_TB_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "TB_Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TB_BorrowHistories_TB_Borrowers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "TB_Borrowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Books_DepartmentId",
                table: "TB_Books",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_BorrowHistories_BookId",
                table: "TB_BorrowHistories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_BorrowHistories_BorrowerId",
                table: "TB_BorrowHistories",
                column: "BorrowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_BorrowHistories");

            migrationBuilder.DropTable(
                name: "TB_Books");

            migrationBuilder.DropTable(
                name: "TB_Borrowers");

            migrationBuilder.DropTable(
                name: "TB_Departments");
        }
    }
}
