using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioFinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProceduresQueriesSearchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcedureSql1 = @"
                CREATE PROCEDURE dbo.sp_ObtenerTodoHistorialBusqueda
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM dbo.HistorialBusquedas
                    ORDER BY FechaConsulta DESC;
                END
            ";

            var createProcedureSql2 = @"
                CREATE PROCEDURE dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor
                    @NombreAutor NVARCHAR(255)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT TOP 1 * FROM dbo.HistorialBusquedas
                    WHERE Autor = @NombreAutor
                    ORDER BY FechaConsulta DESC;
                END
            ";

            migrationBuilder.Sql(createProcedureSql1);
            migrationBuilder.Sql(createProcedureSql2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.sp_ObtenerTodoHistorialBusqueda");
            migrationBuilder.Sql("DROP PROCEDURE dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor");
        }
    }
}
