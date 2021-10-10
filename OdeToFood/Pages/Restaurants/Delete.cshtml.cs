using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        public Restaurant Restaurant { get; set; }
        private readonly IRestaurantData restaurantData;
        private readonly IGuidService guidService;

        public DeleteModel(IRestaurantData restaurantData, IGuidService guidService)
        {
            this.restaurantData = restaurantData;
            this.guidService = guidService;
        }
        public IActionResult OnGet(int restaurantId)
        {
            var restaurant = restaurantData.GetById(restaurantId);
            if (restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            Restaurant = restaurant;
            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            try
            {
                var restaurant = restaurantData.Delete(restaurantId);
                restaurantData.Commit();

                if (restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }

                TempData["Message"] = $"{restaurant.Name} deleted !!";
                return RedirectToPage("./List");
            }
            catch(Exception ex)
            {
                throw new Exception($"Some error has occuured : {guidService.GetGuid()}");
            }

        }
    }
}
