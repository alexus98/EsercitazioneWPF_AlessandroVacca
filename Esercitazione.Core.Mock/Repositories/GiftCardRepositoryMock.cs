using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Storage;
using Esercitazione.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Mock.Repositories
{
    public class GiftCardRepositoryMock : IGiftCardRepository
    {
        public void Create(GiftCard giftCard)
        {
            giftCard.Id = GiftCardMockStorage.GiftCards.Max(x => x.Id) + 1;
            GiftCardMockStorage.GiftCards.Add(giftCard);
        }

        public void Delete(GiftCard giftCard)
        {
            var e = GiftCardMockStorage.GiftCards.Where(x => x.Id == giftCard.Id).FirstOrDefault();
            GiftCardMockStorage.GiftCards.Remove(giftCard);
        }

        public IList<GiftCard> FetchAll()
        {
            return GiftCardMockStorage.GiftCards.ToList();
        }

        public void Update(GiftCard oldGiftCard, GiftCard newGiftCard)
        {
            var e = GiftCardMockStorage.GiftCards.Where(x => x.Id == newGiftCard.Id).FirstOrDefault();
            GiftCardMockStorage.GiftCards.Remove(oldGiftCard);
            GiftCardMockStorage.GiftCards.Add(newGiftCard);
        }
    }
}
