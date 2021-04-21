using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantByName(string name);
        Restaurant GetRestaurantById(int id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        public List<Restaurant> Restaurants { get; set; }

        public InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>
            {
                new Restaurant {Id=1, Name="Grotto's Pizza", Location="Middletown", Cusine = CusineType.Italian},
                new Restaurant {Id=2, Name="Maharaja", Location="Newark", Cusine = CusineType.Indian},
                new Restaurant {Id=3, Name="Qdoba", Location="Baltimore", Cusine = CusineType.Mexican},
                new Restaurant {Id=4, Name="Sam's Pizza", Location="Middletown", Cusine = CusineType.Italian}

            };
        }
        
        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            return from r in this.Restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return Restaurants.SingleOrDefault(x => x.Id == id);
        }
    }
}
