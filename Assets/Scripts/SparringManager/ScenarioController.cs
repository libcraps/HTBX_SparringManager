using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager 
{
    public class ScenarioController : MonoBehaviour
    {

        public StructScenarios _controllerStruct;

        public int a { get; set; }

        [SerializeField]
        protected GameObject _scenarioPrefab;
        public void ControllerConstructor(StructScenarios scenarioStructure)
        {
            _controllerStruct = scenarioStructure;
        }

        public void affiche()
        {
            Debug.Log("Bonjourno");
        }
    }
}