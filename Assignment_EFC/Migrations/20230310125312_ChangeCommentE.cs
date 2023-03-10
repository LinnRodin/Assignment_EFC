using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_EFC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommentE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CustomerEntityId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerEntityId",
                table: "Comments",
                column: "CustomerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Customers_CustomerEntityId",
                table: "Comments",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Customers_CustomerEntityId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CustomerEntityId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CustomerEntityId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
