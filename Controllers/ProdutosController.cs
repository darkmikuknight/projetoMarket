using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Data;
using projetoMarket.Models;
using System.Linq;

namespace projetoMarket.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext database;
        
        public ProdutosController(ApplicationDbContext database){
            this.database = database;
        }
        
        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemp){

            if(ModelState.IsValid){

                Produto produto = new Produto();
                produto.Nome = produtoTemp.Nome;
                produto.Categoria = database.Categorias.First(cat => cat.Id == produtoTemp.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(forn => forn.Id == produtoTemp.FornecedorID);
                produto.PrecoDeCusto = produtoTemp.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemp.PrecoDeVenda;
                produto.Medicao = produtoTemp.Medicao;
                produto.Status = true;

                database.Produtos.Add(produto);
                database.SaveChanges();

                return RedirectToAction("Produtos", "Gestao");
            }
            else{

                ViewBag.Categorias = database.Categorias.ToList();
                ViewBag.Fornecedores = database.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }
    }
}