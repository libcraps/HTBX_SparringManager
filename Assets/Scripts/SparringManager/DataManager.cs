using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    public class DataManager : MonoBehaviour
    {
        public static ArrayList DataBase;
        List<string> ScenarioList;
        int i;
        string nameScenarioI;
        void Start()
        {
            DataBase = new ArrayList();
            ArrayList dataScenarioI = new ArrayList();
        }

        public override string ToString()
        {
            Debug.Log("Nombre de scenarios instanciés : " + DataBase.Count);
            return base.ToString();
        }
        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            /*
             * Stock une table de type DataTable dans un CSV
             * 
             * 
             */
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {

                    sw.Write(dr[i].ToString());
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
