using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers;

public class EquipamentoController : Controller
{
    private readonly IRepositorio<Equipamento> repositorioEquipamento;
    private readonly IRepositorio<Fabricante> repositorioFabricante;

    public EquipamentoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
        repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
    }

    // Ações / Operação CRUD
    [HttpGet]
    public ActionResult Listar()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<ListarEquipamentosViewModel> listarVms = new List<ListarEquipamentosViewModel>();

        foreach (Equipamento e in equipamentos)
        {
            ListarEquipamentosViewModel viewModel = new ListarEquipamentosViewModel(
                e.Id,
                e.Nome,
                e.PrecoAquisicao,
                e.DataFabricacao,
                e.Fabricante.Nome
            );

            listarVms.Add(viewModel);
        }

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Fabricantes = CarregarFabricantes();

        CadastrarEquipamentoViewModel cadastrarVm = new CadastrarEquipamentoViewModel(
            string.Empty,
            0,
            DateTime.Today,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(cadastrarVm.FabricanteId);

        if (!string.IsNullOrWhiteSpace(cadastrarVm.FabricanteId) && fabricante == null)
        {
            ModelState.AddModelError(
                nameof(cadastrarVm.FabricanteId),
                "Selecione um fabricante válido."
            );
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Fabricantes = CarregarFabricantes();

            return View(cadastrarVm);
        }

        Equipamento novoEquipamento = new Equipamento(
            cadastrarVm.Nome,
            cadastrarVm.PrecoAquisicao,
            cadastrarVm.DataFabricacao,
            fabricante!
        );

        repositorioEquipamento.Cadastrar(novoEquipamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(id);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        EditarEquipamentoViewModel editarVm = new EditarEquipamentoViewModel(
            id,
            equipamento.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao,
            equipamento.Fabricante.Id
        );

        ViewBag.Fabricantes = CarregarFabricantes();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarEquipamentoViewModel editarVm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(editarVm.FabricanteId);

        if (!string.IsNullOrWhiteSpace(editarVm.FabricanteId) && fabricante == null)
        {
            ModelState.AddModelError(
                nameof(editarVm.FabricanteId),
                "Selecione um fabricante válido."
            );
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Fabricantes = CarregarFabricantes();

            return View(editarVm);
        }

        Equipamento equipamentoAtualizado = new Equipamento(
            editarVm.Nome,
            editarVm.PrecoAquisicao,
            editarVm.DataFabricacao,
            fabricante!
        );

        repositorioEquipamento.Editar(editarVm.Id, equipamentoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(id);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        ExcluirEquipamentoViewModel excluirVm = new ExcluirEquipamentoViewModel(
            id,
            equipamento.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao,
            equipamento.Fabricante.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirEquipamentoViewModel excluirVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(excluirVm.Id);

        if (equipamento != null)
            repositorioEquipamento.Excluir(equipamento);

        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> CarregarFabricantes()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        List<SelectListItem> selecionarFabricantes = new List<SelectListItem>();

        foreach (Fabricante f in fabricantes)
        {
            SelectListItem selecionarFabricanteVm = new SelectListItem(
                f.Nome,
                f.Id
            );

            selecionarFabricantes.Add(selecionarFabricanteVm);
        }

        return selecionarFabricantes;
    }
}