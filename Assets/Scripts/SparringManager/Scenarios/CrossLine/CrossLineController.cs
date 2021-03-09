using SparringManager.DataManager;
using SparringManager.Scenarios;
using System;
using System.Collections;
using System.Collections.Generic;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios.CrossLine
{
    /* Class nof the CrossLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the CrossLine prefab.
     *  The CrossLine moves lateraly and vertically and it instantiates an hit after _timeBeforeHit seconds
     *  And the player will have to hit it less than _deltaHit seconds.
     *  
     *  Attributs :
     *      //Usefull parameters of the scenario, they are in the splhitLineStructure
     *      int _accelerationMax : Maximum acceleration that the line can have
     *      int _deltaTimeMax : Maximum time before the line change its acceleration
     *      int _deltaTimeMin : Minimum time before the line change its acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line

     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      float _previousTime : Time that we keep in memory every changement of the comportement of the line
     *      float _reactTime : reaction time culculated when a hit is detected
     *      float _timerScenario : Duration of the scenario
     *      
     *      // CONTAINERS
     *      ScenarioController _scenarioControllerComponent : Allows us to stock the StructScenarios structure that comes from SessionManager (scenarios[i])
     *      StructScenarios _controllerStruct : We stock in the _controllerStruct the structure that is in the _scenarioControllerComponent
     *      CrossLineStruct _crossLineControllerStruct : We stock the part SplHitLineStruct of the _controllerStruct
     *      CrossLineDataStruct _crossLineData : Structure that will contain the data of the SplHitline scenario
     *      
     *      GameObject _scenarioComposant : Prefab of the line
     *      CrossLineBehaviour _crossLineComponent : SplHitLineBehaviour component of the prefab, it gives u acces ti different variable of the splHitLine Prefab
     *      
     *      List<float> mouvementConsign : List that contain all the position of the line
     *      List<float> timeListScenario : Time list of the scenario
     *      
     *  Methods :
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
     *      Method for an hitting object
     *      void GetHit(Vector2 position2d_) : Get the Hit of the position2d_ (use events)
     *      
     */
    public class CrossLineController : ScenarioControllerBehaviour
    {
        //----------- ATTRIBUTS ----------------------
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

        public ScenarioCrossLine scenario { get; set; }
        private CrossLineBehaviour scenarioBehaviour;
        private DataSessionScenario dataScenario;
        private Movuino[] movuino;
        private DataSessionMovuino dataSessionMovuino;
        private DataSession dataSessionPlayer;
        //list of the data that we will export
        private DataController dataManagerComponent;

        private float previousTime;
        private float tTime;
        private float reactTime;
        private float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }

        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            DataSession dataSessionPlayer = DataSession.CreateDataObject<DataSession>();
            movuino[0] = GameObject.FindGameObjectsWithTag("Movuino")[0].GetComponent<Movuino>();
            movuino[1] = GameObject.FindGameObjectsWithTag("Movuino")[1].GetComponent<Movuino>();
            nbApparition += 1;
        }
        void Start()
        {
            //Initialisation of the time and the acceleration
            startTimeScenario = Time.time;
            tTime = Time.time - startTimeScenario;
            previousTime = tTime; 

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            //Instantiation of scenario display
            var go  = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<CrossLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenario);
            Destroy(go, scenario.timerScenario);

            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");
        }
        private void FixedUpdate()
        {
            //Behaviour Management
            tTime = Time.time - startTimeScenario;
            RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);

            //Data management
            dataScenario.StockData(tTime, scenarioBehaviour.transform.localPosition);
            //dataSessionMovuino.StockData(tTime, movuino.MovuinoSensorData.accelerometer, movuino.MovuinoSensorData.gyroscope, movuino.MovuinoSensorData.magnetometer);

        }
        void OnDestroy()
        {
            DataController.Database_static.Add(dataScenario.CreateDataTable());
            DataController.Database_static.Add(dataScenario.CreateDataTable());
            dataManagerComponent.ToCSVGlobal(DataController.Database_static, "OK.csv");
            dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;
            reactTime = 0;
            scenarioBehaviour.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<CrossLineStruct>.CreateScenarioObject<ScenarioCrossLine>();
            scenario.Init(structScenarios);

            //Export Data Variables
            dataScenario = DataSession.CreateDataObject<DataSessionScenario>();
            dataSessionMovuino = DataSession.CreateDataObject<DataSessionMovuino>();
            
            dataScenario.scenarioSumUp = DataController.StructToDictionary<CrossLineStruct>(scenario.structScenario);

            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataScenario.scenarioSumUp); //Mettre dans 

        }
        //Method that change parameters of a moving object
        void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > scenarioBehaviour.DeltaTimeChangeAcceleration)
            {
                scenarioBehaviour.LineAcceleration[0] = random.Next(-accelerationMax, accelerationMax);
                scenarioBehaviour.LineAcceleration[1] = random.Next(-accelerationMax, accelerationMax);
                scenarioBehaviour.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                previousTime = tTime;

            }
        }
        //Method for an hitting object
        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }
        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }
        public void GetHit(Vector2 position2d_)
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > scenario.timeBeforeHit && (tTime - scenario.timeBeforeHit) < scenario.deltaHit);

            if (rayOnTarget && canHit && scenarioBehaviour.Hitted == false)
            {
                reactTime = tTime - scenario.timeBeforeHit;
                scenarioBehaviour.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + reactTime);
            }
        }
    }
}