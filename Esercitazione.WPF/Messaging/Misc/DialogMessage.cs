using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Esercitazione.WPF.Messaging.Misc
{
    public class DialogMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public MessageBoxImage Icon { get; set; }
        public MessageBoxButton Buttons { get; set; }
        public Action<MessageBoxResult> Callback { get; set; }
    }
}
