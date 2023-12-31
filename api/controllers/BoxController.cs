﻿using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Box_Factory.controllers;

[ApiController]

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
            var createdBox = _service.CreateBox(box.BoxName, box.Price, box.BoxWidth, box.BoxLength, box.BoxHeight,
                box.BoxThickness, box.BoxColor, box.BoxImgUrl);
            return createdBox;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"An error occurred: {e.Message}");
        }
    }

    [HttpGet]
    [Route("api/boxes")]
    public IEnumerable<Box> GetBoxes()
    {
        IEnumerable<Box> boxes = new List<Box>();
        try
        {
           
            boxes = _service.GetBoxes();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return boxes;
    }

    [HttpDelete]
    [Route("/api/delete/{boxId}")]
    public void DeleteBox(int boxId)
    { 
        try 
        { 
            _service.DeleteBox(boxId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [HttpGet]
    [Route("/api/boxes/{boxId}")]
    public Box GetBoxById(int boxId)
    {

        Box box = null;
        try
        {
            box = _service.GetBoxById(boxId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return box;
    }
    


    [HttpPut]
    [Route("/api/update/{boxId}")]
    public IActionResult UpdateBox(int boxId, [FromBody] Box updatedBox)
    {
        try
        {
            var existingBox = _service.GetBoxById(boxId);
            if(existingBox == null)
            {
                return NotFound(new { Message = "Box with given ID not found." });
            }

            updatedBox.Id = boxId;
            var updated = _service.UpdateBox(updatedBox);
            if (updated != null)
            {
                return Ok(updated);
            }
            else
            {
                return NotFound(new { Message = "Box could not be updated." });
            }
        }
        catch (Exception e)
        {
            // Log the exception
            return BadRequest(new { Message = "An error occurred while updating the box" });
        }
    }

    [HttpGet]
    [Route("api/search")]
    public IActionResult SearchBoxes([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length < 1)
        {
            return BadRequest("Search term must be at least 2 characters.");
        }

        return Ok(_service.SearchBoxes(searchTerm));
    }
    
}