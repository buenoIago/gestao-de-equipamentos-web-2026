using System.Security.Cryptography.X509Certificates;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ChamadoController : Controller
{
    private readonly IRepositorio<Chamado> repositorioChamado;
    private readonly IRepositorio<Equipamento> repositorioEquipamento;
    public ChamadoController ()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioChamado =  new RepositorioChamadoEmArquivo(contexto);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarTodos();

        List<ListarChamadoViewModel> visualizarChamados = new List<ListarChamadoViewModel>();

        foreach (Chamado c in chamados)
        {
            ListarChamadoViewModel listarChamadoVm = new ListarChamadoViewModel(
                c.Id,
                c.Titulo,
                c.Equipamento.Nome,
                c.DataAbertura,
                c.TempoDecorrido,
                c.EstaConcluido
            );

            visualizarChamados.Add(listarChamadoVm);

        }

        return View(visualizarChamados);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Equipamentos = CarregarEquipamentos();

        CadastrarChamadoViewModel cadastrarVm = new CadastrarChamadoViewModel(string.Empty, null, string.Empty, false);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
    {
        Equipamento? equipamento =
            repositorioEquipamento.SelecionarPorId(cadastrarVm.EquipamentoId);

        if (!string.IsNullOrWhiteSpace(cadastrarVm.EquipamentoId) && equipamento == null)
        {
            ModelState.AddModelError(
                nameof(cadastrarVm.EquipamentoId),
                "Selecione um equipamento válido."
            );
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();

            return View(cadastrarVm);
        }

        Chamado novoChamado = new Chamado(
            cadastrarVm.Titulo,
            equipamento!,
            cadastrarVm.Descricao
        );

        repositorioChamado.Cadastrar(novoChamado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        EditarChamadoViewModel editarChamadoVm = new EditarChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.Equipamento.Id,
            false
        );

        ViewBag.Equipamentos = CarregarEquipamentos();

        return View(editarChamadoVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarChamadoViewModel editarVm)
    {
        Equipamento? equipamento =
            repositorioEquipamento.SelecionarPorId(editarVm.EquipamentoId);

        if (!string.IsNullOrWhiteSpace(editarVm.EquipamentoId) && equipamento == null)
        {
            ModelState.AddModelError(
                nameof(editarVm.EquipamentoId),
                "Selecione um equipamento válido."
            );
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Equipamentos = CarregarEquipamentos();

            return View(editarVm);
        }

        Chamado chamadoAtualizado = new Chamado(
            editarVm.Titulo,
            equipamento!,
            editarVm.EstaConcluido,
            editarVm.Descricao
        );

        repositorioChamado.Editar(editarVm.Id, chamadoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return RedirectToAction(nameof(Listar));

        ExcluirChamadoViewModel excluirVm = new ExcluirChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.Equipamento.Nome,
            chamado.DataAbertura,
            chamado.TempoDecorrido,
            chamado.EstaConcluido
        );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirChamadoViewModel excluirVm)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(excluirVm.Id);

        if (chamado != null)
            repositorioChamado.Excluir(chamado);

        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> CarregarEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<SelectListItem> selecionarEquipamentos = new List<SelectListItem>();

        foreach (Equipamento e in equipamentos)
        {
            SelectListItem selecionarEquipamentoVm = new SelectListItem(
                e.Nome,
                e.Id
            );

            selecionarEquipamentos.Add(selecionarEquipamentoVm);
        }

        return selecionarEquipamentos;
    }
}
