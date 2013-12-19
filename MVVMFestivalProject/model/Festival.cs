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
    class Festival:IDataErrorInfo
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        
        private DateTime? date;
        [Required(ErrorMessage = "De datum is verplicht")]
        public DateTime? Date
        {
            get { return date; }
            set { date = value; }
        }

        public static ObservableCollection<Festival> GetDatums()
        {
            ObservableCollection<Festival> datums = new ObservableCollection<Festival>();
            string sql = "SELECT * FROM Festivaldagen";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                datums.Add(Create(reader));
            }
            return datums;
        }

        public static Festival GetDatum(int id)
        {
            string sql = "SELECT * FROM Festivaldagen WHERE id=@id";
            DbParameter par = Database.AddParameter("@id", id);
            DbDataReader reader = Database.GetData(sql, par);
            if (reader.Read() == true)
            {
                return Create(reader);
            }
            return null;
        }

        private static Festival Create(IDataRecord record)
        {
            return new Festival()
            {
                ID = record["ID"].ToString(),
                Date = Convert.ToDateTime(record["Date"])
            };
        }

        public override string ToString()
        {
            return Date != null ? Date.Value.ToString("dddd dd MMMM") : "n/a";
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
            get { return null; }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
