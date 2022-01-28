using Esercitazione.Core.BusinessLayers;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Repositories;
using Esercitazione.Core.Repositories;
using Esercitazione.WPF.Messaging.GiftCards;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand CreateGiftCardCommand { get; set; }
        public ICommand LoadGiftCardsCommand { get; set; }
        public ICommand UpdateListCommand { get; set; }

        public ObservableCollection<GiftCardRowViewModel> _GiftCardsSource;
        private ICollectionView _GiftCards;
        public ICollectionView GiftCards
        {
            get { return _GiftCards; }
            set { _GiftCards = value; RaisePropertyChanged(); }
        }

        public HomeViewModel()
        {
            CreateGiftCardCommand = new RelayCommand(() => ExecuteShowCreateGiftCard());
            LoadGiftCardsCommand = new RelayCommand(() => ExecuteLoadGiftCards());
            UpdateListCommand = new RelayCommand(() => ExecuteLoadGiftCards());

            _GiftCardsSource = new ObservableCollection<GiftCardRowViewModel>();
            _GiftCards = new CollectionView(_GiftCardsSource);

            LoadGiftCardsCommand.Execute(null);
        }

        private void ExecuteLoadGiftCards()
        {
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            MainBusinessLayer layer = new MainBusinessLayer(repo);

            var giftCards = layer.FetchAllGiftCards();

            _GiftCardsSource.Clear();

            foreach (GiftCard item in giftCards)
                _GiftCardsSource.Add(new GiftCardRowViewModel(item));
        }

        private void ExecuteShowCreateGiftCard()
        {
            Messenger.Default.Send(new ShowCreateGiftCardMessage());
        }
    }
}
