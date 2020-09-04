using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Data;
using projetoMarket.Models;
using System.Linq;

namespace projetoMarket.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext database;
        public FornecedoresController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemp){
            
            if(ModelState.IsValid){
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemp.Nome;
                fornecedor.Email = fornecedorTemp.Email;
                fornecedor.Telefone = fornecedorTemp.Telefone;
                fornecedor.Status = true;

                database.Fornecedores.Add(fornecedor);
                database.SaveChanges();

                return RedirectToAction("Fornecedores", "Gestao");
            }
            else{
                return View("../Gestao/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemp){
            
            if(ModelState.IsValid){
                Fornecedor fornecedor = database.Fornecedores.First(forn => forn.Id == fornecedorTemp.Id);
                fornecedor.Nome = fornecedorTemp.Nome;
                fornecedor.Email = fornecedorTemp.Email;
                fornecedor.Telefone = fornecedorTemp.Telefone;

                database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else{
                return View("../Gestao/EditarFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id){

            if(id > 0){
                var fornecedor = database.Fornecedores.First(forn => forn.Id == id);
                fornecedor.Status = false;
                database.SaveChanges();
            }
            
            return RedirectToAction("Fornecedores", "Gestao");
        }
    }
}