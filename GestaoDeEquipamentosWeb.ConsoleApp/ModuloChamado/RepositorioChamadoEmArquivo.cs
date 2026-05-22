using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorioChamado
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto) { }

    public List<Chamado> SelecionarChamadosConcluidos()
    {
        List<Chamado> chamadosConcluidos = new List<Chamado>();

        foreach (Chamado c in registros)
        {
            if (c.EstaConcluido)
                chamadosConcluidos.Add(c);
        }
        return chamadosConcluidos;
    }

    public List<Chamado> SelecionarChamadosEmAberto()
    {
        List<Chamado> chamadosEmAberto = new List<Chamado>();

        foreach (Chamado c in registros)
        {
            if (!c.EstaConcluido)
                chamadosEmAberto.Add(c);
        }
        return chamadosEmAberto;
    }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}
