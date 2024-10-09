
using FoodDelivery.Models;
using FoodDelivery.Services;
using System;

namespace FoodDeliveryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FoodServices foodServices = new FoodServices();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Food");
                Console.WriteLine("2. Update Food");
                Console.WriteLine("3. Delete Food");
                Console.WriteLine("4. Show Available Foods");
                Console.WriteLine("5. Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddFoodItem(foodServices);
                        break;
                    case 2:
                        UpdateFoodItem(foodServices);
                        break;
                    case 3:
                        DeleteFoodItem(foodServices);
                        break;
                    case 4:
                        DisplayAvailableFoods(foodServices);
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine(); 
            }
        }

        static void AddFoodItem(FoodServices foodServices)
        {
            Console.WriteLine("Enter Food Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Food Price:");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Is the Food Available? (true/false):");
            bool isAvailable = Convert.ToBoolean(Console.ReadLine());

            Food newFood = new Food
            {
                Name = name,
                Price = price,
                IsAvailable = isAvailable
            };

            bool success = foodServices.AddFood(newFood);

            if (success)
            {
                Console.WriteLine("Food added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add food.");
            }
        }

        static void UpdateFoodItem(FoodServices foodServices)
        {
            Console.WriteLine("Enter Food Name to update:");
            string name = Console.ReadLine();

            Food existingFood = foodServices.GetAllFoods().FirstOrDefault(f => f.Name == name);

            if (existingFood == null)
            {
                Console.WriteLine($"Food '{name}' not found.");
                return;
            }

            Console.WriteLine("Enter Updated Price:");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Is the Food Available? (true/false):");
            bool isAvailable = Convert.ToBoolean(Console.ReadLine());

            existingFood.Price = price;
            existingFood.IsAvailable = isAvailable;

            bool success = foodServices.UpdateFood(existingFood);

            if (success)
            {
                Console.WriteLine("Food updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update food.");
            }
        }
        static void DeleteFoodItem(FoodServices foodServices)
        {
            Console.WriteLine("Enter Food Name to delete:");
            string name = Console.ReadLine();

            bool success = foodServices.DeleteFood(name);

            if (success)
            {
                Console.WriteLine("Food deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete food '{name}'.");
            }
        }
        static void DisplayAvailableFoods(FoodServices foodServices)
        {
            var foods = foodServices.GetAvailableFoods();

            if (foods.Count == 0)
            {
                Console.WriteLine("No available foods.");
            }
            else
            {
                foreach (var food in foods)
                {
                    Console.WriteLine($"Name: {food.Name}, Price: {food.Price}, Available: {food.IsAvailable}");
                }
            }
        }
    }
}
