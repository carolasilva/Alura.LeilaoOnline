using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class MaiorLance : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao
                .Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(l => l.Valor)
                .LastOrDefault();
        }
    }
}
