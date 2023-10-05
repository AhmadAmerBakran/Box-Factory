using Dapper;
using infrastructure.Models;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Box CreateBox(string boxName, double price, double boxWidth, 
        double boxLength, double boxHight, double boxThickness, string boxColor)
    {
        var sql = @"INSERT INTO box_factory.boxes (BoxName, Price, 
                               BoxWidth, BoxLength, BoxHeight, BoxThickness, BoxColor) VALUES (@Name, @Price, @Width, @Length, @Hight, @Thickness, @Color) RETURNING *;";

        var parameters = new
        {
            Name = boxName, Price = price, Width = boxWidth, Length = boxLength, Hight = boxHight,
            Thickness = boxThickness, Color = boxColor
        };
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Box>(sql, parameters);
        }
    }
}
