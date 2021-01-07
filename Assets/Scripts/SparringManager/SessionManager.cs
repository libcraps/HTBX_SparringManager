using System.Collections;
using System.Collections.Generic;
using System;
using SparringManager.SimpleLine;
using SparringManager.HitLine;
using SparringManager.CrossLine;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private StructScenarios[] scenarios; //List of StructScenarios, it contains every parameters of the session of the scenario

        private int indexScenario;
        private string nameSenarioI;

        string path;

        //Variables temporaires de scénarios
        private int _timerScenarioI;
        private float _timeStartScenarioI;

        void Start()
        {
            indexScenario = 0;

            _timerScenarioI = scenarios[indexScenario].TimerScenario;
            nameSenarioI = scenarios[indexScenario].ScenarioPrefab.name;
            _timeStartScenarioI = Time.time;
            
            InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position); 
        }
        private void Update()
        {
            if (((Time.time - _timeStartScenarioI) > _timerScenarioI) && (indexScenario < (scenarios.Length -1))) //If the last scenario ended and there is scenarios left
            {
                indexScenario += 1;

                _timerScenarioI = scenarios[indexScenario].TimerScenario;
                _timeStartScenarioI = Time.time;

                InstantiateAndBuildScenario(scenarios[indexScenario], this.gameObject, this.gameObject.transform.position);
            }
        }

        public static void InstantiateAndBuildScenario(StructScenarios strucObject, GameObject referenceGameObject, Vector3 _pos3d, GameObject prefabObject = null)
        {
            /*
             * Function that instatiate an object, the prefab of this object is in the structureScenarios, it contains all the data that is usefull for the scenarios
             * 
             * Parameters :
             *      strucObject : structure that contains parameters of the scenario, the type is the same for everyone because it allows us to unified the type and to choose a more specified type after
             *      referenceGameObject : use to choose the parent of our object, it often this.gameObjetct
             *      _pos3D : position where we want to instantiate our object
             *      prefabGameObject : it is here to be able to use this fonction for scenario composants (because the scenario_controller is in the structure and the scenario composent in the scenario controller)
             */

            if (prefabObject == false)
            {
                prefabObject = strucObject.ScenarioPrefab; //if we don't specified the prefab we used the prefab that is in the structure (so the prefab of the scenario)
            }

            if (_pos3d == null)
            {
                _pos3d = referenceGameObject.transform.position; //if the position isn't specified we place the object a the same place of the reference
            }

            ScenarioController scenarioControllerComponent = prefabObject.GetComponent<ScenarioController>();
            scenarioControllerComponent.ControllerStruct = strucObject; //we attribute the structure to the scenario component 
            Destroy(Instantiate(prefabObject, _pos3d, Quaternion.identity, referenceGameObject.transform), strucObject.TimerScenario);

            Debug.Log(prefabObject.name + " has been instantiated");
        }
    }
}