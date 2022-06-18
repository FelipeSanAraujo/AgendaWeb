using AgendaWeb.Infra.Data.Entities;
using AgendaWeb.Infra.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWeb.Infra.Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private string _connectionString;

        public TarefaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Inserir(Tarefa entity)
        {
            var query = @"
                    INSERT INTO TAREFA(
                        IDTAREFA,
                        NOME,
                        DATA,
                        HORA,
                        DESCRICAO,
                        PRIORIDADE)
                    VALUES(
                        @IdTarefa,
                        @Nome,
                        @Data,
                        @Hora,
                        @Descricao,
                        @Prioridade)
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Alterar(Tarefa entity)
        {
            var query = @"
                    UPDATE TAREFA
                    SET
                        NOME = @Nome,
                        DATA = @Data,
                        HORA = @Hora,
                        DESCRICAO = @Descricao,
                        PRIORIDADE = @Prioridade
                    WHERE
                        IDTAREFA = @IdTarefa
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Tarefa entity)
        {
            var query = @"
                    DELETE FROM TAREFA
                    WHERE IDTAREFA = @IdTarefa
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Tarefa> Consultar()
        {
            var query = @"
                    SELECT * FROM TAREFA
                    ORDER BY DATA
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Tarefa>(query)
                    .ToList();
            }
        }

        public Tarefa ObterPorId(Guid id)
        {
            var query = @"
                    SELECT * FROM TAREFA
                    WHERE IDTAREFA = @id
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Tarefa>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public List<Tarefa> ConsultarPorData(DateTime dataMin, DateTime dataMax)
        {
            var query = @"
                    SELECT * FROM TAREFA
                    WHERE DATA BETWEEN @dataMin AND @dataMax
                    ORDER BY DATA
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Tarefa>(query, new { dataMin, dataMax })
                    .ToList();
            }
        }
    }
}
