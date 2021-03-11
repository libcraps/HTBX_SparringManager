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
        public static int nbApparition;
        //Object that contain datas (structures)
        public ScenarioSimpleLine scenario { get; set; }
        public SimpleLineBehaviour scenarioBehaviour;

        private float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            cameraObject = this.gameObject.transform.GetComponentInParent<DeviceManager>().RenderCamera;
            nbApparition += 1;


        }
        protected override void Start()
        {
            GetDevices();
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

            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            //Behaviour Management
            tTime = Time.time - startTimeScenario;
            RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);

            //Data management
            dataSessionPlayer.DataSessionScenario.StockData(tTime, scenarioBehaviour.transform.localPosition);
            dataSessionPlayer.DataSessionPolar.StockData(scenario.PosToAngle(cameraObject.GetComponent<Camera>().orthographicSize, scenarioBehaviour.transform.localPosition.x));//test angle
            for (int i = 0; i < NbMovuino; i++)
            {
                dataSessionPlayer.DataSessionMovuino.StockData(tTime, movuino[i].MovuinoSensorData.accelerometer, movuino[i].MovuinoSensorData.gyroscope, movuino[i].MovuinoSensorData.magnetometer);
            }
        }
        void OnDestroy()
        {
            //dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);

            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<SimpleLineStruct>.CreateScenarioObject<ScenarioSimpleLine>();
            scenario.Init(structScenarios);

            dataSessionPlayer = new DataSessionPlayer(NbMovuino);


            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<SimpleLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 
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