using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    /* Class nof the SimpleLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the SimpleLine prefab, the data handling of the scenario.
     *  
     *  Attributs :
     *      //Usefull parameters of the scenario, they are in the splhitLineStructure
     *      int _accelerationMax : Maximum acceleration that the line can have
     *      int _deltaTimeMax : Maximum time before the line change its acceleration
     *      int _deltaTimeMin : Minimum time before the line change its acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line
     *      
     *      bool _hitted : Boolean that indicates fi the line is hitted or not
     *      bool _fixPosHit : Boolean that indicates if the line stop during the hit
     *      int _fixPosHitValue : if the boolean _fixPoshit is true we fix the value to 0 in order to have an acceleration null
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      float _previousTime : Time that we keep in memory every changement of the comportement of the line
     *      
     *      // CONTAINERS
     *      ScenarioController _scenarioControllerComponent : Allows us to stock the StructScenarios structure that comes from SessionManager (scenarios[i])
     *      StructScenarios _controllerStruct : We stock in the _controllerStruct the structure that is in the _scenarioControllerComponent
     *      SplHitLineStruct _splHitLineControllerStruct : We stock the part SplHitLineStruct of the _controllerStruct
     *      SplHitLineDataStruct _splHitLineData : Structure that will contain the data of the SplHitline scenario
     *      
     *      GameObject _scenarioComposant : Prefab of the line
     *      SplHitLineBehaviour _splHitLineComponent : SplHitLineBehaviour component of the prefab, it gives u acces ti different variable of the splHitLine Prefab
     *      List<float> mouvementConsign : List that contain all the position of the line
     *      List<float> timeListScenario : Time list of the scenario

     *      
     *  Methods :
     *      GetHit(Vector2 position2d_) :
     *      GetConsigne(float time, float pos) : 
     *      RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : 
     *      void SetLineToHit() : Choose which part of the line will be hitted
     *      void SetControllerVariables() : Set variables of the controller
     *      void SetPrefabComponentVAriables(): Set variables of the prefab component
     */
    public class SimpleLineController : MonoBehaviour
    {
        //----------- ATTRIBUTS ----------------------
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

        //------------ METHODS -------------------
        //General Methods
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

        //MEthods that set variables        
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

        //Method that changes parameters of a moving object
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
    }   
}
