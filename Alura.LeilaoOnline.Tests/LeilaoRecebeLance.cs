using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[] {  800, 900 })]
        [InlineData(4, new double[] { 800, 900, 1000, 100 })]
        [InlineData(1, new double[] { 8 })]
        [InlineData(0, new double[] { })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new MaiorLance();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for(int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1000);

            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(new double[] { 200, 300, 400, 500 })]
        [InlineData(new double[] { 200 })]
        [InlineData(new double[] { 200, 300, 400 })]
        [InlineData(new double[] { 200, 300, 400, 500, 600, 700 })]
        public void QtdePermaneceZeroDadoQuePregaoNaoFoiIniciado(double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new MaiorLance();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano de Tal", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            Assert.Empty(leilao.Lances);
        }
    
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            IModalidadeAvaliacao modalidade = new MaiorLance();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano de Tal", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
