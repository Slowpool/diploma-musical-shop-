using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RedundantTypeIdRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "accessories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "accessories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_accessories_specific_type_type_id",
                table: "accessories",
                column: "type_id",
                principalTable: "specific_type",
                principalColumn: "specific_type_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
