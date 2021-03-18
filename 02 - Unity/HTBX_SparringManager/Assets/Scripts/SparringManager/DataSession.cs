using System.Collections;
using SparringManager.Device;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    /*
     * Classes that allows us to deal with result of a session
     * Mother class : DataSession
     * 
     * With this class we can create data type and stock data in order to transfert everything to the dataController
     * Notice that the file will be created with a list of datatables
     * 
     * Data type :
     *      DataSessionPlayer : general data of a player (it has others data)
     *      DataSessionScenario : data of the scenario like target positions..etc
     *      DataSessionMovuino : data of movuino
     *      DataSessionPolar : data of the polar
     *      DataSessionMovuinoXMM : data of xmm
     *      DataSessonHit : data of hit
     *      
     */
    public abstract class DataSession
    {
        public static T CreateDataObject<T>() where T : DataSession, new()
        {
            T dataObject = new T();
            return dataObject;
        }

        public virtual DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            return table;
        }
        public static DataTable MergeDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            foreach (DataTable tab in data)
            {
                table.Merge(tab);
            }
            return table;
        }
        public static DataTable JoinDataTable(params DataTable[] dataToJoin)
        {
            /*
             * Join horizontally different datatables that are in dataToJoin
             */
            DataTable result = new DataTable();

            foreach (DataTable table in dataToJoin)
            {
                foreach (DataColumn column in table.Columns)
                {
                    result.Columns.Add(column.ColumnName);
                }
            }

            for (int i=0; i<dataToJoin[0].Rows.Count; i++)
            {
                DataRow dr = result.NewRow();
                foreach (DataTable dt in dataToJoin)
                {
                    foreach (DataColumn dc in dt.Columns)
                        dr[dc.ColumnName] = dt.Rows[i][dc.ColumnName];
                }
                result.Rows.Add(dr);

            }
            return result;
        }
        public virtual void StockData(params object[] list)
        {

        }
    }

    public class DataSessionPlayer : DataSession
    {
        /*
         * Class that represent all the data of a player during the session
         */
        public DataSessionScenario DataSessionScenario;
        public DataSessionMovuino[] DataSessionMovuino; //Because a player can have more than one movuino
        public DataSessionMovuinoXMM[] DataSessionMovuinoXMM;
        public DataSessionPolar DataSessionPolar;
        public DataSessionHit DataSessionHit;
        public DataSessionViveTracker DataSessionViveTracker;

        public DataTable DataTable { get { return CreateDataTable(); } }
        public override void StockData(params object[] list)
        {

        }
        public override DataTable CreateDataTable(params DataTable[] data) //TODO
        {
            DataTable result = new DataTable();
            result = JoinDataTable(DataSessionScenario.DataTable, DataSessionViveTracker.DataTable, DataSessionHit.DataTable, DataSessionPolar.DataTable);
            for(int i=0; i<DataSessionMovuino.Length;  i++)
            {
                result = JoinDataTable(result, DataSessionMovuino[i].DataTable, DataSessionMovuinoXMM[i].DataTable);
            }

            return result;
        }
        public DataSessionPlayer(int nbMov)
        {
            DataSessionScenario = new DataSessionScenario();
            DataSessionMovuino = new DataSessionMovuino[nbMov];
            DataSessionMovuinoXMM = new DataSessionMovuinoXMM[nbMov];
            DataSessionHit = new DataSessionHit();
            DataSessionPolar = new DataSessionPolar();
            DataSessionViveTracker = new DataSessionViveTracker();

            for (int i = 0; i < nbMov; i++)
            {
                DataSessionMovuino[i] = new DataSessionMovuino();
                DataSessionMovuinoXMM[i] = new DataSessionMovuinoXMM();
            }
        }

    }
    public class DataSessionScenario : DataSession
    {
        public List<object> consigne = new List<object>();
        public List<object> time = new List<object>();

        public Dictionary<string, string> scenarioSumUp = new Dictionary<string, string>();
        public DataTable DataTable { get { return this.CreateDataTable(); } }
        public override void StockData(params object[] list)
        {
            time.Add(list[0]);
            consigne.Add(list[1]);
        }

        public override DataTable CreateDataTable(params DataTable[] data)
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
        public string id;

        public List<object> listTime = new List<object>();
        public List<Vector3> listAcceleration = new List<Vector3>();
        public List<Vector3> listGyroscope = new List<Vector3>();
        public List<Vector3> listMagneto = new List<Vector3>();

        public DataTable DataTable { get { return this.CreateDataTable(); } }

        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listAcceleration.Add((Vector3)list[1]);
            listGyroscope.Add((Vector3)list[2]);
            listMagneto.Add((Vector3)list[3]);
        }
        public override DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Ax " + id, typeof(float));
            table.Columns.Add("Ay " + id, typeof(float));
            table.Columns.Add("Az " + id, typeof(float));
            table.Columns.Add("Gx " + id, typeof(float));
            table.Columns.Add("Gy " + id, typeof(float));
            table.Columns.Add("Gz " + id, typeof(float));
            table.Columns.Add("Mx " + id, typeof(float));
            table.Columns.Add("My " + id, typeof(float));
            table.Columns.Add("Mz " + id, typeof(float));

            for (int i = 0; i < listAcceleration.Count; i++)
            {
                table.Rows.Add(
                    listAcceleration[i].x, listAcceleration[i].y, listAcceleration[i].z,
                    listGyroscope[i].x, listGyroscope[i].y, listGyroscope[i].z, 
                    listMagneto[i].x, listMagneto[i].y, listMagneto[i].z);
            }

            return table;
        }
    }
    public class DataSessionPolar : DataSession
    {
        public List<object> listTime = new List<object>();
        public List<object> listBpm = new List<object>();

        public DataTable DataTable { get { return this.CreateDataTable(); } }

        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listBpm.Add(list[1]);
        }
        public override DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("BPM", typeof(object));

            for (int i = 0; i < listTime.Count; i++)
            {
                table.Rows.Add(listBpm[i]);
            }

            return table;
        }
    }
    public class DataSessionMovuinoXMM : DataSession
    {
        public string id;

        public List<object> listTime = new List<object>();
        public List<object> listGestureID= new List<object>();
        public List<object> listGestureProb= new List<object>();
        public DataTable DataTable { get { return this.CreateDataTable(); } }

        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listGestureID.Add(list[1]);
            listGestureProb.Add(list[2]);
        }
        public override DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Gesture " + id, typeof(object));
            table.Columns.Add("probability" + id, typeof(object));

            for (int i = 0; i < listTime.Count; i++)
            {
                table.Rows.Add(listGestureID[i], listGestureProb[i]);
            }


            return table;
        }
    }
    public class DataSessionHit : DataSession
    {
        public List<object> listTime = new List<object>();
        public List<object> listHit = new List<object>();
        public List<object> listReacTime = new List<object>();

        public float nbHit;
        public DataTable DataTable { get { return this.CreateDataTable(); } }
        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listHit.Add(list[1]);
        }
        public override DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Hit", typeof(object));

            for (int i = 0; i < listHit.Count; i++)
            {
                table.Rows.Add(listHit[i]);
            }

            return table;
        }
    }
    public class DataSessionViveTracker : DataSession
    {
        public List<object> listTime = new List<object>();
        public List<object> listAngle = new List<object>();

        public DataTable DataTable { get { return this.CreateDataTable(); } }
        public override void StockData(params object[] list)
        {
            listTime.Add(list[0]);
            listAngle.Add(list[1]);
        }
        public override DataTable CreateDataTable(params DataTable[] data)
        {
            DataTable table = new DataTable();

            table.Columns.Add("PlayerMvt", typeof(object));

            for (int i = 0; i < listAngle.Count; i++)
            {
                table.Rows.Add(listAngle[i]);
            }

            return table;
        }
    }
}