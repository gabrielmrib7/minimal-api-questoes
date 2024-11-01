using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using minimal_api.Context;
using minimal_api.Interfaces;

namespace minimal_api.Entities
{
    public class QuestaoServico : IQuestaoServico
    {

        private readonly DbContexto _contexto;

        public QuestaoServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Questao questao)
        {
            _contexto.Questoes.Remove(questao);
            _contexto.SaveChanges();
        }

        public void Atualizar(Questao questao)
        {
            _contexto.Questoes.Update(questao);
            _contexto.SaveChanges();
        }

        public Questao? BuscaPorId(int id)
        {
            return _contexto.Questoes.Where(x => x.Id == id).FirstOrDefault();
        }

        public  List<Questao> BuscaPorMateria(string materia)
        {
            var questao = _contexto.Questoes.AsQueryable();
            if(!string.IsNullOrEmpty(materia))
            {
                questao = questao.Where(q => EF.Functions.Like(q.Materia.ToLower(), $"%{materia.ToLower()}%"));
                return questao.ToList();
            }
            else
                return null;          
            
            
        }

        public void Incluir(Questao questao)
        {
            _contexto.Questoes.Add(questao);
            _contexto.SaveChanges();
        }

        public List<Questao> Todos(int? pagina = 1, string? materia = null)
        {
            var questao = _contexto.Questoes.AsQueryable();
            if(!string.IsNullOrEmpty(materia))
            {
                questao = questao.Where(q => EF.Functions.Like(q.Materia.ToLower(), $"%{materia.ToLower()}%"));
            }
            int itensPorPagina = 10;

            if(pagina != null)
            {            
            questao = questao.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }
            return questao.ToList();
        }
    }
}