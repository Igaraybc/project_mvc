using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_mvc.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO \"Categories\" (\"CategoryName\", \"Description\") "+
                        "Values('Natural', 'Lanche com igredientes integrais e naturais')");
            migrationBuilder.InsertData("Categories", columns: ["CategoryName", "Description"] , values: ["Normal", "Lanche com ingredientes normais"]);
         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Categories\"");
        }
    }
}
