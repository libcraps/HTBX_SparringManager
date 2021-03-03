using System.Collections;
using System.Collections.Generic;
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

        public abstract void StockData(List<object> list);

    }

    public class DataSessionScenario : DataSession
    {
        public List<object> consigne;
        public List<object> time;

        public override void StockData(List<object> list)
        {
            time.Add(list[0]);
            consigne.Add(list[1]);
        }
    }
    public class DataSessionMovuino : DataSession
    {
        public List<object> listAcceleration = new List<object>();
        public List<object> listGyroscope = new List<object>();
        public List<object> listMagneto = new List<object>();
        public override void StockData(List<object> list)
        {
            //listAcceleration.Add(list[0]);
            //listGyroscope.Add(list[1]);
            //listMagneto.Add(list[2]);
        }
    }
    public class DataSessionPolar : DataSession
    {

        public override void StockData(List<object> list)
        {

        }
    }
    public class DataSessionMovuinoXMM : DataSession
    {

        public override void StockData(List<object> list)
        {

        }
    }
}