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
    class Genre:IDataErrorInfo
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [Required(ErrorMessage = "Gelieve minstens één genre te selecteren")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
            string sql = "SELECT * FROM Genres";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                genres.Add(Create(reader));
            }
            return genres;
        }

        public static ObservableCollection<Genre> GetGenres(int id)
        {
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
            string sql = "SELECT * FROM Band_Genre INNER JOIN Genres ON Band_Genre.GenreID=Genres.ID WHERE BandID=@bandid";
            DbParameter par = Database.AddParameter("@bandid", id);
            DbDataReader reader = Database.GetData(sql, par);
            while (reader.Read() == true)
            {
                genres.Add(Create(reader));
            }
            return genres;
        }

        private static Genre Create(IDataRecord record)
        {
            return new Genre()
            {
                ID = record["ID"].ToString(),
                Name = record["Genre"].ToString()
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
