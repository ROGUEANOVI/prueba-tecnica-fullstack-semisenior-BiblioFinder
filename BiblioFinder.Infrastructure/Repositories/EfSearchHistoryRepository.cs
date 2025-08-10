using BiblioFinder.Application.Contracts.Repositories;
using BiblioFinder.Domain.Entities;
using BiblioFinder.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BiblioFinder.Infrastructure.Repositories
{
    public class EfSearchHistoryRepository : SearchHistoryRepository
    {
        private readonly BiblioFinderDbContext _dbContext;

        public EfSearchHistoryRepository(BiblioFinderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SearchHistory>> GetAllAsync()
        {
            return await _dbContext.SearchHistories
                .FromSqlRaw("EXEC dbo.sp_ObtenerTodoHistorialBusqueda")
                .ToListAsync();
        }

        public async Task<SearchHistory?> GetLastByAuthorAsync(string authorName)
        {
            var results = await _dbContext.SearchHistories
                .FromSqlInterpolated($"EXEC dbo.sp_ObtenerUltimoHistorialBusquedaPorAutor @NombreAutor = {authorName}")
                .ToListAsync();

            return results.FirstOrDefault();
        }

        public async Task AddRangeAsync(IEnumerable<SearchHistory> searchHistories)
        {
            var historyTable = new DataTable();
            historyTable.Columns.Add("Autor", typeof(string));
            historyTable.Columns.Add("Titulo", typeof(string));
            historyTable.Columns.Add("AnioPublicacion", typeof(int));
            historyTable.Columns.Add("Editorial", typeof(string));

            foreach (var record in searchHistories)
            {
                historyTable.Rows.Add(
                    record.Author,
                    record.Title,
                    (object)record.PublicationYear ?? DBNull.Value,
                    (object)record.Publisher ?? DBNull.Value
                );
            }

            var parameter = new SqlParameter("@HistoryData", SqlDbType.Structured)
            {
                TypeName = "dbo.TipoHistorialBusqueda",
                Value = historyTable
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.sp_InsertarHistorialBusqueda @HistoryData", parameter);
        }
    }
}
