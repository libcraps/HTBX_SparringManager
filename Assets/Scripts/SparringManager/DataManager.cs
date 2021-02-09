using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SparringManager.Structures;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    /* Class of the Data Manager
    * 
    *  Summary :
    *  This class manage the session :
    *      - it instantiates scenarios
    *      - it deals with the DataManager
    *  
    *  Attributs :
    *      //Usefull parameters of the class
    *      string _name :  Name of the player 
    *      StructScenarios[] _scenarios : List of StructScenarios, it contains every parameters of the session of the scenario
    *      int _indexScenario: index of the current secnario that is playing
    *      
    *      bool ChildDestroyed : Boolean that indicates if the session manager can launch the next scenario (it true when the current scenario is destroyed)
    *      
    *      //Variables for the DataManager
    *      string _filePath : Path of the data folder, it is initialized to .\_data\
    *      DataManager.DataManager _dataManager : DataManager component
    *      
    *  Methods :
    *      void InstantiateAndBuildScenario(StructScenarios strucObject, GameObject referenceGameObject, Vector3 _pos3d, GameObject prefabObject = null)
    */
    public class DataManager : MonoBehaviour
    {
//--------------------------    ATTRIBUTS    -------------------------------
        private bool _exportIntoFile;
        public bool ExportIntoFile
        {
            get
            {
                return _exportIntoFile;
            }
        }

        private bool _editDataTable = false;
        public bool EditDataTable
        {
            get
            {
                if (ExportIntoFile == false)
                {
                    _editDataTable = false;
                }
                return _editDataTable;
            }
            set
            {
                _editDataTable = value;
            }
        }

        public bool EndScenarioForData { get; set; }

        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
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

        private StructExportData _exportDataStruct;
        public StructExportData ExportDataStruct
        {
            get
            {
                return _exportDataStruct;
            }
            set
            {
                _exportDataStruct = value;
            }
        }


        //private List<string> _sessionResume; //List that sum up the session that we will put in a text file
        private Dictionary<string, Dictionary<string,string>> _sessionSumUp;
        public Dictionary<string, Dictionary<string, string>> SessionSumUp
        {
            get
            {
                return _sessionSumUp;
            }
            set
            {
                _sessionSumUp = value;
            }
        }

        private Dictionary<string, string> _generaralSectionSumUp;
        public Dictionary<string, string> GeneraralSectionSumUp
        {
            get
            {
                return _generaralSectionSumUp;
            }
            set
            {
                _generaralSectionSumUp = value;
            }
        }

        //---------------------------     METHODS    -------------------------------

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _exportDataStruct = new StructExportData();
            _sessionSumUp = new Dictionary<string, Dictionary<string, string>>();
            _generaralSectionSumUp = new Dictionary<string, string>();
            _dataBase = new List<DataTable>();
        }
        private void FixedUpdate()
        {
            if (_editDataTable == true)
            {
                _editDataTable = false;
                EndScenarioForData = false;
                _dataBase.Add(_exportDataStruct.ExportDataTable);
                _exportDataStruct = new StructExportData();
                
            }
        }
        private void OnDestroy()
        {
            if (_exportIntoFile == true) //We export the file t the end of the session if t
            {
                if (!Directory.Exists(_filePath))
                {
                    Debug.Log(_filePath + " has been created");
                    Directory.CreateDirectory(_filePath);
                }
                DicoToTXT(_sessionSumUp, _filePath + "SessionSumUp.txt");
                //_dataManager.ToCSV(_dataManager.DataBase[_indexScenario - 1], ".\\_data\\" + GetNameScenarioI(_indexScenario - 1) + ".csv");
                ToCSVGlobal(_dataBase, _filePath+ "GlobalSessionData.csv");

            }
        }

        //--> Methods we use to stock data in file
        public void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            /*
             * Stock une DataTable in a csv
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
        public void ToCSVGlobal(List<DataTable> dtDataBase, string strFilePath)
        {
            /*
             * Stock a List of DataTable in a csv
             */
            StreamWriter sw = new StreamWriter(strFilePath, false);

            List<string> sumUpKeys = new List<string>();
            sumUpKeys = new List<string>(_sessionSumUp.Keys);

            for (int j = 1; j < sumUpKeys.Count;j++)
            {
                DataTable dtDataTable = dtDataBase[j-1];
                sw.Write(sumUpKeys[j]);
                sw.Write(sw.NewLine);
                //We add the data of the session
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
                sw.Write(sw.NewLine);
            }
            sw.Close();
            Debug.Log(strFilePath + " has been created");
        }
        public void DicoToTXT(Dictionary<string, Dictionary<string, string>> dico, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            
            foreach (string globalKey in dico.Keys)
            {
                sw.Write("--> " + globalKey + " :");
                sw.Write(sw.NewLine);
                
                foreach(string key in dico[globalKey].Keys)
                {
                    sw.Write("  - " + key + " : " + dico[globalKey][key]);
                    sw.Write(sw.NewLine);
                }
                sw.WriteLine(sw.NewLine);
            }
            sw.Close();
            Debug.Log(strFilePath + " has been created");
        }

//--> Methods that manage data conteners
        public Dictionary<string, string> StructToDictionary<StructType>(StructType structure)
        {
            /* 
             * Generic method that go throw a structure and get her data into a dictionary
             * 
             * Arguments :
             *      StructType structure : StructType is the generique of the function, and structure is the structure that we extract her data
             *      
             * Return :
             *      A dictionary Dictionary<string, string> that contain the data 
             */
            Dictionary<string, string> dico = new Dictionary<string, string>();

            foreach (var field in typeof(StructType).GetProperties())
            {
                dico.Add(field.Name, field.GetValue(structure).ToString());
            }

            return dico;
        }
        public void AddContentToSumUp(string key, Dictionary<string, string> content)
        {
            /*
             * Method that Add to the _sessionSumUp dictionary a new item "content" a the key "key"
             * 
             * Arguments : 
             *      string key : key of the content
             *      Dictionary<string, string> content : Dictionary of a new content
             */
            _sessionSumUp.Add(key, content);
        }
        public void InitSumUp(string name, string filepath, int NbScenarios)
        {
            //Initialization of the GeeralSectionSumUp
            this.GeneraralSectionSumUp.Add("Date : ", DateTime.Now.ToString());
            this.GeneraralSectionSumUp.Add("Athlete : ", name);
            this.GeneraralSectionSumUp.Add("File path : ", filepath);
            this.GeneraralSectionSumUp.Add("Nb scenarios : ", NbScenarios.ToString());
            this.AddContentToSumUp("General", this.GeneraralSectionSumUp);
        }
        public void Init(bool export, string filepath)
        {
            //Initialization of the GeeralSectionSumUp
            _exportIntoFile = export;
            _filePath = filepath;
        }

        public void GetScenarioExportDataInStructure(List<float> timeListScenario, List<Vector3> mouvementConsigne)
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _exportDataStruct.MouvementConsigne = mouvementConsigne;
            _exportDataStruct.TimeListScenario = timeListScenario;
        }
        public void GetSceneExportDataInStructure(List<Vector3> mouvementPlayer, List<Vector3> mouvementBag)
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _exportDataStruct.MouvementPlayer = mouvementPlayer;
            _exportDataStruct.MouvementBag = mouvementBag;
        }
    }
}
