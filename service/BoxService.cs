using infrastructure;
using infrastructure.interfaces;
using infrastructure.Models;

namespace service;

public class BoxService
{
    private readonly IBoxRepository _repository;

    public BoxService(IBoxRepository repository)
    {
        _repository = repository;
    }

    public Box CreateBox(string boxName, double price, double boxWidth,
        double boxLength, double boxHight, double boxThickness, string boxColor, string boxImgUrl)
    {
        return _repository.CreateBox(boxName, price, boxWidth,
            boxLength, boxHight, boxThickness, boxColor, boxImgUrl);
    }

    public IEnumerable<Box> GetBoxes()
    {
        return _repository.GetBoxes();
    }

    public Box DeleteBox(int boxId)
    {
        return _repository.DeleteBox(boxId);
    }

    public Box GetBoxById(int boxId)
    {
        return _repository.GetBoxById(boxId);
    }

    public Box UpdateBox(Box updatedBox)
    {
        return _repository.UpdateBox(updatedBox);
    }

    public IEnumerable<SearchBox> SearchBoxes(string term)
    {
        return _repository.SearchBoxes(term);
    }
}