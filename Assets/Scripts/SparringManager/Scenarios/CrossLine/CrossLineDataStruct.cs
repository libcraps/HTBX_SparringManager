﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace SparringManager.DataManager.CrossLine
{
    public struct CrossLineDataStruct
    {
        public bool hitted;
        public float reactionTime;
        public List<float> followingTarget;
        public List<float> timeListSession;
        public DataTable DataBase; //Tableau contenant les listes de temps

        public CrossLineDataStruct(bool hitted, float reactionTime, List<float> followingTarget, List<float> timeListSession, DataTable dataTable = null)
        {
            this.hitted = hitted;
            this.reactionTime = reactionTime;
            this.timeListSession = timeListSession;
            this.followingTarget = followingTarget;
            this.DataBase = dataTable;
            this.DataBase = CreateDataTable(timeListSession, followingTarget);
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
