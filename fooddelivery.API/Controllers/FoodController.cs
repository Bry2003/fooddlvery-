using FoodDelivery.Models;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fooddelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodServices _foodServices;

        public FoodController()
        {
            _foodServices = new FoodServices(); // Dependency injection should be used in production
        }

        // GET api/food
        [HttpGet]
        public ActionResult<List<Food>> GetAllFoods()
        {
            try
            {
                var foods = _foodServices.GetAllFoods();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/food
        [HttpPost]
        public ActionResult AddFood([FromBody] Food food)
        {
            try
            {
                if (food == null)
                {
                    return BadRequest("Food object is null");
                }

                bool result = _foodServices.AddFood(food);
                if (result)
                {
                    return CreatedAtAction(nameof(GetFoodByName), new { name = food.Name }, food);
                }
                else
                {
                    return StatusCode(500, "Failed to add food");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH api/food/{name}
        [HttpPatch("{name}")]
        public ActionResult UpdateFood(string name, [FromBody] Food food)
        {
            try
            {
                if (food == null || food.Name != name)
                {
                    return BadRequest("Invalid food object or name mismatch");
                }

                bool result = _foodServices.UpdateFood(food);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, $"Failed to update food with name {name}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/food/{name}
        [HttpDelete("{name}")]
        public ActionResult DeleteFood(string name)
        {
            try
            {
                bool result = _foodServices.DeleteFood(name);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Food with name {name} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Helper method to retrieve a food by name
        private ActionResult<Food> GetFoodByName(string name)
        {
            var foods = _foodServices.GetAllFoods();
            var food = foods.Find(f => f.Name == name);
            if (food == null)
            {
                return NotFound($"Food with name {name} not found");
            }
            return food;
        }
    }
}
