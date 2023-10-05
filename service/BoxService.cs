using infrastructure;
using infrastructure.Models;

namespace service;

public class BoxService
{
    private readonly Repository _repository;

    public BoxService(Repository repository)
    {
        _repository = repository;
    }

    public Box CreateBox(string boxName, double price, double boxWidth,
        double boxLength, double boxHight, double boxThickness, string boxColor)
    {
        return _repository.CreateBox(boxName, price, boxWidth,
            boxLength, boxHight, boxThickness, boxColor);
    }

    public IEnumerable<Box> GetBoxes()
    {
        return _repository.GetBoxes();
    }

    public Box DeleteBox(int boxId)
    {
        return _repository.DeleteBox(boxId);
    }
}