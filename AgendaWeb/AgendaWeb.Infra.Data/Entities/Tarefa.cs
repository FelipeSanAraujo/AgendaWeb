namespace AgendaWeb.Infra.Data.Entities
{
    public class Tarefa
    {
        public Guid IdTarefa { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Descricao { get; set; }
        public int Prioridade { get; set; }
    }
}
