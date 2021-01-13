using SparringManager;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
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
     *      void SetControllerVariables() : Set variables of the controller
     *      void SetPrefabComponentVAriables(): Set variables of the prefab component
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
        private bool _hitted = false;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private HitLineStruct _hitLineControllerStruct;
        private HitLineDataStruct _hitLineData;

        [SerializeField]
        private GameObject _scenarioComposant; //HitLine Prefab
        private HitLineBehaviour _hitLineComponent;

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
            _hitLineControllerStruct = _controllerStruct.HitLineStruct;
            SetControllerVariables();

            _hitted = false;
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
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _hitLineComponent = this.gameObject.GetComponentInChildren<HitLineBehaviour>();
            SetPrefabComponentVariables();
        }
        private void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);
            GetConsigne(_tTime, _hitLineComponent.transform.position.x);
        }
        void OnDestroy()
        {
            _reactTime = 0;
            _hitted = false;
            _hitLineComponent.Hitted = true;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Method that set variables
        private void SetPrefabComponentVariables()
        {
            _hitLineComponent.TimeBeforeHit = _hitLineControllerStruct.TimeBeforeHit;
            _hitLineComponent.DeltaHit = _hitLineControllerStruct.DeltaHit;
            _hitLineComponent.FixPosHit = _hitLineControllerStruct.FixPosHit;
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
        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
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

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;
                _hitted = true;
                _hitLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

    }
}