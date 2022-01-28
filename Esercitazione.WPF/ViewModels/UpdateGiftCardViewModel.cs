using Esercitazione.Core.BusinessLayers;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Repositories;
using Esercitazione.WPF.Messaging.GiftCards;
using Esercitazione.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class UpdateGiftCardViewModel : ViewModelBase
    {
        private GiftCard _GiftCard;

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(); }
        }

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

        private DateTime _DataScadenza = DateTime.Now;
        public DateTime DataScadenza
        {
            get { return _DataScadenza; }
            set { _DataScadenza = value; RaisePropertyChanged(); }
        }
        public ICommand UpdateGiftCardCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public UpdateGiftCardViewModel()
        { 
            UpdateGiftCardCommand = new RelayCommand(() => ExecuteUpdate(), () => CanExecuteUpdate());
            CancelCommand = new RelayCommand(() => ExecuteCancel());
        }

        public UpdateGiftCardViewModel(GiftCard giftCard) : this()
        {
            if (giftCard == null) throw new ArgumentNullException(nameof(giftCard));

            this._GiftCard = giftCard;
            ID = giftCard.Id;
            Mittente = giftCard.Mittente;
            Destinatario = giftCard.Destinatario;
            Messaggio = giftCard.Messaggio;
            Importo = giftCard.Importo;
            DataScadenza = giftCard.DataScadenza;
        }

        private void ExecuteCancel()
        {
            Messenger.Default.Send(new CloseUpdateGiftCardMessage());
        }

        private bool CanExecuteUpdate()
        {
            return !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Destinatario) &&
                !string.IsNullOrEmpty(Messaggio) &&
                !string.IsNullOrEmpty(Importo.ToString()) &&
                !string.IsNullOrEmpty(DataScadenza.ToString());
        }

        private void ExecuteUpdate()
        {
            var giftcard = new GiftCard
            {
                Id = ID,
                Mittente = Mittente,
                Destinatario = Destinatario,
                DataScadenza = DataScadenza,
                Importo = Importo,
                Messaggio = Messaggio
            };

            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

            var result = layer.UpdateGiftCard(giftcard);

            if (!result.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Alcuni dati non validi",
                    Content = result.Message,
                    Icon = MessageBoxImage.Warning
                });
                return;
            }

            Messenger.Default.Send(new DialogMessage
            {
                Title = "Conferma",
                Content = $"Gift Card con ID: {ID} aggiornata",
                Icon = MessageBoxImage.Information
            });

            CancelCommand.Execute(null);
        }
    }
}
