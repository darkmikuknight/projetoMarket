using Microsoft.AspNetCore.Mvc;
using projetoMarket.Models;
using projetoMarket.Data;

namespace projetoMarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;
        
        public EstoqueController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemp){

            database.Estoques.Add(estoqueTemp);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}