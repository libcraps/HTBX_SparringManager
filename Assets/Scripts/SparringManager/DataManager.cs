﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    public class DataManager : MonoBehaviour
    {
//---------------    ATTRIBUTS    ----------------------
        private SessionManager _SessionManagerComponent;

        private string _dataPath = ".\\_data\\";
        private string _sumUpNameTXT = "_sessionResume.txt";
        private string _dataFileNameCSV = "_dataSession";

        private bool _editFile = false;
        public bool EditFile
        {
            get
            {
                return _editFile;
            }
            set
            {
                _editFile = value;
            }
        }

        private List<DataTable> _dataBase;
        public List<DataTable> DataBase
        {
            get
            {
                return _dataBase;
            }
            set
            {
                _dataBase = value;
            }
        }

        //private List<string> _sessionResume; //List that sum up the session that we will put in a text file
        private Dictionary<string, List<string>> _sessionSumUp;
        private List<string> _introductionSumUpTXT;
        private List<string> _listScenarioI;
        private int _previousIndex = 0;

        //variables of scenario[i]
        private string _nameScenario;
        private int _indexScenario;

        //---------------     METHODS    -----------------------
        private void Awake()
        {
            //_sessionResume = new List<string>();
            _sessionSumUp = new Dictionary<string, List<string>>();
            _dataBase = new List<DataTable>();
            _SessionManagerComponent = GetComponent<SessionManager>();

        }
        void Start()
        {
            _indexScenario = _SessionManagerComponent.IndexScenarios;
            _nameScenario = _SessionManagerComponent.Scenarios[_indexScenario].ScenarioPrefab.name;
            _introductionSumUpTXT = completeGeneralDataList();
            _sessionSumUp.Add("General", _introductionSumUpTXT);
        }
        private void FixedUpdate()
        {
            if (_editFile == true)
            {
                //Compléter la liste SumUp avec le scénario actuel
                _indexScenario = _SessionManagerComponent.IndexScenarios;
                _nameScenario = _SessionManagerComponent.Scenarios[_indexScenario-1].ScenarioPrefab.name;
                ToCSV(_dataBase[_indexScenario-1], _dataPath + _nameScenario + ".csv");
            }
            _editFile = false;

            if (_indexScenario == _SessionManagerComponent.NbScenarios)
            {
                _indexScenario += 1;
            }
        }


        //Methods we use to stock data in file
        private void ToCSV(DataTable dtDataTable, string strFilePath)
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

        private List<string> completeGeneralDataList()
        {
            List<string> generalData = new List<string>();
            generalData.Add("Practice Session of the " + DateTime.Now);
            generalData.Add("Athlete : " + _SessionManagerComponent.Name);
            generalData.Add("Nombre de scenario : " + _SessionManagerComponent.NbScenarios);

            return generalData;
        }


    }
}
