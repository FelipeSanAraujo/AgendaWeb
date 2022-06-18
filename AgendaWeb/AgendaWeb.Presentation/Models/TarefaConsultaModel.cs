using AgendaWeb.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaWeb.Presentation.Models
{
    public class TarefaConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataMax { get; set; }

        //Lista para exibir as tarefas na página de consulta (dentro de um GRID (table))
        public List<Tarefa>? Tarefas { get; set; }
    }
}
