using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
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
        
        public IEnumerable<Restaurant> GetByName(string name)
        {
            return from r in this.Restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return Restaurants.SingleOrDefault(x => x.Id == id);
        }

        public Restaurant Update(Restaurant input)
        {
            var restaurant = Restaurants.SingleOrDefault(x => x.Id == input.Id);
            if (restaurant != null)
            {
                restaurant.Name = input.Name;
                restaurant.Location = input.Location;
                restaurant.Cusine = input.Cusine;
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = Restaurants.Max(x => x.Id) + 1;
            Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restuarnt = Restaurants.FirstOrDefault(x => x.Id == id);
            if (restuarnt != null)
            {
                Restaurants.Remove(restuarnt);
            }

            return restuarnt;
        }

        public int GetRestaurantCount()
        {
            return Restaurants.Count;
        }
    }
}
