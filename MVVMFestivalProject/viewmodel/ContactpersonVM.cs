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
            SelectedContactperson = new Contactperson();
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
            set {_selectedContactperson = value; OnPropertyChanged("SelectedContactperson");}
        }
        private ObservableCollection<ContactpersonType> _contactpersonTypeList;
        public ObservableCollection<ContactpersonType> ContactpersonTypeList
        {
            get { return _contactpersonTypeList; }
            set { _contactpersonTypeList = value; OnPropertyChanged("ContactpersonTypeList"); }
        }

        public ICommand DeleteContactpersonCommand
        {
            get { return new RelayCommand(DeleteContactperson); }
        }

        public ICommand NewContactpersonCommand
        {
            get { return new RelayCommand(NewContactperson); }
        }

        public ICommand EditContactpersonCommand
        {
            get { return new RelayCommand(EditContactperson); }
        }
        public ICommand SaveContactpersonCommand
        {
            get { return new RelayCommand(SaveContactperson); }
        }

        public void EditContactperson()
        {
            try
            {
                string sql="UPDATE Contactpersonen";
                sql += " SET Name=@Name,FirstName=@FirstName,Gender=@Gender,Birthdate=@Birthdate,Company=@Company,Street=@Street,City=@City,Phone=@Phone,Cellphone=@Cellphone,Email=@Email,ContactpersoontypeID=@JobRole,Number=@Number";
                sql += " WHERE ID=@ID";
                DbParameter parID = Database.AddParameter("@ID", SelectedContactperson.ID);
                DbParameter parName = Database.AddParameter("@Name", SelectedContactperson.Name);
                DbParameter parFirstName = Database.AddParameter("@FirstName", SelectedContactperson.FirstName);
                DbParameter parGender = Database.AddParameter("@Gender", SelectedContactperson.Gender);
                DbParameter parBirthdate = Database.AddParameter("@Birthdate", SelectedContactperson.BirthDate);
                DbParameter parCompany = Database.AddParameter("@Company", SelectedContactperson.Company);
                DbParameter parStreet = Database.AddParameter("@Street", SelectedContactperson.Street);
                DbParameter parCity = Database.AddParameter("@City", SelectedContactperson.City);
                DbParameter parPhone = Database.AddParameter("@Phone", SelectedContactperson.Phone);
                DbParameter parCellphone = Database.AddParameter("@Cellphone", SelectedContactperson.Cellphone);
                DbParameter parEmail = Database.AddParameter("@Email", SelectedContactperson.Email);
                DbParameter parNumber = Database.AddParameter("@Number", SelectedContactperson.Number);
                DbParameter parJobRole = Database.AddParameter("@JobRole", SelectedContactperson.JobRole.ID);

                Database.ModifyData(sql,parID,parName, parFirstName, parGender, parBirthdate, parCompany, parStreet, parCity, parPhone, parCellphone, parEmail, parNumber, parJobRole);
                MessageBox.Show("Contactpersoon werd succesvol bewerkt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveContactperson()
        {
            try
            {
                string sql = "INSERT INTO Contactpersonen(Name, FirstName, Gender, Birthdate, Company, Street,City,Phone,Cellphone,Email,ContactpersoontypeID,Number) ";
                sql += "VALUES (@Name,@FirstName,@Gender,@Birthdate,@Company,@Street,@City,@Phone,@Cellphone,@Email,@JobRole,@Number)";
                DbParameter parName = Database.AddParameter("@Name", SelectedContactperson.Name);
                DbParameter parFirstName = Database.AddParameter("@FirstName", SelectedContactperson.FirstName);
                DbParameter parGender = Database.AddParameter("@Gender", SelectedContactperson.Gender);
                DbParameter parBirthdate = Database.AddParameter("@Birthdate", SelectedContactperson.BirthDate);
                DbParameter parCompany = Database.AddParameter("@Company", SelectedContactperson.Company);
                DbParameter parStreet = Database.AddParameter("@Street", SelectedContactperson.Street);
                DbParameter parCity = Database.AddParameter("@City", SelectedContactperson.City);
                DbParameter parPhone = Database.AddParameter("@Phone", SelectedContactperson.Phone);
                DbParameter parCellphone = Database.AddParameter("@Cellphone", SelectedContactperson.Cellphone);
                DbParameter parEmail = Database.AddParameter("@Email", SelectedContactperson.Email);
                DbParameter parNumber = Database.AddParameter("@Number", SelectedContactperson.Number);
                DbParameter parJobRole = Database.AddParameter("@JobRole", SelectedContactperson.JobRole.ID);

                Database.ModifyData(sql, parName, parFirstName, parGender, parBirthdate, parCompany, parStreet, parCity, parPhone, parCellphone, parEmail, parNumber, parJobRole);
                ContactpersonList.Add(SelectedContactperson);
                MessageBox.Show("Contactpersoon werd succesvol toegevoegd");
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
                    ContactpersonList.Remove(SelectedContactperson);
                    MessageBox.Show("Contactpersoon werd succesvol verwijderd");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void NewContactperson()
        {
                try
                {
                    Contactperson NewPerson = new Contactperson();
                    SelectedContactperson = NewPerson;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}