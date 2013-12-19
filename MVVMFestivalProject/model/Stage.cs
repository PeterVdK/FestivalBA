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
    class Stage:IDataErrorInfo
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [Required(ErrorMessage = "Het podium is verplicht")]
        [StringLength(50, ErrorMessage = "Maximaal 50 karakters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> stages = new ObservableCollection<Stage>();
            string sql = "SELECT * FROM Stages";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                stages.Add(Create(reader));
            }
            return stages;
        }

        public static Stage GetStage(int id)
        {
            string sql = "SELECT * FROM Stages WHERE id=@id";
            DbParameter par = Database.AddParameter("@id", id);
            DbDataReader reader = Database.GetData(sql, par);
            if (reader.Read() == true)
            {
                return Create(reader);
            }
            return null;
        }

        private static Stage Create(IDataRecord record)
        {
            return new Stage()
            {
                ID = record["ID"].ToString(),
                Name = record["Name"].ToString()
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
