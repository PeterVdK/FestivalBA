using DBHelper;
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class FestivaldagDA
    {
        public static List<Festivaldag> GetFestivaldagen()
        {
            string sSQL = "SELECT * FROM [Festivaldagen]";

            return GetList(sSQL);
        }
        private static List<Festivaldag> GetList(string sSQL, params DbParameter[] dbParams)
        {
            List<Festivaldag> list = new List<Festivaldag>();

            DbDataReader reader = Database.GetData(sSQL, dbParams);
            while (reader.Read())
            {
                list.Add(Fill(reader));
            }
            return list;
        }
        public static Festivaldag FindById(int festivaldagID)
        {
            //0.vars          
            //1. SQL instructie 
            string sSQL = "SELECT * FROM [Festivaldagen]";
            sSQL += " WHERE [Festivaldagen].[Id] = @festivaldagID";

            //2. SQL parameters
            DbParameter idPar = Database.AddParameter("@festivaldagID", festivaldagID);

            //3. Haal data op en controleer op null/lege velden
            return GetList(sSQL, idPar)[0];
        }


        private static Festivaldag Fill(DbDataReader reader)
        {
            Festivaldag festivaldag = new Festivaldag();
            festivaldag.ID = (int)reader["ID"];
            festivaldag.Date = (DateTime)reader["Date"];
            return festivaldag;
        }
    }
}