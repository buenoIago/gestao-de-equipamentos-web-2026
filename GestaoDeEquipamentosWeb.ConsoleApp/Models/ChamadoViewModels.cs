namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarChamadoViewModel(
    string Id,
    string Titulo,
    string Equipamento,
    DateTime DataAbertura,
    int TempoDecorrido,
    bool EstaConcluido
);

public record CadastrarChamadoViewModel(
    string Titulo,
    string? Descricao,
    string EquipamentoId
);