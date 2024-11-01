using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;
using minimal_api.Context;
using minimal_api.Interfaces;

namespace minimal_api.Entities
{
    public class AdminServico : IAdminServico
    {
        private readonly DbContexto _contexto;
        public AdminServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public Admin? BuscaPorId(int id)
        {
            return _contexto.Admins.Where(a => a.Id == id).FirstOrDefault();
        }

        public Admin? Incluir(Admin admin)
        {
            _contexto.Admins.Add(admin);
            _contexto.SaveChanges();

            return admin;
        }

        public Admin? Login(LoginDTO loginDTO)
        {
            
            var adm = _contexto.Admins.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            
            return adm;
            
        }

        public List<Admin> Todos(int? pagina)
        {
            var admin = _contexto.Admins.AsQueryable();
            
            int itensPorPagina = 10;

            if(pagina != null)
            {            
            admin = admin.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }
            return admin.ToList();
        }
    }
}