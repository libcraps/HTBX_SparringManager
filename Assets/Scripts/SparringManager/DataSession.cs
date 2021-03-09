using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    public class DataSession
    {
        DataSessionScenario dataSessionScenario;
        DataSessionMovuino dataSessionMovuino;
        DataSessionMovuinoXMM dataSessionMovuinoXMM;
        DataSessionPolar dataSessionPolar;

        public static T CreateDataObject<T>() where T : DataSession, new()
        {
            T dataObject = new T();
            return dataObject;
        }

        public virtual DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            //table = dataSessionScenario.CreateDataTable() + dataSessionMovuino.CreateDataTable();
            return table;
        }

        public virtual void StockData(params object[] list)
        {

        }
        public DataSession()
        {
            dataSessionScenario = new DataSessionScenario();
            dataSessionMovuino = new DataSessionMovuino();
            dataSessionMovuinoXMM = new DataSessionMovuinoXMM();
            dataSessionPolar = new DataSessionPolar();
        }

    }

    public class DataSessionScenario : DataSession
    {
        public List<object> consigne = new List<object>();
        public List<object> time = new List<object>();

        public Dictionary<string, string> scenarioSumUp = new Dictionary<string, string>();
        public override void StockData(params object[] list)
        {
            time.Add(list[0]);
            consigne.Add(list[1]);
        }

        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("time", typeof(object));
            table.Columns.Add("Consigne", typeof(object));

            for (int i = 0; i < consigne.Count; i++)
            {
                table.Rows.Add(time[i], consigne[i]);
            }
            return table;
        }
    }

    public class DataSessionMovuino : DataSession
    {
        public List<object> listTime = new List<object>();
        public List<object> listAcceleration = new List<object>();
        public List<object> listGyroscope = new List<object>();
        public List<object> listMagneto = new List<object>();

        
        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listAcceleration.Add(list[1]);
            listGyroscope.Add(list[2]);
            listMagneto.Add(list[3]);
        }
        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Time", typeof(object));
            table.Columns.Add("Acceleration", typeof(object));
            table.Columns.Add("Gyroscope", typeof(object));
            table.Columns.Add("Magnetometre", typeof(object));

            for (int i = 0; i < listAcceleration.Count; i++)
            {
                table.Rows.Add(listTime[i], listAcceleration[i], listGyroscope[i], listMagneto[i]);
            }

            return table;
        }
    }
    public class DataSessionPolar : DataSession
    {
        public List<object> listBpm = new List<object>();
        public override void StockData(params object[] list)
        {
            listBpm.Add(list[0]);
        }
        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BPM", typeof(object));

            for (int i = 0; i < listBpm.Count; i++)
            {
                table.Rows.Add(listBpm[i]);
            }

            return table;
        }
    }
    public class DataSessionMovuinoXMM : DataSession
    {

        public override void StockData(params object[] list)
        {

        }
        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();


            return table;
        }
    }
}