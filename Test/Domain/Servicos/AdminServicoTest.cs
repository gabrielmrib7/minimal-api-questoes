using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Context;
using minimal_api.Entities;

namespace Test.Domain.Entidades;

[TestClass]
public class AdminServicoTest
{

    private DbContexto CriarContextoDeTeste()
    {
       
       var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
       var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }



    [TestMethod]
    public void TestSaveAdm()
    {

        var context = CriarContextoDeTeste(); 
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE admins");


        var adm = new Admin();
        adm.Email = "test@test.com";
        adm.Senha = "teste";
        adm.Perfil = EnumPerfil.Adm;
        adm.Id = 1;

        
        

        var admServico = new AdminServico(context);

        admServico.Incluir(adm);
        admServico.BuscaPorId(adm.Id);
       

        Assert.AreEqual(1, admServico.Todos(1).Count());       
       

        
    }

    [TestMethod]
    public void TestBuscaIdAdm()
    {

        var context = CriarContextoDeTeste(); 
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE admins");


        var adm = new Admin();
        adm.Email = "test@test.com";
        adm.Senha = "teste";
        adm.Perfil = EnumPerfil.Adm;
        adm.Id = 1;

        
        

        var admServico = new AdminServico(context);

        admServico.Incluir(adm);
        var admm = admServico.BuscaPorId(adm.Id);
       

        Assert.AreEqual(1, admm.Id);       
       

        
    }
}

// Starting test run
// [Passed] TestBuscaIdAdm
// [Passed] TestSaveAdm

// ==== Summary ====
// Passed!  - Failed:    0, Passed:    2, Skipped:    0, Total:    2, Duration: 1s