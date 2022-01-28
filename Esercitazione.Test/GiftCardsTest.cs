using Esercitazione.Core.BusinessLayers;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Repositories;
using Esercitazione.Core.Mock.Storage;
using Esercitazione.Core.Repositories;
using System;
using Xunit;

namespace Esercitazione.Test
{
    public class GiftCardsTest
    {
        [Fact]
        public void ShouldCreateGiftCardBeOkWithIncrementedElements()
        {
            //ARRANGE
            GiftCardMockStorage.Initialize();

            IGiftCardRepository giftCardRepo = new GiftCardRepositoryMock();

            MainBusinessLayer bl = new MainBusinessLayer(giftCardRepo);

            GiftCard newGiftCard = new GiftCard()
            {
                DataScadenza = new DateTime(2022, 05, 30),
                Mittente = "Federico Melis",
                Destinatario = "Mario Piras",
                Importo = 50,
                Messaggio = "Auguri Mario"
            };

            //ACT
            var result = bl.CreateGiftCard(newGiftCard);

            Assert.True(result.Success);
        }
    }
}