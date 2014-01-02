using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortableClassLibrary;
using System.Data.Common;
using DBHelper;

namespace API.Models
{
    public class GenreDA
    {
        public static List<Genre> GetGenres()
        {
            string sSQL = "SELECT * FROM [Stages]";

            return GetList(sSQL);
        }
        private static List<Genre> GetList(string sSQL, params DbParameter[] dbParams)
        {
            List<Genre> list = new List<Genre>();

            DbDataReader reader = Database.GetData(sSQL, dbParams);
            while (reader.Read())
            {
                list.Add(Fill(reader));
            }
            return list;
        }
        public static Genre FindById(int id)
        {
            //0.vars          
            //1. SQL instructie 
            string sql = "SELECT * FROM Band_Genre INNER JOIN Genres ON Band_Genre.GenreID=Genres.ID WHERE BandID=@bandid";

            //2. SQL parameters
            DbParameter idPar = Database.AddParameter("@bandID", id);

            //3. Haal data op en controleer op null/lege velden
            return GetList(sql, idPar)[0];
        }


        private static Genre Fill(DbDataReader reader)
        {
            Genre genre = new Genre();
            genre.ID = (int)reader["ID"];
            genre.Name = reader["Name"].ToString();
            return genre;
        }
    }
}