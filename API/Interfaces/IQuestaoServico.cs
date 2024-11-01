using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Entities;

namespace minimal_api.Interfaces
{
    public interface IQuestaoServico
    {
        List<Questao> Todos(int? pagina = 1, string? materia = null);
        Questao? BuscaPorId(int id);
        List<Questao> BuscaPorMateria(string materia);
        void Incluir(Questao questao);
        void Atualizar(Questao questao);
        void Apagar(Questao questao);
        
    }
}