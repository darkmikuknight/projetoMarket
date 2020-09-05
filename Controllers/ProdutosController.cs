using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Data;
using projetoMarket.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemp){

            if(ModelState.IsValid){

                var produto = database.Produtos.First(p => p.Id == produtoTemp.Id);
                produto.Nome = produtoTemp.Nome;
                produto.Categoria = database.Categorias.First(cat => cat.Id == produtoTemp.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(forn => forn.Id == produtoTemp.FornecedorID);
                produto.PrecoDeCusto = produtoTemp.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemp.PrecoDeVenda;
                produto.Medicao = produtoTemp.Medicao;

                database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else{
                // ViewBag.Categorias = database.Categorias.ToList();
                // ViewBag.Fornecedores = database.Fornecedores.ToList();
                // return View("../Gestao/NovoProduto");
                return View("../Gestao/EditarProduto");
            }
        }

        public IActionResult Deletar(int id){
            if(id > 0){
                var produto = database.Produtos.First(p => p.Id == id);
                produto.Status = false;
                database.SaveChanges();
            }

            return RedirectToAction("Produtos", "Gestao");
        }

        [HttpPost]
        public IActionResult Produto(int id){

            if(id > 0){
                var produto = database.Produtos.Where(p => p.Status == true).Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
                if(produto != null){
                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else{
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }
            else{
                Response.StatusCode = 404;
                return Json(null);
            }
        
        }
    }
}