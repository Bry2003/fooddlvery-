using FoodDelivery.Data;
using FoodDelivery.Models;
using System.Collections.Generic;

namespace FoodDelivery.Services
{
    public class FoodServices
    {
        public  FoodData _foodData = new FoodData();

        public List<Food> GetAllFoods()
        {
            return _foodData.GetFoods();
        }

        public bool AddFood(Food food)
        {
            return _foodData.AddFood(food) > 0;
        }

        public bool UpdateFood(Food food)
        {
            return _foodData.UpdateFood(food) > 0;
        }

        public bool DeleteFood(string name)
        {
            return _foodData.DeleteFood(name) > 0;
        }

        public List<Food> GetAvailableFoods()
        {
            List<Food> allFoods = GetAllFoods();
            List<Food> availableFoods = new List<Food>();

            foreach (var food in allFoods)
            {
                if (food.IsAvailable)
                {
                    availableFoods.Add(food);
                }
            }

            return availableFoods;
        }
    }
}
