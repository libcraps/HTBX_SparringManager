using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager
{
    /*
     * Structure for the export data
     */
    public struct PlayerSceneDataStruct
    {
        private List<float> _mouvementPlayer;
        private List<float> _mouvementBag;
        private DataTable _PlayerDataTable; //DataTable that will contain every list of the DataStruct

        public List<float> MouvementPlayer
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
        public List<float> MouvementBag
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
        public DataTable PlayerSceneDataTable
        {
            get
            {
                _PlayerDataTable = CreateDataTable(_mouvementBag, _mouvementPlayer);
                return _PlayerDataTable;
            }
            set
            {
                _PlayerDataTable = value;
            }
        }

        public PlayerSceneDataStruct(List<float> mouvementConsigne, List<float> timeListScenario, DataTable dataTable = null)
        {
            this._mouvementBag = timeListScenario;
            this._mouvementPlayer = mouvementConsigne;
            this._PlayerDataTable = dataTable;
        }

        public DataTable CreateDataTable(List<float> mouvementPlayer, List<float> mouvementBag)
        {
            //Create the data
            DataTable table = new DataTable();

            table.Columns.Add("Player mouvement", typeof(float));
            table.Columns.Add("Consigne", typeof(float));

            for (int i = 0; i < mouvementPlayer.Count; i++)
            {
                table.Rows.Add(mouvementPlayer[i], mouvementBag[i]);
            }
            return table;
        }

    }
}
