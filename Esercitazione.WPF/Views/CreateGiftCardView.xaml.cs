using Esercitazione.WPF.Messaging.GiftCards;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Esercitazione.WPF.Views
{
    /// <summary>
    /// Interaction logic for CreateGiftCardView.xaml
    /// </summary>
    public partial class CreateGiftCardView : Window
    {
        public CreateGiftCardView()
        {
            InitializeComponent();

            Messenger.Default.Register<CloseCreateGiftCardMessage>(this, _ => Close());

            Closing += (s, e) =>
            {
                Messenger.Default.Unregister(this);
                Messenger.Default.Unregister(DataContext);
            };
        }
    }
}
