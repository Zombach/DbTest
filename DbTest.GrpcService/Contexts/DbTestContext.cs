using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace DbTest.GrpcService.Contexts;

public class DbTestContext : DbContext
{
    public DbTestContext(DbContextOptions<DbTestContext> options) : base(options)
    {
    }

    // DbSet-ы представляют таблицы в базе данных
    public DbSet<SelectSequence> Sequences { get; set; }
    // Добавьте другие DbSet-ы для других сущностей

    public int InsertTVP(IEnumerable<string> sequences)
    {
        SqlMetaData[] metaData = { new("Seq", SqlDbType.NVarChar, -1) };
        IEnumerable<SqlDataRecord> records = sequences.Select(seq =>
        {
            SqlDataRecord record = new(metaData);
            record.SetSqlString(0, seq);
            return record;
        });

        SqlParameter tvpParameter = GetSqlParameter("dbo.InsertSequenceTVP", records);
        return this.Database.ExecuteSqlRaw("EXEC InsertTVP @TVP", tvpParameter);
    }

    public IEnumerable<SelectSequence> SelectTVP(IEnumerable<int> tvpItems)
    {
        SqlMetaData[] metaData = { new("Id", SqlDbType.Int) };
        IEnumerable<SqlDataRecord> records = tvpItems.Select(id =>
        {
            SqlDataRecord record = new(metaData);
            record.SetSqlInt32(0, id);
            return record;
        });

        SqlParameter tvpParameter = GetSqlParameter("dbo.SelectSequenceTVP", records);
        return this.Database.SqlQueryRaw<SelectSequence>("EXEC SelectTVP @TVP", tvpParameter);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Здесь вы можете настроить отношения между сущностями и другие настройки
    }

    private SqlParameter GetSqlParameter(string nameTVP, object value) => new()
    {
        ParameterName = "@TVP",
        SqlDbType = SqlDbType.Structured,
        TypeName = nameTVP,
        Value = value
    };
}