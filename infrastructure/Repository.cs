using Dapper;
using infrastructure.interfaces;
using infrastructure.Models;
using Npgsql;

namespace infrastructure;

public class Repository : IBoxRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Box CreateBox(string boxName, double price, double boxWidth, 
        double boxLength, double boxHight, double boxThickness, string boxColor, string boxImgUrl)
    {
        var sql = @"INSERT INTO box_factory.boxes (BoxName, Price, 
                               BoxWidth, BoxLength, BoxHeight, BoxThickness, BoxColor, BoxImgUrl) VALUES (@Name, @Price, @Width, @Length, @Hight, @Thickness, @Color, @ImgUrl) RETURNING *;";

        var parameters = new
        {
            Name = boxName, Price = price, Width = boxWidth, Length = boxLength, Hight = boxHight,
            Thickness = boxThickness, Color = boxColor, ImgUrl = boxImgUrl
        };
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Box>(sql, parameters);
        }
    }

    public IEnumerable<Box> GetBoxes()
    {
        var sql = @"SELECT Id, BoxName, Price, 
                               BoxWidth, BoxLength, BoxHeight, BoxThickness, BoxColor, BoxImgUrl FROM box_factory.boxes;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Box>(sql);
        }
    }

    public Box DeleteBox(int boxId)
    {
        var sql = @"DELETE FROM box_factory.boxes WHERE id = @BoxId RETURNING *;";

        var parameters = new { BoxId = boxId };

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Box>(sql, parameters);
        }
    }

    public Box GetBoxById(int boxId)
    {
        var sql = @"SELECT * FROM box_factory.boxes WHERE id  = @Boxid; ";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingleOrDefault<Box>(sql, new { Boxid = boxId });
        }
    }

    public Box UpdateBox(Box updatedBox)
    {
        
        var sql = @"UPDATE box_factory.boxes SET BoxName = @Name, Price = @BoxPrice, BoxWidth = @Width, BoxLength = @Length, BoxHeight = @Hight, BoxThickness = @Thickness, BoxColor = @Color, BoxImgUrl = @ImgUrl WHERE id = @BoxId RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Box>(sql, new
            {
                BoxId = updatedBox.Id,
                Name = updatedBox.BoxName,
                BoxPrice = updatedBox.Price, Width = updatedBox.BoxWidth, Length = updatedBox.BoxLength,
                Hight = updatedBox.BoxHeight, Thickness = updatedBox.BoxThickness, Color = updatedBox.BoxColor, ImgUrl = updatedBox.BoxImgUrl
                
            });
        }
    }

    public IEnumerable<SearchBox> SearchBoxes(string term)
    {
      
       string sql = "SELECT id, BoxName, Price, BoxColor, BoxImgUrl FROM box_factory.boxes WHERE BoxName LIKE '%' || @Term || '%' OR Price::text LIKE '%' || @Term || '%' OR BoxColor LIKE '%' || @Term || '%';";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<SearchBox>(sql, new {Term = term});
        }
    }

}
