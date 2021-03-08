using SparringManager.DataManager;
using SparringManager.Scenarios;
using System;
using System.Collections;
using System.Collections.Generic;
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

        GameObject scenarioInst;
        public static int nbApparition;

        private float _previousTime;
        private float _tTime;
        private float _reactTime;
        private float _startTimeScenario;

        private ScenarioCrossLine scenario;
        private DataSessionScenario dataScenario;
        private CrossLineBehaviour crossLineComponent;
        List<object> data;



        //list of the data that we will export
        private DataController _dataManagerComponent;
        private List<Vector3> _mouvementConsigne;
        private List<float> _timeListScenario;

        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            nbApparition += 1;
            //INITIALISATION OF VARIABLES 

            _mouvementConsigne = new List<Vector3>();
            _timeListScenario = new List<float>();

            data = new List<object>();

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            
        }
        void Start()
        {
            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;


            scenarioInst = Instantiate(PrefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            crossLineComponent = scenarioInst.GetComponent<CrossLineBehaviour>();
            crossLineComponent.SetPrefabComponentVariables(scenario.structScenario);
            Destroy(scenarioInst, scenario.timerScenario);


            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");

        }
        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);

            //Stock the tTime data in list
            GetScenarioData(_tTime, crossLineComponent.transform.localPosition);
            data.Add(_tTime);
            data.Add(crossLineComponent.transform.localPosition);
            dataScenario.StockData(data);
            data = new List<object>();
        }
        void OnDestroy()
        {
            DataController.testData.Add(dataScenario.CreateDataTable());
            _dataManagerComponent.GetScenarioExportDataInStructure(_timeListScenario, _mouvementConsigne);
            _dataManagerComponent.EndScenarioForData = true;
            GetComponentInParent<SessionManager>().EndScenario = true;
            _reactTime = 0;
            crossLineComponent.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            scenario = Scenario<CrossLineStruct>.CreateScenarioObject<ScenarioCrossLine>();
            scenario.Init(structScenarios);

            dataScenario = DataSession.CreateDataObject<DataSessionScenario>();

            //Export Data Variables
            _dataManagerComponent = GetComponentInParent<DataController>();
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, _dataManagerComponent.StructToDictionary<CrossLineStruct>(scenario.structScenario)); //Mettre dans 

        }



        //Method for the data exportation
        private void GetScenarioData(float time, Vector3 pos)
        {
            _timeListScenario.Add(time);
            _mouvementConsigne.Add(pos);
        }


        //Method that change parameters of a moving object
        void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > crossLineComponent.DeltaTimeChangeAcceleration)
            {
                crossLineComponent.LineAcceleration[0] = random.Next(-accelerationMax, accelerationMax);
                crossLineComponent.LineAcceleration[1] = random.Next(-accelerationMax, accelerationMax);
                crossLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                _previousTime = _tTime;

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
            bool canHit = (_tTime > scenario.timeBeforeHit && (_tTime - scenario.timeBeforeHit) < scenario.deltaHit);

            if (rayOnTarget && canHit && crossLineComponent.Hitted == false)
            {
                _reactTime = _tTime - scenario.timeBeforeHit;
                crossLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }
    }
}