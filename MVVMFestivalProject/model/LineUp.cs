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
    class LineUp:IDataErrorInfo
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private Festival date;
        [Required(ErrorMessage = "De datum is verplicht")]
        public Festival Date
        {
            get { return date; }
            set { date = value; }
        }

        private string from;
        [Required(ErrorMessage = "De starttijd is verplicht")]
        [StringLength(5, ErrorMessage = "De starttijd mag maximaal 5 karakters bevatten")]
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        private string until;
        [Required(ErrorMessage = "De eindtijd is verplicht")]
        [StringLength(5, ErrorMessage = "De eindtijd mag maximaal 5 karakters bevatten")]
        public string Until
        {
            get { return until; }
            set { until = value; }
        }

        private Stage stage;
        [Required(ErrorMessage = "Het podium is verplicht")]
        public Stage Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        private Band band;
        [Required(ErrorMessage = "De artiest is verplicht")]
        public Band Band
        {
            get { return band; }
            set { band = value; }
        }

        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> lineUp = new ObservableCollection<LineUp>();
            string sql = "SELECT id,start,finish,bandid,stageid,festivaldagid FROM LineUp";
            DbDataReader reader = Database.GetData(sql);

            while (reader.Read() == true)
            {
                lineUp.Add(Create(reader));
            }
            return lineUp;
        }

        private static LineUp Create(IDataRecord record)
        {
            return new LineUp()
            {
                ID = record["ID"].ToString(),
                Date = Festival.GetDatum((int)record["FestivaldagID"]) as Festival,
                From = record["Start"].ToString(),
                Until = record["Finish"].ToString(),
                Stage = Stage.GetStage((int)record["StageID"]) as Stage,
                Band = Band.GetBand((int)record["BandID"]) as Band
            };
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
