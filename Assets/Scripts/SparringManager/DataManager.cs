using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            /*
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("C:\\Users\\Pierre\\Documents\\Text.csv");
            //Write a line of text
            sw.WriteLine("Hello World!!");
            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();*/
        }

        public static void AddData<T>(T Data, ArrayList DataBase)
        {
            DataBase.Add(Data);
        }
        private void WriteCSV()
        {
            
        }
        public override string ToString()
        {
            Debug.Log("Nombre de scenarios instancier : " + DataBase.Count);
            return base.ToString();
        }
    }
}
