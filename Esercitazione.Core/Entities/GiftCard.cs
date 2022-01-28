using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Entities
{
    public class GiftCard
    {
        public int Id { get; set; }
        public string Mittente { get; set; }
        public string Destinatario { get; set; }
        public string Messaggio { get; set; }
        public double Importo { get; set; }
        public DateTime DataScadenza { get; set; }
    }
}
