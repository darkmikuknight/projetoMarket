using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.Data;
using projetoMarket.DTO;

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

            var categorias = database.Categorias.Where(cat => cat.Status == true).ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria(){
            return View();
        }

        public IActionResult EditarCategoria(int id){

            var categoria = database.Categorias.First(cat => cat.Id == id);

            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;

            return View(categoriaView);
        }

        public IActionResult Fornecedores(){

            var fornecedores = database.Fornecedores.Where(forn => forn.Status == true).ToList();
            return View(fornecedores);
        }

        public IActionResult NovoFornecedor(){
            return View();
        }

        public IActionResult EditarFornecedor(int id){

            var fornecedor = database.Fornecedores.First(forn => forn.Id == id);
            FornecedorDTO fornecdorView = new FornecedorDTO();
            fornecdorView.Nome = fornecedor.Nome;
            fornecdorView.Id = fornecedor.Id;
            fornecdorView.Email = fornecedor.Email;
            fornecdorView.Telefone = fornecedor.Telefone;
            
            return View(fornecdorView);
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