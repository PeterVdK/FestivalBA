using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GalaSoft.MvvmLight.Command;
using MVVMFestivalProject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            SelectedTicketholder = new Ticket();
            NewTicketType = new TicketType();
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
        private TicketType _selectedTicketType;
        public TicketType SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value; OnPropertyChanged("SelectedTicketType"); }
        }

        private TicketType _newTicketType;
        public TicketType NewTicketType
        {
            get { return _newTicketType; }
            set { _newTicketType = value; OnPropertyChanged("NewTickettype"); }
        }

        public ICommand SaveTicketTypeCommand
        {
            get { return new RelayCommand(SaveTicketType, NewTicketType.IsValid); }
        }

        public ICommand NewTicketholderCommand
        {
            get { return new RelayCommand(NewTicketholder); }
        }

        public ICommand EditTicketholderCommand
        {
            get { return new RelayCommand(EditTicketholder); }
        }

        public ICommand SaveTicketholderCommand
        {
            get { return new RelayCommand(SaveTicketholder); }
        }

        public void SaveTicketType()
        {
            try
            {
                string sql = "INSERT INTO Tickettypes(Name,Price,Available) VALUES (@Name,@Price,@Available)";
                DbParameter parName = Database.AddParameter("@Name", NewTicketType.Name);
                DbParameter parPrice = Database.AddParameter("@Price", NewTicketType.Price);
                DbParameter parAvailable = Database.AddParameter("@Available", NewTicketType.AvailableTickets);
                Database.ModifyData(sql, parName, parPrice, parAvailable);
                TicketTypeList.Add(NewTicketType);
                TicketTypeList = TicketType.GetTicketTypes();
                MessageBox.Show("Tickettype werd succesvol toegevoegd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditTicketholder()
        {
            try
            {
                string sql = "UPDATE Ticketholders";
                sql += " SET Name=@Name,FirstName=@FirstName,Email=@Email,Amount=@Amount,TickettypeID=@Type";
                sql += " WHERE ID=@ID";
                DbParameter parID = Database.AddParameter("@ID", SelectedTicketholder.ID);
                DbParameter parName = Database.AddParameter("@Name", SelectedTicketholder.Name);
                DbParameter parFirstName = Database.AddParameter("@FirstName", SelectedTicketholder.FirstName);
                DbParameter parEmail = Database.AddParameter("@Email", SelectedTicketholder.TicketholderEmail);
                DbParameter parAmount = Database.AddParameter("@Amount", SelectedTicketholder.Amount);
                DbParameter parType = Database.AddParameter("@Type", SelectedTicketholder.Dag.ID);

                string sql2 = "UPDATE Tickettypes SET Tickettypes.Available=Tickettypes.Available-@Aantal ";
                sql2 += "FROM Tickettypes INNER JOIN Ticketholders ON Tickettypes.ID = Ticketholders.TickettypeID ";
                sql2 += "WHERE Tickettypes.Name=@TypeNaam";
                DbParameter parAantal = Database.AddParameter("@Aantal", SelectedTicketholder.Amount);
                DbParameter parTypeNaam = Database.AddParameter("@TypeNaam", SelectedTicketholder.Dag.Name);

                if (SelectedTicketholder.Amount <= SelectedTicketholder.Dag.AvailableTickets)
                {
                    Database.ModifyData(sql, parID, parName, parFirstName, parEmail, parAmount, parType);
                    Database.ModifyData(sql2, parAantal, parTypeNaam);
                    MessageBox.Show("Reservatie werd succesvol bewerkt");
                }
                else
                    MessageBox.Show("Er zijn onvoldoende tickets beschikbaar");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveTicketholder()
        {
            try
            {
                string sql = "INSERT INTO Ticketholders(Name, FirstName, Email, Amount, TickettypeID) ";
                sql += "VALUES (@Name,@FirstName,@Email,@Amount,@TypeID)";
                DbParameter parName = Database.AddParameter("@Name", SelectedTicketholder.Name);
                DbParameter parFirstName = Database.AddParameter("@FirstName", SelectedTicketholder.FirstName);
                DbParameter parEmail = Database.AddParameter("@Email", SelectedTicketholder.TicketholderEmail);
                DbParameter parTypeID = Database.AddParameter("@TypeID", SelectedTicketholder.Dag.ID);
                DbParameter parAmount = Database.AddParameter("@Amount", SelectedTicketholder.Amount);

                string sql2 = "UPDATE Tickettypes SET Tickettypes.Available=Tickettypes.Available-@Aantal ";
                sql2 += "FROM Tickettypes INNER JOIN Ticketholders ON Tickettypes.ID = Ticketholders.TickettypeID ";
                sql2 += "WHERE Tickettypes.Name=@TypeNaam";
                DbParameter parAantal = Database.AddParameter("@Aantal", SelectedTicketholder.Amount);
                DbParameter parTypeNaam = Database.AddParameter("@TypeNaam", SelectedTicketholder.Dag.Name);

                if (SelectedTicketholder.Amount <= SelectedTicketholder.Dag.AvailableTickets)
                {
                    Database.ModifyData(sql, parName, parFirstName, parEmail, parTypeID, parAmount);
                    Database.ModifyData(sql2, parAantal, parTypeNaam);
                    TicketholderList.Add(SelectedTicketholder);
                    SelectedTicketholder.Dag.AvailableTickets -= SelectedTicketholder.Amount;
                    MessageBox.Show("Reservatie werd succesvol toegevoegd");
                }
                else
                    MessageBox.Show("Er zijn onvoldoende tickets beschikbaar");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void NewTicketholder()
        {
            try
            {
                Ticket NewTicket = new Ticket();
                SelectedTicketholder = NewTicket;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand ExportCommand
        {
            get { return new RelayCommand(Export); }
        }

        public void Export()
        {
            try
            {
                string filename = SelectedTicketholder.Name + "_" + SelectedTicketholder.FirstName + "_" + SelectedTicketholder.Dag.Name + ".docx";
                File.Copy("template.docx", filename, true);

                WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true);
                IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
                foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                {
                    bookmarks[bms.Name] = bms;
                }


                Run runName = new Run(new Text(SelectedTicketholder.Name + " " + SelectedTicketholder.FirstName));
                RunProperties propName = new RunProperties();
                RunFonts fontName = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri" };
                FontSize sizeName = new FontSize() { Val = "48" };
                propName.Append(fontName);
                propName.Append(sizeName);
                runName.PrependChild<RunProperties>(propName);

                Run runType = new Run(new Text(SelectedTicketholder.Dag.Name));
                RunProperties propType = new RunProperties();
                RunFonts fontType = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri" };
                FontSize sizeType = new FontSize() { Val = "144" };
                propType.Append(fontType);
                propType.Append(sizeType);
                runType.PrependChild<RunProperties>(propType);

                Run runPrice = new Run(new Text("€"+SelectedTicketholder.Dag.Price.ToString()));
                RunProperties propPrice = new RunProperties();
                RunFonts fontPrice = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri" };
                FontSize sizePrice = new FontSize() { Val = "72" };
                propPrice.Append(fontPrice);
                propPrice.Append(sizePrice);
                runPrice.PrependChild<RunProperties>(propPrice);

                Run runCode = new Run(new Text(SelectedTicketholder.ID+SelectedTicketholder.Dag.ID+SelectedTicketholder.Dag.Price.ToString()+"0000"));
                RunProperties propCode = new RunProperties();
                RunFonts fontCode = new RunFonts() { Ascii = "Free 3 of 9", HighAnsi = "Free 3 of 9" };
                FontSize sizeCode = new FontSize() { Val = "144" };
                propCode.Append(fontCode);
                propCode.Append(sizeCode);
                runCode.PrependChild<RunProperties>(propCode);

                bookmarks["Naam"].Parent.InsertAfter<Run>(runName, bookmarks["Naam"]);
                bookmarks["Type"].Parent.InsertAfter<Run>(runType, bookmarks["Type"]);
                bookmarks["Prijs"].Parent.InsertAfter<Run>(runPrice, bookmarks["Prijs"]);
                bookmarks["Code"].Parent.InsertAfter<Run>(runCode, bookmarks["Code"]);

                newdoc.Close();
                MessageBox.Show("Succesvol geëxporteerd naar Word");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
