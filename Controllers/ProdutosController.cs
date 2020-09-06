using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Data;
using projetoMarket.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                produto.PrecoDeCusto = float.Parse(produtoTemp.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemp.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
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
                produto.PrecoDeCusto = float.Parse(produtoTemp.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemp.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
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

                var produto = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).First(p => p.Id == id);

                if(produto != null){

                    Estoque estoque;
                    
                    try{
                        estoque = database.Estoques.First(e => e.Produto.Id == produto.Id);
                    }catch(Exception e){
                        estoque = null;
                    }

                    if(estoque != null){

                        Promocao promocao;

                        try{
                            promocao = database.Promocoes.Include(p => p.Produto).First(prom => prom.Produto.Id == produto.Id && prom.Status == true);
                        }catch(Exception e){
                            promocao = null;
                        }

                        if(promocao != null){
                            produto.PrecoDeVenda -= (produto.PrecoDeVenda * promocao.Porcentagem/100);
                        }

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
            else{
                Response.StatusCode = 404;
                return Json(null);
            }
        }

        [HttpPost]
        public IActionResult GerarVenda([FromBody] VendaDTO dados){
            //Gerando Venda//
            Venda venda = new Venda();
            venda.Total = dados.total;
            venda.Troco = dados.troco;
            venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.troco + dados.total;
            venda.Data = DateTime.Now;

            database.Vendas.Add(venda);
            database.SaveChanges();

            return Ok(new { msg = "Venda processada com sucesso!"});
        }

        public class SaidaDTO{
            public int produto {get; set;}
            public int quantidade {get; set;}
            public float subtotal {get; set;}
        }

        public class VendaDTO{
            public float total {get; set;}
            public float troco {get; set;}
            public SaidaDTO[] produtos {get; set;}
        }
    }
}