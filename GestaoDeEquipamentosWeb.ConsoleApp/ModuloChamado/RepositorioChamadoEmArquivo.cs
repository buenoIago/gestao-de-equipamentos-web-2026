using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using static GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado.IRepositorioChamado;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorioChamado
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto) { }

        public List<Chamado> FiltrarChamados(FiltroChamado filtro)
    {
        List<Chamado> chamadosFiltrados = new List<Chamado>();

        foreach (Chamado c in registros)
        {
            if (filtro(c))
                chamadosFiltrados.Add(c);
        }
        return chamadosFiltrados;
    }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}
