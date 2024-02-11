using Board.Infrastucture.DataAccess;

namespace Board.Host.DbMigrator
{
    /// <summary>
    /// Контест БД для мигратора.
    /// </summary>
    public class MigrationDbContext : BoardDbContext
    {
        public MigrationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
        {
        }
    }
}
