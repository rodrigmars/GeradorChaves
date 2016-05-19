using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeradorChaves;

namespace UnitTestGeradorChaves
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stub = new Chaves();

            var colisao = stub.VerficaColisoesRngCrypto(8);

            Assert.IsFalse(colisao.Ocorrencia);
       } 
    }
}
