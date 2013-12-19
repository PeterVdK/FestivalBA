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

namespace MVVMFestivalProject.model
{
    class Band:IDataErrorInfo
    {
        private string id;
        public string ID
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

        private string picture;
        [Required(ErrorMessage = "het fotopad is verplicht")]
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string description;
        [Required(ErrorMessage = "De omschrijving is verplicht")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string twitter;
        [Required(ErrorMessage = "De Twitter-URL is verplicht")]
        [StringLength(100, ErrorMessage = "De Twitter-URL mag maximaal 100 karakters bevatten")]
        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }

        private string facebook;
        [Required(ErrorMessage = "De Facebook-URL is verplicht")]
        [StringLength(100, ErrorMessage = "De Facebook-URL mag maximaal 100 karakters bevatten")]
        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; }
        }

        private ObservableCollection<Genre> genres;
        public ObservableCollection<Genre> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> bands = new ObservableCollection<Band>();
            string sql = "SELECT * FROM Bands";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                bands.Add(Create(reader));
            }
            return bands;
        }

        public static Band GetBand(int id)
        {
            string sql = "SELECT * FROM Bands WHERE id=@id";
            DbParameter par = Database.AddParameter("@id", id);
            DbDataReader reader = Database.GetData(sql, par);
            if (reader.Read() == true)
            {
                return Create(reader);
            }
            return null;
        }

        private static Band Create(IDataRecord record)
        {
            return new Band()
            {
                ID = record["ID"].ToString(),
                Name = record["Name"].ToString(),
                Picture = record["Picture"].ToString(),
                Description = record["Description"].ToString(),
                Facebook = record["Facebook"].ToString(),
                Twitter = record["Twitter"].ToString(),
                Genres = Genre.GetGenres((int)record["ID"]) as ObservableCollection<Genre>
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
            get { return null; }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
