using SparringManager.DataManager.CrossLine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.CrossLine
{
    /* Class nof the CrossLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the CrossLine prefab.
     *  The SplHitLine only moves lateraly and vertically and it instantiates an hit at the bottom or the top of the line afeter _timeBeforeHit seconds
     *  The part of the line scale that the player have to hit scale itself in an aleatory direction
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
     *      float _reactTime : 
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
     *      GetHit(Vector2 position2d_) :
     *      GetConsigne(float time, float pos) : 
     *      RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : 
     *      void SetLineToHit() : Choose which part of the line will be hitted
     *      void SetControllerVariables() : Set variables of the controller
     *      void SetPrefabComponentVAriables(): Set variables of the prefab component
     */
    public class CrossLineController : MonoBehaviour
    {
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the crossLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;

        private float _previousTime;
        private float _tTime;
        private float _reactTime;
        private float _startTimeScenario;
        private float _timerScenario;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private CrossLineStruct _crossLineControllerStruct;
        private CrossLineDataStruct _crossLineDataStruct;

        [SerializeField]
        private GameObject _scenarioComposant;
        private CrossLineBehaviour _crossLineComponent;

        //list of the data that we will export
        private DataManager.DataManager _dataManagerComponent;
        private List<float> _mouvementConsigneX;
        private List<float> _mouvementConsigneY;
        private List<float> _timeListScenario;

        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            //Scenario Variables
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _crossLineControllerStruct = _controllerStruct.CrossLineStruct;
            SetControllerVariables();

            //Export Data Variables
            _dataManagerComponent = GetComponentInParent<DataManager.DataManager>();
            _mouvementConsigneX = new List<float>();
            _mouvementConsigneY = new List<float>();
            _timeListScenario = new List<float>();

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

            _crossLineComponent = GetComponentInChildren<CrossLineBehaviour>();
            SetPrefabComponentVariables();
        }
        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);

            //Stock the tTime data in list
            GetConsigne(_tTime, _crossLineComponent.transform.position.x, _crossLineComponent.transform.position.y);
        }
        void OnDestroy()
        {
            GetExportDataInStructure();
            ExportDataInDataManager();
            _dataManagerComponent.EditFile = true;
            _reactTime = 0;
            _crossLineComponent.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Methods that set variables
        private void SetPrefabComponentVariables()
        {
            _crossLineComponent.TimeBeforeHit = _crossLineControllerStruct.TimeBeforeHit;
            _crossLineComponent.DeltaHit = _crossLineControllerStruct.DeltaHit;
            _crossLineComponent.FixPosHit = _crossLineControllerStruct.FixPosHit;
        }
        private void SetControllerVariables()
        {
            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _crossLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _crossLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _crossLineControllerStruct.DeltaTimeMin;
            _timeBeforeHit = _crossLineControllerStruct.TimeBeforeHit;
            _deltaHit = _crossLineControllerStruct.DeltaHit;
        }

        //Method for the data exportation
        private void GetConsigne(float time, float posX, float posY)
        {
            _timeListScenario.Add(time);
            _mouvementConsigneX.Add(posX);
            _mouvementConsigneY.Add(posY);
        }
        private void GetExportDataInStructure()
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _crossLineDataStruct.MouvementConsigneX = _mouvementConsigneX;
            _crossLineDataStruct.MouvementConsigneY = _mouvementConsigneY;
            _crossLineDataStruct.TimeListScenario = _timeListScenario;
            _crossLineDataStruct.Hitted = _crossLineComponent.Hitted;
            _crossLineDataStruct.ReactionTime = _reactTime;
        }
        private void ExportDataInDataManager()
        {
            //Export the dataStructure in the datamanager
            _dataManagerComponent.DataBase.Add(_crossLineDataStruct.CrossLineDataTable);
        }

        //Method that change parameters of a moving object
        void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _crossLineComponent.DeltaTimeChangeAcceleration)
            {
                _crossLineComponent.LineAcceleration[0] = random.Next(-accelerationMax, accelerationMax);
                _crossLineComponent.LineAcceleration[1] = random.Next(-accelerationMax, accelerationMax);
                _crossLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

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
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _crossLineComponent.Hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;
                _crossLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }
    }
}