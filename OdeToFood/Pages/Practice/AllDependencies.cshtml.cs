using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Practice
{
    public class AllDependenciesModel : PageModel
    {
        private readonly IEnumerable<IGuidService> guidServices;

        public List<GuidModel> Values { get; set; }
        public AllDependenciesModel(IEnumerable<IGuidService> guidServices)
        {
            this.guidServices = guidServices;
        }
        public void OnGet()
        {
            Values = new List<GuidModel>();
            foreach (var item in guidServices)
            {
                Values.Add(new GuidModel(item.GetGuid()));
            }
        }
    }
}
