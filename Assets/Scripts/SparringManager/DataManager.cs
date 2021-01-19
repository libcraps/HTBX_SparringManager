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
//--------------------------    ATTRIBUTS    -------------------------------
        [SerializeField]
        private bool _exportIntoFile;
        public bool ExportIntoFile
        {
            get
            {
                return _exportIntoFile;
            }
        }

        private bool _editFile = false;
        public bool EditFile
        {
            get
            {
                if (ExportIntoFile == false)
                {
                    _editFile = false;
                }
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
        private void Start()
        {
            //_sessionResume = new List<string>();
            _sessionSumUp = new Dictionary<string, Dictionary<string, string>>();
            _generaralSectionSumUp = new Dictionary<string, string>();
            _dataBase = new List<DataTable>();
        }

        //Methods we use to stock data in file
        public void ToCSV(DataTable dtDataTable, string strFilePath)
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
        }

        public Dictionary<string, string> StructToDictionary<StructType>(StructType structure)
        {
            Dictionary<string, string> dico = new Dictionary<string, string>();

            foreach (var field in typeof(StructType).GetProperties())
            {
                dico.Add(field.Name, field.GetValue(structure).ToString());
            }

            return dico;
        }

        public void AddContentToSumUp(string key, Dictionary<string, string> content)
        {
            _sessionSumUp.Add(key, content);
        }
        public void GetGeneralContentForSumUp()
        {
            
        }

    }
}
