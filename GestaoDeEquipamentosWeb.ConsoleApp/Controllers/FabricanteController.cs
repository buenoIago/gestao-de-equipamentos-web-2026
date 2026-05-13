using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers;

// MVC -  Model , view 
public class FabricanteController : Controller
{
    private readonly IRepositorio<Fabricante> repositorioFabricante;
    public FabricanteController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

         repositorioFabricante = 
            new RepositorioFabricanteEmArquivo(contexto);
    }
    // GET: FabricanteController
    public ActionResult Listar()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        return View(fabricantes);
    }

}
