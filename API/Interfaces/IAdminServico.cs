using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Context;
using minimal_api.Entities;

namespace minimal_api.Interfaces
{
    public interface IAdminServico
    {

        Admin? Login(LoginDTO loginDTO);

        Admin Incluir(Admin admin);

        List<Admin> Todos(int? pagina);

        Admin? BuscaPorId(int id);

      
    }
}