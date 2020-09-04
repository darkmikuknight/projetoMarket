using System;
using Microsoft.AspNetCore.Mvc;

namespace projetoMarket.Controllers
{
    public class GestaoController : Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}