using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            var valorNegativo = -100;

            Assert.Throws<System.ArgumentException>(
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
