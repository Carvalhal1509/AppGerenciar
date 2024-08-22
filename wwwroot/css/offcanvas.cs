using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.wwwroot.css
{
    public class offcanvas : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
