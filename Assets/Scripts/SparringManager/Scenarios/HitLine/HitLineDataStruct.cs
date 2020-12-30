using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager.HitLine
{
    public struct HitLineDataStruct
    {
        public bool hitted;
        public float reactionTime;
        public List<float> followingTarget;
        public List<float> timeListSession;
        public DataTable HitLineDataBase; //Tableau contenant les listes de temps

        public HitLineDataStruct(bool hitted, float reactionTime, List<float> followingTarget, List<float> timeListSession, DataTable dataTable = null)
        {
            this.hitted = hitted;
            this.reactionTime = reactionTime;
            this.timeListSession = timeListSession;
            this.followingTarget = followingTarget;
            this.HitLineDataBase = dataTable;
            this.HitLineDataBase = CreateDataTable(timeListSession, followingTarget);
        }

        public override string ToString()
        {
            Debug.Log("HitLineData structure : ");
            Debug.Log("Hitted : " + hitted);
            Debug.Log("Reaction Time : " + reactionTime);
            Debug.Log("List length : " + timeListSession.Count);
            return "HitLine Data";
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
