using Esercitazione.Core.BusinessLayers;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Repositories;
using Esercitazione.WPF.Messaging.GiftCards;
using Esercitazione.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class GiftCardRowViewModel : ViewModelBase
    {
        private GiftCard _giftCard;

        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value; RaisePropertyChanged(); }
        }

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged(); }
        }

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }

        private DateTime _DataScadenza;
        public DateTime DataScadenza
        {
            get { return _DataScadenza; }
            set { _DataScadenza = value; RaisePropertyChanged(); }
        }

        private bool _Details = false;
        public bool Details
        {
            get { return _Details; }
            set { _Details = value; RaisePropertyChanged(); }
        }

        public ICommand DeleteGiftCardCommand { get; set; }
        public ICommand UpdateGiftCardCommand { get; set; }

        public GiftCardRowViewModel()
        {
            DeleteGiftCardCommand = new RelayCommand(() => ExecuteDelete());
            UpdateGiftCardCommand = new RelayCommand(() => ExecuteUpdate());
        }

        public GiftCardRowViewModel(GiftCard giftCard) : this()
        {
            this._giftCard = giftCard;
            Mittente = giftCard.Mittente;
            Destinatario = giftCard.Destinatario;
            Messaggio = giftCard.Messaggio;
            Importo = giftCard.Importo;
            DataScadenza = giftCard.DataScadenza;
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm delete",
                Content = "Are you sure?",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }

        private void ExecuteUpdate()
        {
            Messenger.Default.Send(new ShowUpdateGiftCardMessage { giftCard = _giftCard });
        }

        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

                var response = layer.DeleteGiftCard(_giftCard);

                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Deletion Confirmed",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }
    }
}
