using DBHelper;
using PortableClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class StageDA
    {
        public static List<Stage> GetStages()
        {
            string sSQL = "SELECT * FROM [Stages]";

            return GetList(sSQL);
        }
        private static List<Stage> GetList(string sSQL, params DbParameter[] dbParams)
        {
            List<Stage> list = new List<Stage>();

            DbDataReader reader = Database.GetData(sSQL, dbParams);
            while (reader.Read())
            {
                list.Add(Fill(reader));
            }
            return list;
        }
        public static Stage FindById(int stageID)
        {
            //0.vars          
            //1. SQL instructie 
            string sSQL = "SELECT * FROM [Stages]";
            sSQL += " WHERE [Stages].[Id] = @stageID";

            //2. SQL parameters
            DbParameter idPar = Database.AddParameter("@stageID", stageID);

            //3. Haal data op en controleer op null/lege velden
            return GetList(sSQL, idPar)[0];
        }


        private static Stage Fill(DbDataReader reader)
        {
            Stage stage = new Stage();
            stage.ID = (int)reader["ID"];
            stage.Name = reader["Name"].ToString();
            return stage;
        }

    }
}