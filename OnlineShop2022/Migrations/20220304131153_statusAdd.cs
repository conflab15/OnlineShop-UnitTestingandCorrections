using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class statusAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEATcJ0Xc9r+3pUqbB7Byxw6BqmMz2Ja4/0ah89rht/qc6k8dwYE7MPFurHQkQ4nfHQ==", "df7b9f94-dfde-4a27-8bd4-1fbc86ebe449" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEDjB2ydvTIXT4D3bbScPBZNDz1yIv4k/IZ//bNhIvLR7b3HPAC1JuRF5F4XO5z6rpQ==", "b3dce930-90ca-481b-b878-0209c967a6aa" });
        }
    }
}
