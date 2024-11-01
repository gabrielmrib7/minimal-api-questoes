using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Entities
{
    public class AdminDTO
    {
        public string Email { get; set; }
        
        public string Senha { get; set; }
        
        public EnumPerfil Perfil { get; set; }
        
    }
}