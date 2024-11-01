using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Entities
{
    public record QuestaoDTO
    {
        
       
        
        public string Enunciado { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
       
        public EnumAlternativas AlternativaCorreta { get; set; }
        
        public string Materia { get; set; }
    }
    }
