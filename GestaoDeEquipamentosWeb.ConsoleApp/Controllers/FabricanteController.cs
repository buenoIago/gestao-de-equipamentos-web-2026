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
    [HttpGet]
    public ActionResult Listar()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        return View(fabricantes);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string email, string telefone)
    {
        Fabricante novoFabricante = new Fabricante(nome, email, telefone);

        repositorioFabricante.Cadastrar(novoFabricante);
    
        string listarStr = nameof(Listar); // "Listar"

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if (fabricante == null)
            return RedirectToAction(nameof(Listar));

        return View(fabricante);
    }

    [HttpPost]
    public ActionResult Editar(string id, string nome, string email, string telefone)
    {
        Fabricante fabricanteAtualizado = new Fabricante(nome, email, telefone);

        repositorioFabricante.Editar(id, fabricanteAtualizado);

         return RedirectToAction(nameof(Listar));
    }
}
