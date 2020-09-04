using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Models;
using projetoMarket.Data;
using System.Linq;

namespace projetoMarket.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext database;

        public PromocoesController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemp){

            if(ModelState.IsValid){
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemp.Nome;
                promocao.Porcentagem = promocaoTemp.Porcentagem;
                promocao.Produto = database.Produtos.First(p => p.Id == promocaoTemp.ProdutoID);
                promocao.Status = true;
                promocao.Porcentagem = promocaoTemp.Porcentagem;

                database.Promocoes.Add(promocao);
                database.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else{
                ViewBag.Produtos = database.Produtos.ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemp){
            
            if(ModelState.IsValid){

                var promocao = database.Promocoes.First(p => p.Id == promocaoTemp.Id);
                promocao.Nome = promocaoTemp.Nome;
                promocao.Porcentagem = promocaoTemp.Porcentagem;
                promocao.Produto = database.Produtos.First(prod => prod.Id == promocaoTemp.ProdutoID);
                
                database.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");

            }
            else{
                ViewBag.Produtos = database.Produtos.ToList();
                return RedirectToAction("Promocoes", "Gestao");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id){
            if(id > 0){

                var promocao = database.Promocoes.First(p => p.Id == id);
                promocao.Status = false;
                database.SaveChanges();
            }

            return RedirectToAction("Promocoes", "Gestao");
        }
    }
}