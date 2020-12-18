using UnityEngine;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using System.Collections.Generic;

namespace SparringManager
{
    [System.Serializable]
    public struct StructScenarios
    {
        [SerializeField]
        private GameObject scenario;

        [SerializeField]
        private int _timerScenario;

 
        public StructScenarios(GameObject scenario, int time)
        {
            this.scenario = scenario;
            this._timerScenario = time;
        }
    }
}