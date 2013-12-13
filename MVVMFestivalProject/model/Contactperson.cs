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
    class Contactperson:IDataErrorInfo
    {
        //fields en properties
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, ErrorMessage = "De naam mag maximaal 50 karakters bevatten")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string firstName;
        [Required(ErrorMessage = "De voornaam is verplicht")]
        [StringLength(50, ErrorMessage = "De voornaam mag maximaal 50 karakters bevatten")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private char gender;
        [Required(ErrorMessage = "Het geslacht is verplicht")]
        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        private string company;
        [Required(ErrorMessage = "Het bedrijf is verplicht")]
        [StringLength(50, ErrorMessage = "Het bedrijf mag maximaal 50 karakters bevatten")]
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private ContactpersonType jobRole;
        [Required(ErrorMessage = "De rol is verplicht")]
        public ContactpersonType JobRole
        {
            get { return jobRole; }
            set { jobRole = value; }
        }

        private string street;
        [Required(ErrorMessage = "De straat is verplicht")]
        [StringLength(50, ErrorMessage = "De straat mag maximaal 50 karakters bevatten")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        private int number;
        [Required(ErrorMessage = "Het huisnummer is verplicht")]
        [Range(1,9999,ErrorMessage="Het huisnummer moet tussen 1 en 9999 liggen")]
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private string city;
        [Required(ErrorMessage = "De gemeente is verplicht")]
        [StringLength(50, ErrorMessage = "De gemeente mag maximaal 50 karakters bevatten")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        
        private string phone;
        //[Phone(ErrorMessage = "Het nummer is niet geldig")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string cellphone;
        //[Phone(ErrorMessage = "Het nummer is niet geldig")]
        public string Cellphone
        {
            get { return cellphone; }
            set { cellphone = value; }
        }

        private string email;
        [Required(ErrorMessage = "het emailadres is verplicht")]
        [EmailAddress(ErrorMessage="Het emailadres is niet geldig")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public static ObservableCollection<Contactperson> GetContactpersons()
        {
            ObservableCollection<Contactperson> contactpersons = new ObservableCollection<Contactperson>();
            string sql = "SELECT id, name,firstname,gender,Birthdate,company,street,number,city,phone,cellphone,email,ContactpersoontypeID FROM Contactpersonen";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                contactpersons.Add(Create(reader));
            }
            return contactpersons;
        }

        private static Contactperson Create(IDataRecord record)
        {
            return new Contactperson()
            {
                ID = (int)record["ID"],
                Name = record["Name"].ToString(),
                FirstName = record["FirstName"].ToString(),
                Gender = Convert.ToChar(record["Gender"]),
                BirthDate = Convert.ToDateTime(record["Birthdate"]),
                Company = record["Company"].ToString(),
                Street = record["Street"].ToString(),
                Number = Convert.ToInt32(record["Number"]),
                City = record["City"].ToString(),
                Phone = record["Phone"].ToString(),
                Cellphone = record["Cellphone"].ToString(),
                Email = record["Email"].ToString(),
                JobRole = ContactpersonType.GetContactpersonType((int)record["ContactpersoontypeID"]) as ContactpersonType
            };
        }

        public override string ToString()
        {
            return FirstName + " " + Name;
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

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
