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
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class CreateGiftCardViewModel : ViewModelBase
    {

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
        public ICommand CreateGiftCardCommand { get; set; }

        public CreateGiftCardViewModel()
        {
            CreateGiftCardCommand = new RelayCommand(() => ExecuteCreate(), () => CanExecuteCreate());

            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateGiftCardCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }
        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Destinatario) &&
                !string.IsNullOrEmpty(Messaggio) &&
                !string.IsNullOrEmpty(Importo.ToString()) &&
                !string.IsNullOrEmpty(DataScadenza.ToString());
        }

        private void ExecuteCreate()
        {
            var giftcard = new GiftCard
            {
                Mittente = Mittente,
                Destinatario = Destinatario,
                DataScadenza = DataScadenza,
                Importo = Importo,
                Messaggio = Messaggio
            };

            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
            var response = layer.CreateGiftCard(giftcard);

            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Something wrong",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Creazione completata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateGiftCardMessage());
        }
    }
}
