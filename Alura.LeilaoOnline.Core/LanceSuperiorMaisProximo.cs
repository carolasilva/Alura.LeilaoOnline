﻿using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class LanceSuperiorMaisProximo : IModalidadeAvaliacao
    {
        public double ValorDestino { get; }

        public LanceSuperiorMaisProximo(double valorDestino)
        {
            this.ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances.DefaultIfEmpty(new Lance(null, 0))
                    .Where(l => l.Valor > ValorDestino)
                    .OrderBy(l => l.Valor)
                    .FirstOrDefault();
        }
    }
}
