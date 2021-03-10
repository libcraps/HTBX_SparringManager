﻿using SparringManager.DataManager;
using SparringManager.Device;
using SparringManager.Scenarios;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

namespace SparringManager.SplHitLine
{
    /* Class nof the SplHitLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the SplHitLine prefab.
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
     *      static int nbApparition : Integer that counts the number of instantiation of the scenario
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
    public class SplHitLineController : ScenarioControllerBehaviour
    {
        #region Attributs
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

        public ScenarioSplHitLine scenario { get; set; }
        private SplHitLineBehaviour scenarioBehaviour;

        public DataSessionPlayer dataSessionPlayer;
        public Movuino[] movuino;
        public DataSessionMovuino dataSessionMovuino;
        public DataSessionScenario dataScenario;

        //list of the data that we will export
        private DataController dataManagerComponent;

        private float previousTime;
        private float tTime;
        private float reactTime;
        private float startTimeScenario { get { return scenario.startTimeScenario; } set { scenario.startTimeScenario = value; } }
        #endregion

        //--------------------------    METHODS     ----------------------------------------
        // ---> General Methods
        private void Awake()
        {
            movuino = new Movuino[2];
            nbApparition += 1;
        }
        void Start()
        {
            //Initialisation of the time and the acceleration
            startTimeScenario = Time.time;
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            var go = Instantiate(_prefabScenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour = go.GetComponent<SplHitLineBehaviour>();
            scenarioBehaviour.Init(scenario.structScenario);
            Destroy(go, scenario.timerScenario);

            movuino[0] = GameObject.FindGameObjectsWithTag("Movuino")[0].GetComponent<Movuino>();
            movuino[1] = GameObject.FindGameObjectsWithTag("Movuino")[1].GetComponent<Movuino>();
            Debug.Log(this.gameObject.name + " for " + scenario.timerScenario + " seconds");

            SetLineToHit(); // We define at the beginning of the scenario which line will be scale and in which direction
        }
        private void FixedUpdate()
        {
            //Update the "situation" of the line
            tTime = Time.time - startTimeScenario;
            RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);

            //Data management
            dataScenario.StockData(tTime, scenarioBehaviour.transform.localPosition);
            dataSessionMovuino.StockData(tTime, movuino[0].MovuinoSensorData.accelerometer, movuino[0].MovuinoSensorData.gyroscope, movuino[0].MovuinoSensorData.magnetometer);
        }
        void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(DataSession.JoinDataTable(dataScenario.DataTable, dataSessionMovuino.DataTable));
            dataManagerComponent.EndScenarioForData = true;

            GetComponentInParent<SessionManager>().EndScenario = true;

            reactTime = 0;
            scenarioBehaviour.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        // ---> Methods that set variables
        public override void Init(StructScenarios structScenarios)
        {
            //Initialize this Class
            //Scenario controller
            scenario = Scenario<SplHitLineStruct>.CreateScenarioObject<ScenarioSplHitLine>();
            scenario.Init(structScenarios);


            dataSessionPlayer = DataSession.CreateDataObject<DataSessionPlayer>();
            dataScenario = DataSession.CreateDataObject<DataSessionScenario>();
            dataSessionMovuino = DataSession.CreateDataObject<DataSessionMovuino>();


            dataScenario.scenarioSumUp = DataController.StructToDictionary<SplHitLineStruct>(scenario.structScenario);
            dataManagerComponent = GetComponentInParent<DataController>();
            dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, dataScenario.scenarioSumUp); //Mettre dans 

        }

        // ---> Method that change parameters of a moving object
        private void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
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

// ---> Method for an hitting object
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

// ---> Specific method of the splHitLine scenario
        private void SetLineToHit()
        {
            /*
             * Methode that defines which part of the line the player will have to hit and in which direction it will scale
             * 
             */
            if (scenarioBehaviour.LineToHit == null)
            {
                System.Random random = new System.Random();
                int randomLine = random.Next(2);
                int randomScaleSide = random.Next(2);

                scenarioBehaviour.LineToHit = GameObject.Find(scenarioBehaviour.transform.GetChild(randomLine).name);

                if (randomScaleSide == 0)
                {
                    scenarioBehaviour.ScaleSide = -1;
                }
                else
                {
                    scenarioBehaviour.ScaleSide = 1;
                }
            }
        }
    }
}