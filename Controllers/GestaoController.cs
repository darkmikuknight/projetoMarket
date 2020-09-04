using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.Data;

namespace projetoMarket.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext database;
        
        public GestaoController(ApplicationDbContext database){
            this.database = database;
        }
        public IActionResult Index(){
            return View();
        }

        public IActionResult Categorias(){

            var categorias = database.Categorias.ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria(){
            return View();
        }

        public IActionResult Fornecedores(){
            return View();
        }

        public IActionResult NovoFornecedor(){
            return View();
        }

        public IActionResult Produtos(){
            return View();
        }

        public IActionResult NovoProduto(){
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();

            return View();
        }
    }
}