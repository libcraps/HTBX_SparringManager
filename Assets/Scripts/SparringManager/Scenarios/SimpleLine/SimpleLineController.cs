using SparringManager.Scenarios;
using SparringManager.DataManager;
using SparringManager.DataManager.SimpleLine;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    /* Class nof the SimpleLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the SimpleLine prefab, and the data handling of the scenario.
     *  
     *  Attributs :
     *      //Usefull parameters of the scenario, they are in the simpleLineStructure
     *      int _accelerationMax : Maximum acceleration that the line can have
     *      int _deltaTimeMax : Maximum time before the line change its acceleration
     *      int _deltaTimeMin : Minimum time before the line change its acceleration
     *      
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      float _previousTime : Time that we keep in memory every changement of the comportement of the line
     *      float _timerScenario
     *      
     *      // CONTAINERS
     *      ScenarioController _scenarioControllerComponent : Allows us to stock the StructScenarios structure that comes from SessionManager (scenarios[i])
     *      StructScenarios _controllerStruct : We stock in the _controllerStruct the structure that is in the _scenarioControllerComponent
     *      SplHitLineStruct _splHitLineControllerStruct : We stock the part SimpleLineStruct of the _controllerStruct
     *      SimpleLineDataStruct _simpleLineData : Structure that will contain the data of the SplHitline scenario
     *      
     *      GameObject _scenarioComposant : Prefab of the line
     *      SplHitLineBehaviour _splHitLineComponent : SimpleLineBehaviour component of the prefab, it gives u acces ti different variable of the splHitLine Prefab
     *      
     *      List<float> mouvementConsign : List that contain all the position of the line
     *      List<float> timeListScenario : Time list of the scenario
     *      
     *      Methods :
     *      //Methods that set variables
     *      void SetControllerVariables() : Set variables of the controller
     *      void SetPrefabComponentVAriables(): Set variables of the prefab component
     *      
     *      //Method for the data exportation
     *      void GetConsigne(float time, float pos) : Get the tTime data in a list
     *      void GetExportDataInStructure() : Put the data that we need in the dataStruture of the controller
     *      void ExportDataInDataManager() : Export the data into the DataManager
     *      
     *      //Method that change parameters of a moving object
     *      void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : Randomize the movement of the cross
     *      
     */
    public class SimpleLineController : MonoBehaviour
    {
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the SimpleLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _timerScenario;

        private float _previousTime;
        private float _tTime;
        private float _startTimeScenario;

        public static int nbApparition;
        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private SimpleLineStruct _simpleLineControllerStruct;
        private SimpleLineDataStruct _simpleLineDataStruct;

        [SerializeField]
        private GameObject _scenarioComposant;
        private SimpleLineBehaviour _simpleLineComponent;

        //List of the data that we will export 
        private DataManager.DataManager _dataManagerComponent;
        private List<float> _mouvementConsign;
        private List<float> _timeListScenario;

        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            nbApparition += 1;
            //INITIALISATION OF VARIABLES 
            //Scenario Variables
            _controllerStruct = new StructScenarios();
            _scenarioControllerComponent = GetComponent<ScenarioController>();

            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _simpleLineControllerStruct = _controllerStruct.SimpleLineStruct;
            SetControllerVariables();

            //Export Data variables
            _dataManagerComponent = this.gameObject.transform.parent.GetComponentInParent<DataManager.DataManager>();
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, _dataManagerComponent.StructToDictionary<SimpleLineStruct>(_simpleLineControllerStruct));

            _mouvementConsign = new List<float>();
            _timeListScenario = new List<float>();

            //Initialisation of the time
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

            //Stock the tTime data in list
            GetConsigne(_tTime, _simpleLineComponent.transform.position.x);
        }
        void OnDestroy()
        {
            GetExportDataInStructure();
            ExportDataInDataManager();

            _dataManagerComponent.EditFile = true;
            GetComponentInParent<SessionManager>().ChildDestroyed = true;
            Debug.Log(this.gameObject.name + " has been destroyed");
        }

        //Methods that set variables        
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

        //Method for the data exportation
        private void GetConsigne(float time, float pos)
        {
            //Put tTime's data in list
            _mouvementConsign.Add(pos);
            _timeListScenario.Add(time);
        }
        private void GetExportDataInStructure()
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _simpleLineDataStruct.MouvementConsigne = _mouvementConsign;
            _simpleLineDataStruct.TimeListScenario = _timeListScenario;
        }
        private void ExportDataInDataManager()
        {
            //Export the dataStructure in the datamanager
            _dataManagerComponent.DataBase.Add(_simpleLineDataStruct.SimpleLineDataTable);
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
