using DBHelper;
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class BandDA
    {
        public static List<Band> GetBands()
        {
            string sSQL = "SELECT * FROM [Bands]";

            return GetList(sSQL);
        }

        private static List<Band> GetList(string sSQL, params DbParameter[] dbParams)
        {
            List<Band> list = new List<Band>();

            DbDataReader reader = Database.GetData(sSQL, dbParams);
            while (reader.Read())
            {
                list.Add(Fill(reader));
            }
            return list;
        }
        public static Band FindById(int bandID)
        {
            //0.vars          
            //1. SQL instructie 
            string sSQL = "SELECT * FROM [Bands]";
            sSQL += " WHERE [Bands].[Id] = @bandID";

            //2. SQL parameters
            DbParameter idPar = Database.AddParameter("@bandID", bandID);

            //3. Haal data op en controleer op null/lege velden
            return GetList(sSQL, idPar)[0];
        }

        private static Band Fill(DbDataReader reader)
        {
            Band band = new Band();
            band.ID = (int)reader["ID"];
            band.Name = reader["Name"].ToString();
            band.Picture = reader["Picture"].ToString();
            band.Description = reader["Description"].ToString();
            band.Facebook = reader["Facebook"].ToString();
            band.Twitter = reader["Twitter"].ToString();
            return band;
        }

    }
}