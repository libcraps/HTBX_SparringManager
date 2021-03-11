using SparringManager.DataManager;
using SparringManager.Device;
using SparringManager.Structures;
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
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        public static int nbApparition;
        //Scenario
        public ScenarioCrossLine scenario { get; set; }
        public CrossLineBehaviour scenarioBehaviour { get; set; }

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
            base.Start();
            //Initialisation of the time and the acceleration
            startTimeScenario = Time.time;
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;


            //Instantiation of scenario behaviour display
            Vector3 pos3d;
            pos3d.x = this.gameObject.transform.position.x;
            pos3d.y = this.gameObject.transform.position.y;
            pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(PrefabScenarioComposant, pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<CrossLineBehaviour>();
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
            dataSessionPlayer.DataSessionHit.StockData(tTime, scenarioBehaviour.Hitted);
            for (int i = 0; i < NbMovuino; i++)
            {
                dataSessionPlayer.DataSessionMovuino.StockData(tTime, movuino[i].MovuinoSensorData.accelerometer, movuino[i].MovuinoSensorData.gyroscope, movuino[i].MovuinoSensorData.magnetometer);
            }
        }
        void OnDestroy()
        {

            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);

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

            //StructPlayerSCene

            dataSessionPlayer = new DataSessionPlayer(NbMovuino);

            dataSessionPlayer.DataSessionScenario.scenarioSumUp = DataController.StructToDictionary<CrossLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataSessionPlayer.DataSessionScenario.scenarioSumUp); //Mettre dans 

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
        #endregion
    }
}