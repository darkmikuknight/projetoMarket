using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var produtos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto(){
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();

            return View();
        }

        public IActionResult EditarProduto(int id){

            var produto = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.Medicao = produto.Medicao;
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;

            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();

            return View(produtoView);
        }

        public IActionResult Promocoes(){
            var promocoes = database.Promocoes.Include(p => p.Produto).Where(i => i.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao(){
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id){

            var promocao = database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoID = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;
            ViewBag.Produtos = database.Produtos.ToList();
            
            return View(promocaoView);
        }

        public IActionResult Estoque(){
            var listaEstoques = database.Estoques.Include(e => e.Produto).ToList();
            return View(listaEstoques);
        }

        public IActionResult NovoEstoque(){
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarEstoque(){
            return Content("l");
        }
    }
}
