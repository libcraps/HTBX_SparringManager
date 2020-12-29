using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using SparringManager;
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

        public static void AddData<T>(T Data, ArrayList DataBase)
        {
            DataBase.Add(Data);
        }
        public override string ToString()
        {
            Debug.Log("Nombre de scenarios instancier : " + DataBase.Count);
            return base.ToString();
        }
        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    string value = dr[i].ToString();
                    if (value.Contains(","))
                    {
                        value = string.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
