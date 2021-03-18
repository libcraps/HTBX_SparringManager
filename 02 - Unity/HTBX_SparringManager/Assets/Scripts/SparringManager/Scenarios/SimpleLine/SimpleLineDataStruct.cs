using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager.SimpleLine
{
    /*
     * Structure for the export data
     */
    public struct SimpleLineDataStruct
    {
        private List<Vector3> _mouvementConsigne;
        private List<float> _timeListScenario;
        private DataTable _simpleLineDataTable; //DataTable that will contain every list of the DataStruct

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
        public DataTable SimpleLineDataTable
        {
            get
            {
                _simpleLineDataTable = CreateDataTable(_timeListScenario, _mouvementConsigne);
                return _simpleLineDataTable;
            }
            set
            {
                _simpleLineDataTable = value;
            }
        }

        public SimpleLineDataStruct(List<Vector3> mouvementConsigne, List<float> timeListScenario, DataTable dataTable = null)
        {
            this._timeListScenario = timeListScenario;
            this._mouvementConsigne = mouvementConsigne;
            this._simpleLineDataTable = dataTable;
        }

        public DataTable CreateDataTable(List<float> timeListScenario, List<Vector3> mouvementConsign)
        {
            //Create the data
            DataTable table = new DataTable();

            table.Columns.Add("Time" , typeof(float));
            table.Columns.Add("ConsigneX", typeof(float));

            for (int i = 0; i < timeListScenario.Count; i++)
            {
                table.Rows.Add(timeListScenario[i], mouvementConsign[i][0]);
            }
            return table;
        }
        
    }
}
