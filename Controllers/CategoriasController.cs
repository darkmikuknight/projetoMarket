using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;

namespace projetoMarket.Controllers
{
    public class CategoriasController : Controller
    {
        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria){
            return Content("teste");
        }
    }
}