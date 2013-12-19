using GalaSoft.MvvmLight.Command;
using MVVMFestivalProject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MVVMFestivalProject.viewmodel
{
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line Up"; }
        }
        public LineUpVM()
        {
            _lineUpList = LineUp.GetLineUp();
            _festivaldateList = Festival.GetDatums();
            _genreList = Genre.GetGenres();
            _stageList = Stage.GetStages();
            _bandList = Band.GetBands();
            NewStage = new Stage();
            NewDate = new Festival();
            NewBand = new Band();
            SelectedLineUp = new LineUp();
        }

        private ObservableCollection<LineUp> _lineUpList;
        public ObservableCollection<LineUp> LineUpList
        {
            get { return _lineUpList; }
            set { _lineUpList = value; OnPropertyChanged("LineUpList"); }
        }
        private LineUp _selectedLineUp;
        public LineUp SelectedLineUp
        {
            get { return _selectedLineUp; }
            set { _selectedLineUp = value; OnPropertyChanged("SelectedLineUp"); }
        }

        private ObservableCollection<Festival> _festivaldateList;
        public ObservableCollection<Festival> FestivaldateList
        {
            get { return _festivaldateList; }
            set { _festivaldateList = value; OnPropertyChanged("FestivaldateList"); }
        }
        private Festival _selectedFestivaldate;
        public Festival SelectedFestivaldate
        {
            get { return _selectedFestivaldate; }
            set { _selectedFestivaldate = value; OnPropertyChanged("SelectedFestivaldate"); }
        }
        private Festival _newDate;
        public Festival NewDate
        {
            get { return _newDate; }
            set { _newDate = value; OnPropertyChanged("NewDate"); }
        }

        private ObservableCollection<Genre> _genreList;
        public ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged("GenreList"); }
        }
        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }

        private ObservableCollection<Stage> _stageList;
        public ObservableCollection<Stage> StageList
        {
            get { return _stageList; }
            set { _stageList = value; OnPropertyChanged("StageList"); }
        }
        private Stage _selectedStage;
        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); }
        }

        private Stage _newStage;
        public Stage NewStage
        {
            get { return _newStage; }
            set { _newStage = value; OnPropertyChanged("NewStage"); }
        }

        private ObservableCollection<Band> _bandList;
        public ObservableCollection<Band> BandList
        {
            get { return _bandList; }
            set { _bandList = value; OnPropertyChanged("BandList"); }
        }
        private Band _selectedBand;
        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        private Band _newBand;
        public Band NewBand
        {
            get { return _newBand; }
            set { _newBand = value; OnPropertyChanged("NewBand"); }
        }

        public ICommand SaveBandCommand
        {
            get { return new RelayCommand(SaveBand,NewBand.IsValid); }
        }

        public ICommand SaveStageCommand
        {
            get { return new RelayCommand(SaveStage, NewStage.IsValid); }
        }

        public ICommand SaveDateCommand
        {
            get { return new RelayCommand(SaveDate,NewDate.IsValid); }
        }

        public ICommand FilterCommand
        {
            get { return new RelayCommand(Filter); }
        }

        public void Filter()
        {
            if (SelectedFestivaldate == null && SelectedStage != null)
            {
                LineUpList = LineUp.GetLineUpByStageID(SelectedStage.ID);
            }
            else if (SelectedFestivaldate != null && SelectedStage == null)
            {
                LineUpList = LineUp.GetLineUpByDateID(SelectedFestivaldate.ID);
            }
            else if (SelectedFestivaldate != null && SelectedStage != null)
            {
                LineUpList = LineUp.GetLineUpByStageAndDateID(SelectedStage.ID, SelectedFestivaldate.ID);
            }
        }

        public ICommand RemoveFiltersCommand
        {
            get { return new RelayCommand(RemoveFilters); }
        }

        public void RemoveFilters()
        {
            SelectedStage = null;
            SelectedFestivaldate = null;
            LineUpList = LineUp.GetLineUp();
        }

        public void SaveBand()
        {
            try
            {
                string sql = "INSERT INTO Bands(Name,Picture,Description,Facebook,Twitter) VALUES (@Name,@Picture,@Description,@Facebook,@Twitter);";
                DbParameter parName = Database.AddParameter("@Name", NewBand.Name);
                DbParameter parPicture = Database.AddParameter("@Picture", NewBand.Picture);
                DbParameter parDescription = Database.AddParameter("@Description", NewBand.Description);
                DbParameter parFacebook = Database.AddParameter("@Facebook", NewBand.Facebook);
                DbParameter parTwitter = Database.AddParameter("@Twitter", NewBand.Twitter);
                Database.ModifyData(sql, parName, parPicture, parDescription, parFacebook, parTwitter);
                BandList.Add(NewBand);
                BandList = Band.GetBands();
                MessageBox.Show("Artiest werd succesvol toegevoegd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveDate()
        {
            try
            {
                string sql = "INSERT INTO Festivaldagen(Date) VALUES (@Datum);";
                DbParameter par = Database.AddParameter("@Datum", NewDate.Date);
                Database.ModifyData(sql, par);
                FestivaldateList.Add(NewDate);
                FestivaldateList = Festival.GetDatums();
                MessageBox.Show("Datum werd succesvol toegevoegd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveStage()
        {
            try
            {
                string sql = "INSERT INTO Stages(Name) VALUES (@Stage)";
                DbParameter par = Database.AddParameter("@Stage", NewStage.Name);
                Database.ModifyData(sql, par);
                StageList.Add(NewStage);
                StageList = Stage.GetStages();
                MessageBox.Show("Podium werd succesvol toegevoegd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ICommand DeleteLineUpCommand
        {
            get { return new RelayCommand(DeleteLineUp); }
        }
        public void DeleteLineUp()
        {
            if (SelectedLineUp != null)
            {
                try
                {
                    string sql = "DELETE FROM LineUp WHERE ID=@id";
                    DbParameter par = Database.AddParameter("@id", SelectedLineUp.ID);
                    Database.ModifyData(sql, par);
                    LineUpList.Remove(SelectedLineUp);
                    NewLineUp();
                    MessageBox.Show("Line up werd succesvol verwijderd");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public ICommand DeleteLineUpArtiestCommand
        {
            get { return new RelayCommand(DeleteLineUpArtiest); }
        }
        public void DeleteLineUpArtiest()
        {
            if (SelectedLineUp != null)
            {
                try
                {
                    string sql = "DELETE FROM LineUp WHERE ID=@id";
                    DbParameter parID = Database.AddParameter("@id", SelectedLineUp.ID);
                    Database.ModifyData(sql, parID);

                    string sql2 = "DELETE FROM Bands WHERE ID=@id2";
                    DbParameter parID2 = Database.AddParameter("@id2", SelectedLineUp.Band.ID);
                    Database.ModifyData(sql2, parID2);

                    string sql3 = "DELETE FROM Band_Genre WHERE ID=@bandid";
                    DbParameter parBandID = Database.AddParameter("@ibandid", SelectedLineUp.Band.ID);
                    Database.ModifyData(sql3, parBandID);
                    BandList.Remove(SelectedLineUp.Band);
                    LineUpList.Remove(SelectedLineUp);
                    NewLineUp();
                    MessageBox.Show("Line up en artiest werden succesvol verwijderd");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public ICommand SaveLineUpCommand
        {
            get { return new RelayCommand(SaveLineUp); }
        }
        public void SaveLineUp()
        {
            try
            {
                string sql = "INSERT INTO LineUp(Start,Finish,BandID,StageID,FestivaldagID) VALUES(@Start,@Finish,@BandID,@StageID,@FestivaldagID)";
                DbParameter parStart = Database.AddParameter("@Start", SelectedLineUp.From);
                DbParameter parFinish = Database.AddParameter("@Finish", SelectedLineUp.Until);
                DbParameter parBandID = Database.AddParameter("@BandID", SelectedLineUp.Band.ID);
                DbParameter parStageID = Database.AddParameter("@StageID", SelectedLineUp.Stage.ID);
                DbParameter parDateID = Database.AddParameter("@FestivaldagID", SelectedLineUp.Date.ID);
                Database.ModifyData(sql, parStart, parFinish, parBandID, parStageID, parDateID);
                LineUpList.Add(SelectedLineUp);
                LineUpList = LineUp.GetLineUp();
                NewLineUp();
                MessageBox.Show("Artiest werd succesvol toegevoegd aan de line up");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand EditLineUpCommand
        {
            get { return new RelayCommand(EditLineUp); }
        }
        public void EditLineUp()
        {
            string sql = "UPDATE LineUp ";
            sql += "SET Start=@Start,Finish=@Finish,BandID=@BandID,StageID=@StageID,FestivaldagID=@FestivaldagID ";
            sql += "WHERE ID=@ID";
            DbParameter parID = Database.AddParameter("@ID", SelectedLineUp.ID);
            DbParameter parStart = Database.AddParameter("@Start", SelectedLineUp.From);
            DbParameter parFinish = Database.AddParameter("@Finish", SelectedLineUp.Until);
            DbParameter parBandID = Database.AddParameter("@BandID", SelectedLineUp.Band.ID);
            DbParameter parStageID = Database.AddParameter("@StageID", SelectedLineUp.Stage.ID);
            DbParameter parDateID = Database.AddParameter("@FestivaldagID", SelectedLineUp.Date.ID);

            Database.ModifyData(sql,parID, parStart, parFinish, parBandID, parStageID, parDateID);
            MessageBox.Show("Line up werd succesvol bewerkt");
        }

        public ICommand NewLineUpCommand
        {
            get { return new RelayCommand(NewLineUp); }
        }

        public void NewLineUp()
        {
            try
            {
                LineUp NewLineUp = new LineUp();
                SelectedLineUp = NewLineUp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
