using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVVMFestivalProject.model
{
    class Ticket:IDataErrorInfo
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, ErrorMessage = "De naam mag maximaal 50 karakters bevatten")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string firstName;
        [Required(ErrorMessage = "De voornaam is verplicht")]
        [StringLength(50, ErrorMessage = "De voornaam mag maximaal 50 karakters bevatten")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string ticketholderEmail;
        [Required(ErrorMessage = "Het emailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Het emailadres is niet geldig")]
        public string TicketholderEmail
        {
            get { return ticketholderEmail; }
            set { ticketholderEmail = value; }
        }

        private TicketType ticketType;
        [Required(ErrorMessage = "Het type is verplicht")]
        public TicketType TicketType
        {
            get { return ticketType; }
            set { ticketType = value; }
        }

        private int amount;
        [Required(ErrorMessage = "Het aantal is verplicht")]
        [Range(0, 10, ErrorMessage = "Het aantal moet tussen 0-10 liggen")]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public static ObservableCollection<Ticket> GetTicketholders()
        {
            ObservableCollection<Ticket> ticketholders = new ObservableCollection<Ticket>();
            string sql = "SELECT id, name,firstname,email,amount,tickettypeid FROM ticketholders";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                ticketholders.Add(Create(reader));
            }
            return ticketholders;
        }

        private static Ticket Create(IDataRecord record)
        {
            return new Ticket()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString(),
                FirstName = record["FirstName"].ToString(),
                TicketholderEmail = record["Email"].ToString(),
                Amount = (int)record["Amount"],
                TicketType = TicketType.GetTicketType((int)record["ID"]) as TicketType,
            };
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);

                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }

                return string.Empty;
            }
        }

        public string Error
        {
            get { return "Object not valid"; }
        }
    }
}
