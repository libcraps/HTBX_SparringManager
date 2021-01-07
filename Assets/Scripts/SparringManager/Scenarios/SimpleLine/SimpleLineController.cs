using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;

        private float _lineAcceleration;
        private float _startTimeScenario;
        private System.Random _randomTime = new System.Random();
        private System.Random _randomAcceleration = new System.Random();

        private StructScenarios _controllerStruct;
        private SimpleLineStruct _simpleLineControllerStruct;
        private ScenarioController _scenarioControllerComponent;
        private SimpleLine _simpleLineComponent;

        [SerializeField]
        private GameObject _scenarioComposant;



        private void Awake()
        {
            _tTime = Time.time;
            _startTimeScenario = Time.time;

            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _simpleLineControllerStruct = _controllerStruct.SimpleLineStruct;

            //Initialisation of the parameters
            float _timer = _controllerStruct.TimerScenario;
            _accelerationMax = _simpleLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _simpleLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _simpleLineControllerStruct.DeltaTimeMin;
            _startTimeScenario = Time.time;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //Initialisation of the time and the acceleration
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;
            _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }
        void Start()
        {

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;
            SessionManager.InstantiateAndBuildScenario(_controllerStruct, this.gameObject, _pos3d, _scenarioComposant);

            _simpleLineComponent = GetComponentInChildren<SimpleLine>();
        }

        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_tTime, ref _previousTime, ref _deltaTime, ref _lineAcceleration, _accelerationMax, _deltaTimeMin, _deltaTimeMax);
            _simpleLineComponent.MoveLine(_lineAcceleration);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");

        }
        public void RandomizeParametersLineMovement(float tTime, ref float previousTime, ref float deltaTime, ref float lineAcceleration, int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > deltaTime)
            {
                lineAcceleration = random.Next(-accelerationMax, accelerationMax);
                previousTime = tTime;
                deltaTime = random.Next(deltaTimeMin, deltaTimeMax);
            }
        }
    }   
}
