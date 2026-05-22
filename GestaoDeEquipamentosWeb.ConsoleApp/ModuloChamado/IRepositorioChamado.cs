using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public interface IRepositorioChamado : IRepositorio<Chamado>
{
    List<Chamado> FiltrarChamados(FiltroChamado filtro);
}
