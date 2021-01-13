using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are in the SimpleLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaTime;

        private float _previousTime;
        private float _tTime;
        private float _startTimeScenario;
        private float _timerScenario;

        //Object that contain datas (structures)
        private StructScenarios _controllerStruct;
        private SimpleLineStruct _simpleLineControllerStruct;
        private ScenarioController _scenarioControllerComponent;

        [SerializeField]
        private GameObject _scenarioComposant;
        private SimpleLineBehaviour _simpleLineComponent;

        //List of the data that we will export 
        private List<float> mouvementConsign;
        private List<float> timeListScenario;

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _simpleLineControllerStruct = _controllerStruct.SimpleLineStruct;
            SetControllerVariables();

            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            Debug.Log(this.gameObject.name + " for " + _timerScenario + " seconds");
        }
        void Start()
        {

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _simpleLineComponent = GetComponentInChildren<SimpleLineBehaviour>();
        }

        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");

        }
        public void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _simpleLineComponent.DeltaTimeChangeAcceleration)
            {
                _simpleLineComponent.LineAcceleration = random.Next(-accelerationMax, accelerationMax);
                _simpleLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                _previousTime = _tTime;
            }
        }

        private void SetComponentVariables()
        {

        }

        private void SetControllerVariables()
        {
            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _simpleLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _simpleLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _simpleLineControllerStruct.DeltaTimeMin;
        }
    }   
}
