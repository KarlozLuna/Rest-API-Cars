using apicourseproject1.Data;
using apicourseproject1.Data.Entities;
using apicourseproject1.Data.Interfaces;
using Dapper;

namespace apicourseproject1.Data.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DatabaseConnectionFactory databaseConnectionFactory;

    public CarRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        this.databaseConnectionFactory = databaseConnectionFactory;
    }


    public async Task<List<CarFlat?>> Get(int id)
    {
        var query = "select * from car c left join options o on o.car_id= c.id where c.id = @id";
        using var db = databaseConnectionFactory.GetConnection();
        return (await db.QueryAsync<CarFlat>(query, new { id })).ToList();
    }

    private async Task<T?> QueryFirstOrDefaultAsync<T>(
    string sql,
    object param)
    {
        using var db = databaseConnectionFactory.GetConnection();

        return await db.QueryFirstOrDefaultAsync<T>(sql, param);
    }
    public async Task<IEnumerable<Car>> Get(
        bool returnDeletedRecords,
        int pageNumber,
        int pageSize)
    {
        var builder = new SqlBuilder();
        var sqlTemplate = builder.AddTemplate(
            "SELECT * FROM car " +
            "/**where**/ " +
            "ORDER BY id OFFSET @PageNumber ROWS " +
            "FETCH NEXT @PageSize ROWS ONLY");
        if (!returnDeletedRecords)
        {
            builder.Where("is_deleted=0");
        }
        var offset = (pageNumber - 1) * pageSize;
        using var db = databaseConnectionFactory.GetConnection();
        return await db.QueryAsync<Car>(sqlTemplate.RawSql,
            new { PageNumber = pageNumber, PageSize = pageSize });
    }
    public async Task<int> UpsertAsync(Car car)
    {
        using var db = databaseConnectionFactory.GetConnection();

        var sql = @"
        DECLARE @InsertedRows AS TABLE (Id int);

        MERGE INTO Car AS target
        USING (
            SELECT 
                @Id AS Id,
                @make AS make,
                @citympg AS citympg,
                @cylindernumber AS cylindernumber,
                @enginesize AS enginesize,
                @horsepower AS horsepower,
                @carbody AS carbody,
                @peakrpm AS peakrpm,
                @price AS price,
                ISNULL(@is_deleted, 0) AS is_deleted
        ) AS source

        ON target.Id = source.Id

        WHEN MATCHED THEN
            UPDATE SET
                make = source.make,
                citympg = source.citympg,
                cylindernumber = source.cylindernumber,
                enginesize = source.enginesize,
                horsepower = source.horsepower,
                carbody = source.carbody,
                peakrpm = source.peakrpm,
                price = source.price,
                is_deleted = source.is_deleted

        WHEN NOT MATCHED THEN
            INSERT (
                make,
                citympg,
                cylindernumber,
                enginesize,
                horsepower,
                carbody,
                peakrpm,
                price,
                is_deleted
            )
            VALUES (
                source.make,
                source.citympg,
                source.cylindernumber,
                source.enginesize,
                source.horsepower,
                source.carbody,
                source.peakrpm,
                source.price,
                source.is_deleted
            )
            OUTPUT inserted.Id INTO @InsertedRows;

        SELECT Id FROM @InsertedRows;
    ";

        var newId = await db.QuerySingleOrDefaultAsync<int>(sql, car);

        return newId == 0 ? car.Id : newId;
    }
    public async Task<int> DeleteAsync(int id)
    {
        using var db = databaseConnectionFactory.GetConnection();
        var query = "UPDATE car SET Is_Deleted = 1 WHERE Id = @Id";
        return await db.ExecuteAsync(query, new { Id = id });
    }
}