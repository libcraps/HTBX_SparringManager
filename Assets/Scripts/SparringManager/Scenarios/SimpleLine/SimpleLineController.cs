using SparringManager.DataManager;
using SparringManager.Device;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
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
    public class SimpleLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the SimpleLineStructure
        [SerializeField]
        private GameObject _prefabScenarioComposant;
        public override GameObject PrefabScenarioComposant
        {
            get
            {
                return _prefabScenarioComposant;
            }
            set
            {
                _prefabScenarioComposant = value;
            }
        }

        public static int nbApparition;
        //Object that contain datas (structures)
        private ScenarioSimpleLine scenario { get; set; }
        private SimpleLineBehaviour scenarioBehaviour;

        public DataSessionPlayer dataSessionPlayer;
        public Movuino[] movuino;
        public DataSessionMovuino dataSessionMovuino;
        public DataSessionScenario dataScenario;

        //List of the data that we will export 
        private DataController dataManagerComponent;

        private float previousTime;
        private float tTime;
        private float reactTime;
        private float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            nbApparition += 1;
            movuino = new Movuino[2];

        }
        void Start()
        {
            //Initialisation of the time
            startTimeScenario = Time.time;
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<SimpleLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenario);
            Destroy(go, scenario.timerScenario);

            //Get to other devices
            movuino[0] = GameObject.FindGameObjectsWithTag("Movuino")[0].GetComponent<Movuino>();
            movuino[1] = GameObject.FindGameObjectsWithTag("Movuino")[1].GetComponent<Movuino>();
            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");
        }
        private void FixedUpdate()
        {
            tTime = Time.time - startTimeScenario;
            RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);

            //Data management
            dataScenario.StockData(tTime, scenarioBehaviour.transform.localPosition);
            dataSessionMovuino.StockData(tTime, movuino[0].MovuinoSensorData.accelerometer, movuino[0].MovuinoSensorData.gyroscope, movuino[0].MovuinoSensorData.magnetometer);
        }
        void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(DataSession.JoinDataTable(dataScenario.DataTable, dataSessionMovuino.DataTable));

            dataManagerComponent.ToCSVGlobal(dataManagerComponent.DataBase, "OKdac.csv");
            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<SimpleLineStruct>.CreateScenarioObject<ScenarioSimpleLine>();
            scenario.Init(structScenarios);

            dataSessionPlayer = DataSession.CreateDataObject<DataSessionPlayer>();
            dataScenario = DataSession.CreateDataObject<DataSessionScenario>();
            dataSessionMovuino = DataSession.CreateDataObject<DataSessionMovuino>();

            dataScenario.scenarioSumUp = DataController.StructToDictionary<SimpleLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataScenario.scenarioSumUp); //Mettre dans 
        }

        //Method that changes parameters of a moving object
        public void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > scenarioBehaviour.DeltaTimeChangeAcceleration)
            {
                scenarioBehaviour.LineAcceleration = random.Next(-accelerationMax, accelerationMax);
                scenarioBehaviour.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                previousTime = tTime;
            }
        }
        #endregion
    }
}