using AgendaWeb.Infra.Data.Entities;

namespace AgendaWeb.Infra.Data.Interfaces
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        List<Tarefa> ConsultarPorData(DateTime dataMin, DateTime dataMax);
    }
}
