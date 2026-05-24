using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using static GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado.IRepositorioChamado;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorioChamado
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}
