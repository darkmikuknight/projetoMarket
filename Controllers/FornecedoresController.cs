using System;
using Microsoft.AspNetCore.Mvc;
using projetoMarket.DTO;
using projetoMarket.Data;
using projetoMarket.Models;

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
    }
}