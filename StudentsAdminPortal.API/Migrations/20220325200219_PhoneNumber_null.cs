using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsAdminPortal.API.Migrations
{
    public partial class PhoneNumber_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditScore_Student_StudentId",
                table: "CreditScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditScore",
                table: "CreditScore");

            migrationBuilder.RenameTable(
                name: "CreditScore",
                newName: "Credits");

            migrationBuilder.RenameIndex(
                name: "IX_CreditScore_StudentId",
                table: "Credits",
                newName: "IX_Credits_StudentId");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "ContactInfo",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credits",
                table: "Credits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Student_StudentId",
                table: "Credits",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credits_Student_StudentId",
                table: "Credits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credits",
                table: "Credits");

            migrationBuilder.RenameTable(
                name: "Credits",
                newName: "CreditScore");

            migrationBuilder.RenameIndex(
                name: "IX_Credits_StudentId",
                table: "CreditScore",
                newName: "IX_CreditScore_StudentId");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "ContactInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditScore",
                table: "CreditScore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditScore_Student_StudentId",
                table: "CreditScore",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
