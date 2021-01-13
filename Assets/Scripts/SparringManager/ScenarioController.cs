using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager 
{
    public class ScenarioController : MonoBehaviour
    {
        /*
         * Class that will be a component of every prefab, it allows us to stock parameters of our scenario
         */
        [SerializeField]
        private StructScenarios _controllerStruct;

        public StructScenarios ControllerStruct
        {
            get
            {
                return _controllerStruct;
            }
            set
            {
                _controllerStruct = value;
            }
        }

        public List<string> ToListString()
        {
            List<string> listString = new List<string>();

            return listString;
        }
    }
}