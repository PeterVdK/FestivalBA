using DBHelper;
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LineUpDA
    {
        public static List<LineUp> GetLineUp(string stageid,string dateid)
        {
            string sql = "SELECT id,start,finish,bandid,stageid,festivaldagid FROM LineUp ORDER BY festivaldagid,start";
            return GetList(sql);
        }

        private static List<LineUp> GetList(string sSQL, params DbParameter[] dbParams)
        {
            List<LineUp> list = new List<LineUp>();

            DbDataReader reader = Database.GetData(sSQL, dbParams);
            while (reader.Read())
            {
                list.Add(Fill(reader));
            }
            return list;
        }

        private static LineUp Fill(DbDataReader reader)
        {
            LineUp lineup = new LineUp();
            lineup.Start = reader["Start"].ToString();
            lineup.Finish = reader["Finish"].ToString();
            lineup.BandID =(int)reader["BandID"];
            lineup.Band = BandDA.FindById(lineup.BandID);
            lineup.StageID = (int)reader["StageID"];
            lineup.Stage = StageDA.FindById(lineup.StageID);
            lineup.FestivaldagID = (int)reader["FestivaldagID"];
            lineup.Festivaldag = FestivaldagDA.FindById(lineup.FestivaldagID);
            return lineup;
        }
    }
}