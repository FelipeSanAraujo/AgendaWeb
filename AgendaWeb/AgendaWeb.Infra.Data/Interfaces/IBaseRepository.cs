namespace AgendaWeb.Infra.Data.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);

        List<TEntity> Consultar();
        TEntity ObterPorId(Guid id);
    }
}
