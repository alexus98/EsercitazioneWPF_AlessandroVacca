using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.WPF.Messaging.GiftCards
{
    public class ShowUpdateGiftCardMessage
    {
        public GiftCard giftCard { get; set; }
    }
}
