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
    class TicketType:IDataErrorInfo
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [Required(ErrorMessage = "Het type is verplicht")]
        [StringLength(10, ErrorMessage = "Het type mag maximaal 10 karakters bevatten")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double price;
        [Required(ErrorMessage = "De prijs is verplicht")]
        [Range(0,999,ErrorMessage="De prijs moet tussen €0-999 liggen")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int availableTickets;
        [Required(ErrorMessage = "Het aantal is verplicht")]
        [Range(0, 1000000)]
        public int AvailableTickets
        {
            get { return availableTickets; }
            set { availableTickets = value; }
        }

        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            ObservableCollection<TicketType> ticketTypes = new ObservableCollection<TicketType>();
            string sql = "SELECT id,name,price,available FROM tickettypes";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                ticketTypes.Add(Create(reader));
            }
            return ticketTypes;
        }

        public static TicketType GetTicketType(int id)
        {
            string sql = "SELECT * FROM Tickettypes WHERE id=@id";
            DbParameter par = Database.AddParameter("@id", id);
            DbDataReader reader = Database.GetData(sql, par);
            if (reader.Read() == true)
            {
                return Create(reader);
            }
            return null;
        }

        private static TicketType Create(IDataRecord record)
        {
            return new TicketType()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString(),
                Price = (double)record["Price"],
                AvailableTickets = (int)record["Available"]
            };
        }
        public override string ToString()
        {
            return Name;
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
