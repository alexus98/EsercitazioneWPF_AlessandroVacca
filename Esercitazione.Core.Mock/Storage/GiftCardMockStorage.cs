using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Mock.Storage
{
    public static class GiftCardMockStorage
    {
        public static IList<GiftCard> GiftCards { get; set; }

        public static void Initialize()
        {
            GiftCards = new List<GiftCard>();
            GiftCards.Add(new GiftCard()
            {
                Id = 1,
                Mittente = "Mario Rossi",
                Destinatario = "Giuseppe Verdi",
                DataScadenza = new DateTime(2022,12,28),
                Importo = 50.00,
                Messaggio = "Auguri!"
            });

            GiftCards.Add(new GiftCard()
            {
                Id = 2,
                Mittente = "Marco Bianchi",
                Destinatario = "Francesco Pandolfi",
                DataScadenza = new DateTime(2021,05,11),
                Importo = 100.00,
                Messaggio = "Buon compleanno!"
            });

            GiftCards.Add(new GiftCard()
            {
                Id = 3,
                Mittente = "Diego Orrù",
                Destinatario = "Massimo Zedda",
                DataScadenza = new DateTime(2022, 07, 20),
                Importo = 25.00,
                Messaggio = "Auguroni!"
            });
        }
    }
}
