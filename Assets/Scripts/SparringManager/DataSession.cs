using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    public abstract class DataSession
    {
        public static T CreateDataObject<T>() where T : DataSession, new()
        {
            T dataObject = new T();
            return dataObject;
        }

        public abstract DataTable CreateDataTable();

        public abstract void StockData(List<object> list);

    }

    public class DataSessionScenario : DataSession
    {
        public List<object> consigne = new List<object>();
        public List<object> time = new List<object>();

        public override void StockData(List<object> list)
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
        public List<object> listAcceleration = new List<object>();
        public List<object> listGyroscope = new List<object>();
        public List<object> listMagneto = new List<object>();
        public override void StockData(List<object> list)
        {
            listAcceleration.Add(list[0]);
            listGyroscope.Add(list[1]);
            listMagneto.Add(list[2]);
        }
        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Acceleration", typeof(object));
            table.Columns.Add("Gyroscope", typeof(object));
            table.Columns.Add("Magnetometre", typeof(object));

            for (int i = 0; i < listAcceleration.Count; i++)
            {
                table.Rows.Add(listAcceleration[i], listGyroscope[i], listMagneto[i]);
            }

            return table;
        }
    }
    public class DataSessionPolar : DataSession
    {
        public List<object> listBpm = new List<object>();
        public override void StockData(List<object> list)
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

        public override void StockData(List<object> list)
        {

        }
        public override DataTable CreateDataTable()
        {
            DataTable table = new DataTable();


            return table;
        }
    }
}