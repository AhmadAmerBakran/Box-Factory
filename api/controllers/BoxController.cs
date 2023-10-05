using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Box_Factory.controllers;

[ApiController]
[Route("[Controller]")]
public class BoxController : ControllerBase
{
    private readonly BoxService _service;

    public BoxController(BoxService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("/api/box")]
    public ActionResult<Box> CreateBox([FromBody] Box box)
    {
        try
        {
            var createdBox = _service.CreateBox(box.BoxName, box.Price, box.BoxWidth, box.BoxLenght, box.BoxHight,
                box.BoxThickness, box.BoxColor);
            return createdBox;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"An error occurred: {e.Message}");
        }
    }
}