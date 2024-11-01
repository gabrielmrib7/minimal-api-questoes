using minimal_api.Entities;

namespace Test.Domain.Entidades;

[TestClass]
public class AdminTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        var adm = new Admin();
        

        adm.Email = "test@test.com";
        adm.Senha = "teste";
        adm.Perfil = EnumPerfil.Adm;
        adm.Id = 1;

       

        Assert.AreEqual("test@test.com", adm.Email);
        Assert.AreEqual("teste", adm.Senha);
        Assert.AreEqual(EnumPerfil.Adm, adm.Perfil);
        Assert.AreEqual(1, adm.Id);
       

        //         Starting test run
        // [Passed] TestarGetSetPropriedades

        // ==== Summary ====
        // Passed!  - Failed:    0, Passed:    1, Skipped:    0, Total:    1, Duration: 237ms

        
    }
}