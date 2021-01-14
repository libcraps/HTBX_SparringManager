using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager.SplHitLine
{
    public struct SplHitLineDataStruct
    {
        private bool _hitted;
        private float _reactionTime;
        private List<float> _mouvementConsigne;
        private List<float> _timeListScenario;
        private DataTable _splHitLineDataTable; //Tableau contenant les listes de temps

        public bool Hitted
        {
            get
            {
                return _hitted;
            }
            set
            {
                _hitted = value;
            }
        }
        public float ReactionTime
        {
            get
            {
                return _reactionTime;
            }
            set
            {
                _reactionTime = value;
            }
        }
        public List<float> MouvementConsigne
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
        public DataTable SplHitLineDataTable
        {
            get
            {
                _splHitLineDataTable = CreateDataTable(_timeListScenario, _mouvementConsigne);
                return _splHitLineDataTable;
            }
            set
            {
                _splHitLineDataTable = value;
            }
        }

        public SplHitLineDataStruct(bool hitted, float reactionTime, List<float> mouvementConsigne, List<float> timeListSession, DataTable dataTable = null)
        {
            this._hitted = hitted;
            this._reactionTime = reactionTime;
            this._timeListScenario = timeListSession;
            this._mouvementConsigne = mouvementConsigne;
            this._splHitLineDataTable = dataTable;
        }

        public DataTable CreateDataTable(List<float> timeListSession, List<float> mouvementConsign)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Time", typeof(float));
            table.Columns.Add("Consigne", typeof(float));

            for (int i = 0; i < timeListSession.Count; i++)
            {
                table.Rows.Add(timeListSession[i], mouvementConsign[i]);
            }
            return table;
        }
        
    }
}
