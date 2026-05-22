using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public interface IRepositorioChamado : IRepositorio<Chamado>
{
    List<Chamado> SelecionarChamadosConcluidos();
    List<Chamado> SelecionarChamadosEmAberto();
}
