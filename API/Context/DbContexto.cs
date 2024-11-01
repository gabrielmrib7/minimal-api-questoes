using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Entities;

namespace minimal_api.Context
{
    public class DbContexto : DbContext
    {

        private readonly IConfiguration _configuration;
        public DbContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Admin> Admins {get; set;} = default!;
        public DbSet<Questao> Questoes {get; set;} = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin {
                    Id = 1,
                    Email = "adm@teste.com",
                    Senha = "123456",
                    Perfil = EnumPerfil.Adm
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var stringConexao = _configuration.GetConnectionString("mysql")?.ToString();

                if(!string.IsNullOrEmpty(stringConexao))
                    {
                        optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
                    }
        }
           
        }
    }
}