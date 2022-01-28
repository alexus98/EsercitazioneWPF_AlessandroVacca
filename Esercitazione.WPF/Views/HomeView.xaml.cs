using Esercitazione.WPF.Messaging.GiftCards;
using Esercitazione.WPF.ViewModels;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
            Messenger.Default.Register<ShowCreateGiftCardMessage>(this, OnShowCreateEmployeeExecuted);
            Messenger.Default.Register<ShowUpdateGiftCardMessage>(this, OnShowUpdateEmployeeExecuted);
        }

        private void OnShowCreateEmployeeExecuted(ShowCreateGiftCardMessage obj)
        {
            CreateGiftCardView view = new CreateGiftCardView();
            CreateGiftCardViewModel vm = new CreateGiftCardViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void OnShowUpdateEmployeeExecuted(ShowUpdateGiftCardMessage obj)
        {
            UpdateGiftCardView view = new UpdateGiftCardView();
            UpdateGiftCardViewModel vm = new UpdateGiftCardViewModel(obj.giftCard);
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
