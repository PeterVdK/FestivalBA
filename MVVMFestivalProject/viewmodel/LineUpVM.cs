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
            get { return "Line-up"; }
        }
        public LineUpVM()
        {
            _lineUpList = LineUp.GetLineUp();
            _festivaldateList = Festival.GetDatums();
            _genreList = Genre.GetGenres();
            _stageList = Stage.GetStages();
            _bandList = Band.GetBands();
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

        public ICommand SaveStageCommand
        {
            get { return new RelayCommand<Stage>(SaveStage); }
        }

        public ICommand SaveDateCommand
        {
            get { return new RelayCommand<Festival>(SaveDate); }
        }

        public ICommand ImageSourceNullCommand
        {
            get { return new RelayCommand<object>(ImageSourceNull); }
        }

        public void SaveDate(Festival date)
        {
            string sql = "INSERT INTO Festivaldagen (Date) VALUES ('@Datum');";
            DbParameter par = Database.AddParameter("@Datum", date);
            Database.ModifyData(sql, par);
        }

        public void SaveStage(Stage stage)
        {
            string sql = "INSERT INTO Stages (Name) VALUES ('@Stage');";
            DbParameter par = Database.AddParameter("@Stage", stage);
            Database.ModifyData(sql, par);
        }

        public void ImageSourceNull(object sender)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("NoImage.png", UriKind.Relative));
        }
    }
}
