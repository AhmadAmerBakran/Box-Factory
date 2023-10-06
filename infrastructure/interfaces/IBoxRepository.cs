
using infrastructure.Models;

namespace infrastructure.interfaces
{
    public interface IBoxRepository
    {
        Box CreateBox(string boxName, double price, double boxWidth,
            double boxLength, double boxHeight, double boxThickness, string boxColor, string boxImgUrl);
        IEnumerable<Box> GetBoxes();
        Box DeleteBox(int boxId);
        Box GetBoxById(int boxId);
        Box UpdateBox(Box updatedBox);
        IEnumerable<SearchBox> SearchBoxes(string term);
    }
}