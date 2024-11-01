using minimal_api.Entities;

namespace Test.Domain.Entidades;

[TestClass]
public class QuestaoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
       
        var questao = new Questao();

        questao.Id = 1;
        questao.Enunciado = "teste";
        questao.Materia = "teste";
        questao.AlternativaCorreta = EnumAlternativas.A;
        questao.A = "teste";
        questao.B = "teste";
        questao.C = "teste";
        questao.D = "teste";
        
        Assert.AreEqual("teste", questao.Enunciado);
        Assert.AreEqual("teste", questao.Materia);
        Assert.AreEqual(EnumAlternativas.A, questao.AlternativaCorreta);
        Assert.AreEqual("teste", questao.A);
        Assert.AreEqual("teste", questao.B);
        Assert.AreEqual("teste", questao.C);
        Assert.AreEqual("teste", questao.D);

        //         Starting test run
        // [Passed] TestarGetSetPropriedades

        // ==== Summary ====
        // Passed!  - Failed:    0, Passed:    1, Skipped:    0, Total:    1, Duration: 56ms

       
    }
}