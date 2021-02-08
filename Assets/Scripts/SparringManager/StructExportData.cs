using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.Structures
{
    /*
     * Structure for the export data
     */
    public struct StructExportData
    {
        private List<Vector3> _mouvementConsigne;
        private List<Vector3> _mouvementPlayer;
        private List<Vector3> _mouvementBag;
        
        private List<float> _timeListScenario;
        private DataTable _ExportDataTable; //DataTable that will contain every list of the DataStruct

        public List<Vector3> MouvementConsigne
        {
            get
            {
                return _mouvementConsigne;
            }
            set
            {
                _mouvementConsigne = value;
            }
        }
        public List<Vector3> MouvementPlayer
        {
            get
            {
                return _mouvementPlayer;
            }
            set
            {
                _mouvementPlayer = value;
            }
        }
        public List<Vector3> MouvementBag
        {
            get
            {
                return _mouvementBag;
            }
            set
            {
                _mouvementBag = value;
            }
        }
        public List<float> TimeListScenario
        {
            get
            {
                return _timeListScenario;
            }
            set
            {
                _timeListScenario = value;
            }
        }
        public DataTable ExportDataTable
        {
            get
            {
                _ExportDataTable = CreateDataTable(_timeListScenario, _mouvementConsigne, _mouvementPlayer);
                return _ExportDataTable;
            }
            set
            {
                _ExportDataTable = value;
            }
        }

        public StructExportData(List<Vector3> mouvementConsigne, List<Vector3> mouvementPlayer, List<Vector3> mouvementBag, List<float> timeListScenario, DataTable dataTable = null)
        {
            this._timeListScenario = timeListScenario;
            this._mouvementConsigne = mouvementConsigne;
            this._mouvementPlayer = mouvementPlayer;
            this._mouvementBag = mouvementBag;
            this._ExportDataTable = dataTable;
        }

        public DataTable CreateDataTable(List<float> timeListScenario,
            List<Vector3> mouvementConsign,
            List<Vector3> mouvementPlayer)
        {
            //Create the data
            DataTable table = new DataTable();

            table.Columns.Add("Time" , typeof(float));
            table.Columns.Add("ConsigneX", typeof(float));
            table.Columns.Add("ConsigneY", typeof(float));
            table.Columns.Add("ConsigneZ", typeof(float));

            table.Columns.Add("PlayerX", typeof(float));
            table.Columns.Add("PlayerY", typeof(float));
            table.Columns.Add("PlayerZ", typeof(float));

            for (int i = 0; i < timeListScenario.Count; i++)
            {
                table.Rows.Add(timeListScenario[i],
                    mouvementConsign[i][0],
                    mouvementConsign[i][1],
                    mouvementConsign[i][2],
                    mouvementPlayer[i][0],
                    mouvementPlayer[i][1],
                    mouvementPlayer[i][2]);
            }
            return table;
        }
        
    }
}
