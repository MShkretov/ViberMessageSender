using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViberMessageSenderAPI.Migrations
{
    /// <inheritdoc />
    public partial class connect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdReceiver",
                table: "PhoneSenders",
                newName: "PhoneReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneSenders_PhoneReceiverId",
                table: "PhoneSenders",
                column: "PhoneReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneSenders_PhoneReceivers_PhoneReceiverId",
                table: "PhoneSenders",
                column: "PhoneReceiverId",
                principalTable: "PhoneReceivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneSenders_PhoneReceivers_PhoneReceiverId",
                table: "PhoneSenders");

            migrationBuilder.DropIndex(
                name: "IX_PhoneSenders_PhoneReceiverId",
                table: "PhoneSenders");

            migrationBuilder.RenameColumn(
                name: "PhoneReceiverId",
                table: "PhoneSenders",
                newName: "IdReceiver");
        }
    }
}
