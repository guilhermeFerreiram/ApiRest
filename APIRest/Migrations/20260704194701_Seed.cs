using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRest.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Value", "CreatedAt", "UpdatedAt", "DeletedAt" },
                values: new object[,]
                {
                    { 1, "Notebook Ultrawide", 4599.90, DateTime.UtcNow, null, null },
                    { 2, "Mouse Ergonômico Wireless", 189.50, DateTime.UtcNow, null, null },
                    { 3, "Teclado Mecânico RGB", 349.00, DateTime.UtcNow, null, null },
                    { 4, "Monitor 27' 144Hz", 1250.00, DateTime.UtcNow, null, null },
                    { 5, "Cadeira Gamer Reclinável", 999.90, DateTime.UtcNow, null, null },
                    { 6, "Headset Gamer 7.1", 299.00, DateTime.UtcNow, null, null },
                    { 7, "Webcam Full HD 1080p", 220.00, DateTime.UtcNow, null, null },
                    { 8, "SSD NVMe 1TB", 420.00, DateTime.UtcNow, null, null },
                    { 9, "Memória RAM DDR4 16GB", 280.00, DateTime.UtcNow, null, null },
                    { 10, "Placa de Vídeo RTX 4060", 2499.00, DateTime.UtcNow, null, null },
                    { 11, "Processador Ryzen 7", 1350.00, DateTime.UtcNow, null, null },
                    { 12, "Placa-Mãe B550M", 780.00, DateTime.UtcNow, null, null },
                    { 13, "Fonte Modular 650W", 450.00, DateTime.UtcNow, null, null },
                    { 14, "Gabinete Mid Tower Vidro", 320.00, DateTime.UtcNow, null, null },
                    { 15, "Water Cooler 240mm", 399.90, DateTime.UtcNow, null, null },
                    { 16, "Suporte Articulado Monitor", 179.00, DateTime.UtcNow, null, null },
                    { 17, "Hub USB-C 5 em 1", 115.00, DateTime.UtcNow, null, null },
                    { 18, "Mousepad Extra Grande", 89.90, DateTime.UtcNow, null, null },
                    { 19, "Caixa de Som Bluetooth", 249.00, DateTime.UtcNow, null, null },
                    { 20, "Fita LED Smart Rgb 5m", 139.90, DateTime.UtcNow, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 20; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Products",
                    keyColumn: "Id",
                    keyValue: i);
            }
        }
    }
}
