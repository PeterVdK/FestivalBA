using GalaSoft.MvvmLight.Command;
using MVVMFestivalProject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMFestivalProject.viewmodel
{
    class ContactpersonVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contacten"; }
        }
        public ContactpersonVM()
        {
            _contactpersonList = Contactperson.GetContactpersons();
            _contactpersonTypeList = ContactpersonType.GetContactpersonTypes();
            Contactperson = new Contactperson();
        }

        private ObservableCollection<Contactperson> _contactpersonList;
        public ObservableCollection<Contactperson> ContactpersonList
        {
            get { return _contactpersonList; }
            set { _contactpersonList = value; OnPropertyChanged("ContactpersonList"); }
        }
        private Contactperson _selectedContactperson;
        public Contactperson SelectedContactperson
        {
            get { return _selectedContactperson; }
            set { _selectedContactperson = value; OnPropertyChanged("SelectedContactperson"); }
        }
        private ObservableCollection<ContactpersonType> _contactpersonTypeList;
        public ObservableCollection<ContactpersonType> ContactpersonTypeList
        {
            get { return _contactpersonTypeList; }
            set { _contactpersonTypeList = value; OnPropertyChanged("ContactpersonTypeList"); }
        }

        private Contactperson _contactperson;
        public Contactperson Contactperson
        {
            get { return _contactperson; }
            set { _contactperson = value; OnPropertyChanged("Contactperson"); }
        }

        public ICommand DeleteContactpersonCommand
        {
            get { return new RelayCommand(DeleteContactperson); }
        }
        public ICommand SaveContactpersonCommand
        {
            get { return new RelayCommand(SaveContactperson); }
        }
        public void SaveContactperson()
        {
            try
            {
                string sql = "INSERT INTO Contactpersonen(Name, FirstName, Gender, Birthdate, Company, Street,City,Phone,Cellphone,Email,ContactpersoontypeID,Number) ";
                sql += "VALUES ('@Name','@FirstName','@Gender','@Birthdate','@Company','@Street','@City','@Phone','@Cellphone','@Email','@JobRole','@Number')";
                DbParameter parName = Database.AddParameter("@Name", Contactperson.Name);
                DbParameter parFirstName = Database.AddParameter("@FirstName", Contactperson.FirstName);
                DbParameter parGender = Database.AddParameter("@Gender", Contactperson.Gender);
                DbParameter parBirthdate = Database.AddParameter("@Birthdate", Contactperson.BirthDate);
                DbParameter parCompany = Database.AddParameter("@Company", Contactperson.Company);
                DbParameter parStreet = Database.AddParameter("@Street", Contactperson.Street);
                DbParameter parCity = Database.AddParameter("@City", Contactperson.City);
                DbParameter parPhone = Database.AddParameter("@Phone", Contactperson.Phone);
                DbParameter parCellphone = Database.AddParameter("@Cellphone", Contactperson.Cellphone);
                DbParameter parEmail = Database.AddParameter("@Email", Contactperson.Email);
                DbParameter parNumber = Database.AddParameter("@Number", Contactperson.Number);
                DbParameter parJobRole = Database.AddParameter("@JobRole", Contactperson.JobRole);

                Database.ModifyData(sql, parName, parFirstName, parGender, parBirthdate, parCompany, parStreet, parCity, parPhone, parCellphone, parEmail, parNumber, parJobRole);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        public void DeleteContactperson()
        {
            if (SelectedContactperson != null)
            {
                try
                {
                    string sql = "DELETE FROM contactpersonen WHERE ID=@id";
                    DbParameter par = Database.AddParameter("@id", SelectedContactperson.ID);
                    Database.ModifyData(sql, par);
                    MessageBox.Show("Contactpersoon werd succesvol verwijderd");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}