using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;

namespace OdeToFood.Pages.Practice
{
    public class ActionDependencyModel : PageModel
    {
        public void OnGet([FromServices]IGuidService guidServices)
        {
        }
    }
}
