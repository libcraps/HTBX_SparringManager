using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager.CrossLine
{
    public struct CrossLineDataStruct
    {
        private bool _hitted;
        private float _reactionTime;
        private List<float> _mouvementConsigneX;
        private List<float> _mouvementConsigneY;
        private List<float> _timeListScenario;
        private DataTable _crossLineDataTable; //Tableau contenant les listes de temps

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
        public List<float> MouvementConsigneX
        {
            get
            {
                return _mouvementConsigneX;
            }
            set
            {
                _mouvementConsigneX = value;
            }
        }
        public List<float> MouvementConsigneY
        {
            get
            {
                return _mouvementConsigneY;
            }
            set
            {
                _mouvementConsigneY = value;
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
        public DataTable CrossLineDataTable
        {
            get
            {
                _crossLineDataTable = CreateDataTable(_timeListScenario, _mouvementConsigneX, _mouvementConsigneY);
                return _crossLineDataTable;
            }
            set
            {
                _crossLineDataTable = value;
            }
        }

        public CrossLineDataStruct(bool hitted, float reactionTime, List<float> mouvementConsigneX, List<float> mouvementConsigneY, List<float> timeListSession, DataTable dataTable = null)
        {
            this._hitted = hitted;
            this._reactionTime = reactionTime;
            this._timeListScenario = timeListSession;
            this._mouvementConsigneX = mouvementConsigneX;
            this._mouvementConsigneY = mouvementConsigneY;
            this._crossLineDataTable = dataTable;
        }

        public DataTable CreateDataTable(List<float> timeListSession, List<float> mouvementConsigneX, List<float> mouvementConsigneY)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Time", typeof(float));
            table.Columns.Add("Consigne X", typeof(float));
            table.Columns.Add("Consigne Y", typeof(float));

            for (int i = 0; i < timeListSession.Count; i++)
            {
                table.Rows.Add(timeListSession[i], mouvementConsigneX[i], mouvementConsigneY[i]);
            }
            return table;
        }

    }
}
