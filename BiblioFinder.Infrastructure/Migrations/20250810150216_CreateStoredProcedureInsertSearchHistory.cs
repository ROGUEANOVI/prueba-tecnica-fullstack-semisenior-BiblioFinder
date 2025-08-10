using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioFinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedureInsertSearchHistory : Migration
    {
        //// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createTypeSql = @"
                CREATE TYPE dbo.TipoHistorialBusqueda AS TABLE(
                    Autor NVARCHAR(255) NOT NULL,
                    Titulo NVARCHAR(255) NOT NULL,
                    AnioPublicacion INT NULL,
                    Editorial NVARCHAR(255) NULL
                );";

            var createProcedureSql = @"
                CREATE PROCEDURE dbo.sp_InsertarHistorialBusqueda
                    @HistoryData dbo.TipoHistorialBusqueda READONLY
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO dbo.HistorialBusquedas (Autor, Titulo, AnioPublicacion, Editorial)
                    SELECT Autor, Titulo, AnioPublicacion, Editorial
                    FROM @HistoryData;
                END";

            migrationBuilder.Sql(createTypeSql);
            migrationBuilder.Sql(createProcedureSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.sp_InsertarHistorialBusqueda");

            migrationBuilder.Sql("DROP TYPE dbo.TipoHistorialBusqueda");
        }

    }
}
