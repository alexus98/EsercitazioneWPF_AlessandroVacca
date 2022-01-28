using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Repositories
{
    public interface IGiftCardRepository
    {
        IList<GiftCard> FetchAll();
        void Create(GiftCard giftCard);
        void Update(GiftCard oldGiftCard, GiftCard newGiftCard);
        void Delete(GiftCard giftCard);
    }
}
