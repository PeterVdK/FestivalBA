using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVVMFestivalProject.model
{
    class ContactpersonType
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static ObservableCollection<ContactpersonType> GetContactpersonTypes()
        {
            ObservableCollection<ContactpersonType> contactpersonTypes = new ObservableCollection<ContactpersonType>();
            string sql = "SELECT id,name FROM Contactpersoontypes";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                contactpersonTypes.Add(Create(reader));
            }
            return contactpersonTypes;
        }

        public static ContactpersonType GetContactpersonType(int id)
        {
            string sql = "SELECT id,name FROM Contactpersoontypes WHERE id=@id";
            DbParameter par = Database.AddParameter("@id", id);
            DbDataReader reader = Database.GetData(sql,par);
            if (reader.Read() == true)
            {
                return Create(reader);
            }
            return null;
        }

        private static ContactpersonType Create(IDataRecord record)
        {
            return new ContactpersonType()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString()
            };   
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
