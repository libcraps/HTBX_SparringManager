using SparringManager;
using SparringManager.Scenarios;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
using SparringManager.SimpleLine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

namespace SparringManager.HitLine
{
    /* Class nof the HitLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the HitLine prefab.
     *  
     *  Attributs :
     *      //Usefull parameters of the scenario, they are in the splhitLineStructure
     *      int _accelerationMax : Maximum acceleration that the line can have
     *      int _deltaTimeMax : Maximum time before the line change its acceleration
     *      int _deltaTimeMin : Minimum time before the line change its acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line
     *      
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      float _previousTime : Time that we keep in memory every changement of the comportement of the line
     *      float _reactTime :
     *      float _timerScenario :
     *      
     *      // CONTAINERS
     *      ScenarioController _scenarioControllerComponent : Allows us to stock the StructScenarios structure that comes from SessionManager (scenarios[i])
     *      StructScenarios _controllerStruct : We stock in the _controllerStruct the structure that is in the _scenarioControllerComponent
     *      SplHitLineStruct _splHitLineControllerStruct : We stock the part SplHitLineStruct of the _controllerStruct
     *      SplHitLineDataStruct _splHitLineData : Structure that will contain the data of the SplHitline scenario
     *      
     *      GameObject _scenarioComposant : Prefab of the line
     *      SplHitLineBehaviour _splHitLineComponent : SplHitLineBehaviour component of the prefab, it gives u acces ti different variable of the splHitLine Prefab
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
     */
    public class HitLineController : MonoBehaviour
    {
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the hitLineStructure
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

        public static int nbApparition = 0;

        //Object that contain datas (structures)
        private ScenarioObj _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private HitLineStruct _hitLineControllerStruct;
        private HitLineDataStruct _hitLineDataStruct;

        [SerializeField]
        private GameObject _scenarioComposant; //HitLine Prefab
        private HitLineBehaviour _hitLineComponent;

        //List of the data that we will export 
        private DataManager.DataController _dataManagerComponent;
        private List<Vector3> _mouvementConsign;
        private List<float> _timeListScenario;

        //------------ METHODS -------------------
        //General Methods
        private void Awake()
        {
            nbApparition += 1;
            //INITIALISATION OF VARIABLES 
            //Scenario Variables
            _controllerStruct = GetComponent<ScenarioObj>().ControllerStruct;
            _hitLineControllerStruct = _controllerStruct.HitLineStruct;
            SetControllerVariables();

            //Export Data Variables
            _dataManagerComponent = GetComponentInParent<DataManager.DataController>();
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, _dataManagerComponent.StructToDictionary<HitLineStruct>(_hitLineControllerStruct));
            _mouvementConsign = new List<Vector3>();
            _timeListScenario = new List<float>();

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            Debug.Log(this.gameObject.name + " for " + _timerScenario + " seconds");
        }
        void Start()
        {
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _hitLineComponent = this.gameObject.GetComponentInChildren<HitLineBehaviour>();
            SetPrefabComponentVariables(_hitLineComponent);
        }
        private void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);

            //Stock the tTime data in list
            GetConsigne(_tTime, _hitLineComponent.transform.localPosition);
        }
        void OnDestroy()
        {
            _dataManagerComponent.GetScenarioExportDataInStructure(_timeListScenario, _mouvementConsign);
            _dataManagerComponent.EndScenarioForData = true;

            GetComponentInParent<SessionManager>().EndScenario = true;

            _reactTime = 0;
            _hitLineComponent.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Method that set variables.
        private void SetPrefabComponentVariables(HitLineBehaviour hitLineBehaviour)
        {
            hitLineBehaviour.TimeBeforeHit = _hitLineControllerStruct.TimeBeforeHit;
            hitLineBehaviour.DeltaHit = _hitLineControllerStruct.DeltaHit;
            hitLineBehaviour.FixPosHit = _hitLineControllerStruct.FixPosHit;
        }
        private void SetControllerVariables()
        {
            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _hitLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _hitLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _hitLineControllerStruct.DeltaTimeMin;
            _timeBeforeHit = _hitLineControllerStruct.TimeBeforeHit;
            _deltaHit = _hitLineControllerStruct.DeltaHit;
        }

        //Method for the data exportation
        private void GetConsigne(float time, Vector3 pos)
        {
            _mouvementConsign.Add(pos);
            _timeListScenario.Add(time);
        }

        //Method that change parameters of a moving object
        private void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _hitLineComponent.DeltaTimeChangeAcceleration)
            {
                _hitLineComponent.LineAcceleration = random.Next(-accelerationMax, accelerationMax);
                _hitLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

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

            if (rayOnTarget && canHit && _hitLineComponent.Hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;
                _hitLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

    }
}