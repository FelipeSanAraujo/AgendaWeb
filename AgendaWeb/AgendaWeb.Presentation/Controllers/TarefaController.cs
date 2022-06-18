using AgendaWeb.Infra.Data.Entities;
using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWeb.Presentation.Controllers
{
    public class TarefaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(TarefaCadastroModel model, [FromServices] ITarefaRepository tarefaRepository)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var tarefa = new Tarefa();

                    tarefa.IdTarefa = Guid.NewGuid();
                    tarefa.Nome = model.Nome;
                    tarefa.Data = DateTime.Parse(model.Data);
                    tarefa.Hora = TimeSpan.Parse(model.Hora);
                    tarefa.Descricao = model.Descricao;
                    tarefa.Prioridade = int.Parse(model.Prioridade);
                    
                    tarefaRepository.Inserir(tarefa);

                    TempData["MensagemSucesso"] = $"A Tarefa '{tarefa.Nome}' foi cadastrada com sucesso!";

                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar tarefa: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = $"Ocorreram erros de validação no preenchimento dos dados, por favor verifique.";
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(TarefaConsultaModel model, [FromServices] ITarefaRepository tarefaRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dataMin = DateTime.Parse(model.DataMin);
                    var dataMax = DateTime.Parse(model.DataMax);

                    model.Tarefas = tarefaRepository.ConsultarPorData(dataMin, dataMax);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao consultar tarefas: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = $"Ocorreram erros de validação no preenchimento dos dados, por favor verifique.";
            }

            return View(model);
        }

        public IActionResult Exclusao(Guid id, [FromServices] ITarefaRepository tarefaRepository)
        {
            try
            {
                var tarefa = new Tarefa();
                tarefa.IdTarefa = id;

                tarefaRepository.Excluir(tarefa);

                TempData["MensagemSucesso"] = $"Tarefa excluída com sucesso!";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao excluir a tarefa: {e.Message}";
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id, [FromServices] ITarefaRepository tarefaRepository)
        {
            var model = new TarefaEdicaoModel();

            try
            {
                var tarefa = tarefaRepository.ObterPorId(id);

                model.IdTarefa = tarefa.IdTarefa;
                model.Nome = tarefa.Nome;
                model.Data = tarefa.Data.ToString("yyyy-MM-dd");
                model.Hora = tarefa.Hora.ToString(@"hh\:mm");
                model.Descricao = tarefa.Descricao;
                model.Prioridade = tarefa.Prioridade.ToString();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Erro ao obter a tarefa: {e.Message}";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(TarefaEdicaoModel model, [FromServices] ITarefaRepository tarefaRepository)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var tarefa = new Tarefa();

                    tarefa.IdTarefa = model.IdTarefa;
                    tarefa.Nome = model.Nome;
                    tarefa.Data = DateTime.Parse(model.Data);
                    tarefa.Hora = TimeSpan.Parse(model.Hora);
                    tarefa.Descricao = model.Descricao;
                    tarefa.Prioridade = int.Parse(model.Prioridade);

                    tarefaRepository.Alterar(tarefa);

                    TempData["MensagemSucesso"] = $"A Tarefa '{tarefa.Nome}' foi atualizada com sucesso!";
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Erro ao atualizar a tarefa: {e.Message}";
                }
            }

            return View();
        }
    }
}
