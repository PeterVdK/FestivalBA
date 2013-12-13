using GalaSoft.MvvmLight.Command;
using MVVMFestivalProject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFestivalProject.viewmodel
{
    class TicketVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Ticketing"; }
        }
        public TicketVM()
        {
            _ticketholderList = Ticket.GetTicketholders();
            _ticketTypeList = TicketType.GetTicketTypes();
        }

        private ObservableCollection<Ticket> _ticketholderList;
        public ObservableCollection<Ticket> TicketholderList
        {
            get { return _ticketholderList; }
            set { _ticketholderList = value; OnPropertyChanged("TicketholderList"); }
        }
        private Ticket _selectedTicketholder;
        public Ticket SelectedTicketholder
        {
            get { return _selectedTicketholder; }
            set { _selectedTicketholder = value; OnPropertyChanged("SelectedTicketholder"); }
        }
        private ObservableCollection<TicketType> _ticketTypeList;
        public ObservableCollection<TicketType> TicketTypeList
        {
            get { return _ticketTypeList; }
            set { _ticketTypeList = value; OnPropertyChanged("TicketTypeList"); }
        }
        private Ticket _selectedTicketType;
        public Ticket SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value; OnPropertyChanged("SelectedTicketType"); }
        }

        public ICommand SaveTicketholderCommand
        {
            get { return new RelayCommand<string>(SaveTicketholder); }
        }
        public void SaveTicketholder(string name)
        {
            Console.WriteLine("save command: " + name );
        }
    }
}
