using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyBook.Data.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Copies = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PenaltyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Borrowings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateToReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Returned = table.Column<bool>(type: "bit", nullable: false),
                    PenaltyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrowings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrowings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Copies", "Genre", "Score", "Title", "Year" },
                values: new object[,]
                {
                    { 10001, "J. R. R. Tolkien", 1, "Fantasia", 8.8m, "La comunidad del anillo", 1954 },
                    { 10002, "Arturo Perez Reverte", 1, "Novela historica", 7.44m, "El capitan alatriste", 1996 },
                    { 10003, "Mary Shelley", 2, "Novela gotica", 7.8m, "Frankenstein", 1818 },
                    { 10004, "J. K. Rowling", 1, "Fantasia", 8m, "Harry Potter y la piedra filosofal", 1997 },
                    { 10005, "Laura Gallego Garcia", 3, "Fantasia", 7.2m, "El libro de los portales", 2013 },
                    { 10006, "Patrick Rothfuss", 4, "Fantasia", 8.1m, "El nombre del viento", 2007 },
                    { 10007, "John Grisham", 2, "Thriller", 7.7m, "El informe pelicano", 1992 },
                    { 10008, "Stephen King", 1, "Terror", 7.9m, "El resplandor", 1977 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PenaltyFee", "RegistrationDate", "Role" },
                values: new object[,]
                {
                    { 1111, "admin@admin.com", "Admin", "admin", 0m, new DateTime(2024, 4, 3, 17, 0, 0, 0, DateTimeKind.Unspecified), "admin" },
                    { 1112, "ignaciocasaus1cns@gmail.com", "Ignacio", "patata", 0m, new DateTime(2024, 4, 4, 19, 0, 0, 0, DateTimeKind.Unspecified), "user" },
                    { 1113, "emaildealex@gmail.com", "Alex", "pimiento", 0m, new DateTime(2024, 4, 5, 18, 30, 0, 0, DateTimeKind.Unspecified), "user" },
                    { 1114, "pepe@pepe.com", "Pepe", "pepe", 10m, new DateTime(2024, 4, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "user" }
                });

            migrationBuilder.InsertData(
                table: "Borrowings",
                columns: new[] { "Id", "BookId", "BorrowingDate", "DateToReturn", "PenaltyFee", "Returned", "ReturnedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 10001, new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1111 },
                    { 2, 10003, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, true, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1112 },
                    { 3, 10004, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, false, null, 1111 },
                    { 4, 10005, new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1113 },
                    { 5, 10008, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, true, new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1114 },
                    { 6, 10006, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, false, null, 1113 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_BookId",
                table: "Borrowings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_UserId",
                table: "Borrowings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrowings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
