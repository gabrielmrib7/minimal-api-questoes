using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Entities.ModelViews
{
    public struct Home
    {
        public string Mensagem {get => "Bem vindo a API de Questões - Minimal API";}
        public string Doc {get => "/swagger";}
    }
}