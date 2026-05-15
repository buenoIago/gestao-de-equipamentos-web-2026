using System;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers;

public class EquipamentoController :  Controller
{
    private readonly IRepositorio<Equipamento> repositorioEquipamento;
    public EquipamentoController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioEquipamento = 
            new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();
        return View(equipamentos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, decimal precoAquisicao, DateTime dataFabricacao, Fabricante fabricante)
    {
        Equipamento novoEquipamento = new Equipamento(nome, precoAquisicao, dataFabricacao, fabricante);

        repositorioEquipamento.Cadastrar(novoEquipamento);

        string listarStr = nameof(Listar);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(id);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        return View(equipamento);
    }

    [HttpPost]
    public ActionResult Editar(string id, string nome, decimal precoAquisicao, DateTime dataFabricacao, Fabricante fabricante)
    {
        Equipamento equipamentoAtualizado = new Equipamento(nome, precoAquisicao, dataFabricacao, fabricante);

        repositorioEquipamento.Editar(id, equipamentoAtualizado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir (string id)
    {
        Equipamento? equipamento =  repositorioEquipamento.SelecionarPorId(id);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        return View();
    }

    [HttpPost]
    public ActionResult ExcluirConfirmado(string id)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(id);

        if (equipamento == null)
            return RedirectToAction(nameof(Listar));

        repositorioEquipamento.Excluir(equipamento);

        return RedirectToAction(nameof(Listar));
    }
}
