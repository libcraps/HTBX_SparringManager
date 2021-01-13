using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    public class DataManager : MonoBehaviour
    {
        private SessionManager _SessionManagerComponent;

        private string _dataPath = "..\\..\\..\\_data\\";
        private string _sumUpTXT = "_sessionResume.txt";

        //private List<string> _sessionResume; //List that sum up the session that we will put in a text file
        private Dictionary<string, List<string>> _sessionSumUp;
        private List<string> _generalData;
        private List<string> _listScenarioI;
        private int _previousIndex = 0;

        private void Awake()
        {
            //_sessionResume = new List<string>();
            _sessionSumUp = new Dictionary<string, List<string>>();
            _generalData = new List<string>();
            _SessionManagerComponent = GetComponent<SessionManager>();
        }

        void Start()
        {
            _generalData = completeGeneralDataList();
            _sessionSumUp.Add("General", _generalData);
        }

        private void Update()
        {
            if (_previousIndex != _SessionManagerComponent.IndexScenarios)
            {
                //Compléter la liste SumUp avec le scénario actuel
                //Compléter les datas
                _previousIndex = _SessionManagerComponent.IndexScenarios;
            }
        }
        private List<string> completeGeneralDataList()
        {
            List<string> generalData = new List<string>();
            _SessionManagerComponent = GetComponent<SessionManager>();

            generalData.Add("Practice Session of the " + DateTime.Now);
            generalData.Add("Athlete : " + _SessionManagerComponent.Name);
            generalData.Add("Nombre de scenario : " + _SessionManagerComponent);

            return generalData;
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

        private void ToTXT()
        {

        }


    }
}
