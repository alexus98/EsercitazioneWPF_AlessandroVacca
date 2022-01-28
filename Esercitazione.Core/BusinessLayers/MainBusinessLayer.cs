using Esercitazione.Core.Entities;
using Esercitazione.Core.Repositories;
using Esercitazione.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.BusinessLayers
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository _giftCardRepository;

        public MainBusinessLayer(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public IList<GiftCard> FetchAllGiftCards()
        {
            return _giftCardRepository.FetchAll();
        }

        public Response CreateGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                return new Response() { Success = false, Message = "Invalid Gift Card" };

            if (giftCard.DataScadenza < DateTime.Now.Date)
                return new Response() { Success = false, Message = "Expiration not valid" };

            if (giftCard.Importo < 0.0)
                return new Response() { Success = false, Message = "Gift Card's amount must be positive" };

            _giftCardRepository.Create(giftCard);

            return new Response() { Success = true, Message = $"Gift Card ID: {giftCard.Id} created" };
        }

        public Response DeleteGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                return new Response { Success = false, Message = "Invalid Gift Card" };

            if (giftCard.Id < 0)
                return new Response { Success = false, Message = "Invalid ID" };

            var giftCardToDelete = FetchAllGiftCards().Where(x => x.Id == giftCard.Id).FirstOrDefault();

            if (giftCardToDelete == null)
                return new Response { Success = false, Message = $"No Gift Card with ID: {giftCard.Id}" };

            _giftCardRepository.Delete(giftCardToDelete);

            return new Response { Success = true, Message = $"Gift Card deleted" };
        }

        public Response UpdateGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                return new Response() { Success = false, Message = "Incorrect entity" };

            var giftCardToUpdate = FetchAllGiftCards().Where(x => x.Id == giftCard.Id).FirstOrDefault();

            if (giftCardToUpdate == null)
                return new Response() { Success = false, Message = $"No gift card with ID: {giftCard.Id}" };

            if (giftCard.DataScadenza < DateTime.Now.Date || giftCard.DataScadenza < giftCardToUpdate.DataScadenza)
                return new Response() { Success = false, Message = "Expiration not valid" };

            _giftCardRepository.Update(giftCardToUpdate, giftCard);

            return new Response() { Success = true, Message = "Gift Card updated" };
        }
    }
}
