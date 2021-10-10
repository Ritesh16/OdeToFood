using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;
using static OdeToFood.Startup;

namespace OdeToFood.Pages.Practice
{
    public class SelectedDependencyModel : PageModel
    {
        private readonly IRepository repository;
        public string Value { get; set; }
        public SelectedDependencyModel(ServiceResolver serviceAccessor)
        {
            repository = serviceAccessor("2");
        }
        public void OnGet()
        {
            Value = repository.Get();
        }
    }
}
