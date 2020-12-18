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
        private int nbScenarios;

        [SerializeField]
        private int scenari;
 
        public StructScenarios(GameObject scenario, int nbScenarios, int scenari)
        {
            this.scenario = scenario;
            this.scenari = scenari;
            this.nbScenarios = nbScenarios;
        }
    }
}