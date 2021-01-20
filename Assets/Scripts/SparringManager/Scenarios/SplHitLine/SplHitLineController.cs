using SparringManager.DataManager.SplHitLine;
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
    public class SplHitLineController : MonoBehaviour
    {
//--------------------------    ATTRIBUTS     ----------------------------------------
        //Usefull parameters of the scenario, they are in the splhitLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;

        private float _timerScenario;
        private float _previousTime;
        private float _tTime;
        private float _reactTime;
        private float _startTimeScenario;
        
        public static int nbApparition;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private SplHitLineStruct _splHitLineControllerStruct;
        private SplHitLineDataStruct _splHitLineDataStruct;

        [SerializeField]
        private GameObject _scenarioComposant; //splHitLine
        private SplHitLineBehaviour _splHitLineComponent;

        //List of the data that we will export 
        private DataManager.DataManager _dataManagerComponent;
        private List<float> _mouvementConsigne;
        private List<float> _timeListScenario;

//--------------------------    METHODS     ----------------------------------------
// ---> General Methods
        private void Awake()
        {
            nbApparition += 1;
            //INITIALISATION OF VARIABLES 
            //Scenario Variables
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _splHitLineControllerStruct = _controllerStruct.SplHitLineStruct;
            SetControllerVariables();

            //Export Data Variables
            _dataManagerComponent = GetComponentInParent<DataManager.DataManager>();
            _dataManagerComponent.AddContentToSumUp(this.name + "_" + nbApparition, _dataManagerComponent.StructToDictionary<SplHitLineStruct>(_splHitLineControllerStruct));

            _mouvementConsigne = new List<float>();
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

            _splHitLineComponent = this.gameObject.GetComponentInChildren<SplHitLineBehaviour>();
            SetPrefabComponentVariables();
            SetLineToHit(); // We define at the beginning of the scenario which line will be scale and in which direction
        }
        private void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);

            //Stock the tTime data in lists
            GetConsigne(_tTime, _splHitLineComponent.transform.position.x);
        }
        void OnDestroy()
        {
            GetExportDataInStructure();
            ExportDataInDataManager();

            _dataManagerComponent.EditFile = true;
            GetComponentInParent<SessionManager>().ChildDestroyed = true;

            _reactTime = 0;
            _splHitLineComponent.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

// ---> Methods that set variables
        private void SetPrefabComponentVariables()
        {
            _splHitLineComponent.TimeBeforeHit = _splHitLineControllerStruct.TimeBeforeHit;
            _splHitLineComponent.DeltaHit = _splHitLineControllerStruct.DeltaHit;
            _splHitLineComponent.ScaleMaxValue = _splHitLineControllerStruct.ScaleMaxValue;
            _splHitLineComponent.ScaleSpeed = _splHitLineControllerStruct.ScaleSpeed;
            _splHitLineComponent.FixPosHit = _splHitLineControllerStruct.FixPosHit;
        }
        private void SetControllerVariables()
        {
            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _splHitLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _splHitLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _splHitLineControllerStruct.DeltaTimeMin;
            _timeBeforeHit = _splHitLineControllerStruct.TimeBeforeHit;
            _deltaHit = _splHitLineControllerStruct.DeltaHit;
        }

// ---> Methods for the data exportation
        private void GetConsigne(float time, float pos)
        {
            _mouvementConsigne.Add(pos);
            _timeListScenario.Add(time);
        }
        private void GetExportDataInStructure()
        {
            //Put the export data in the dataStructure, it is call at the end of the scenario (in the destroy methods)
            _splHitLineDataStruct.MouvementConsigne = _mouvementConsigne;
            _splHitLineDataStruct.TimeListScenario = _timeListScenario;
            _splHitLineDataStruct.Hitted = _splHitLineComponent.Hitted;
            _splHitLineDataStruct.ReactionTime = _reactTime;
        }
        private void ExportDataInDataManager()
        {
            //Export the dataStructure in the datamanager
            _dataManagerComponent.DataBase.Add(_splHitLineDataStruct.SplHitLineDataTable);
        }

// ---> Method that change parameters of a moving object
        private void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _splHitLineComponent.DeltaTimeChangeAcceleration)
            {
                _splHitLineComponent.LineAcceleration = random.Next(-accelerationMax, accelerationMax);
                _splHitLineComponent.DeltaTimeChangeAcceleration = random.Next(deltaTimeMin, deltaTimeMax);

                _previousTime = _tTime;
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
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _splHitLineComponent.Hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;

                _splHitLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

// ---> Specific method of the splHitLine scenario
        private void SetLineToHit()
        {
            /*
             * Methode that defines which part of the line the player will have to hit and in which direction it will scale
             * 
             */
            if (_splHitLineComponent.LineToHit == null)
            {
                System.Random random = new System.Random();
                int randomLine = random.Next(2);
                int randomScaleSide = random.Next(2);

                _splHitLineComponent.LineToHit = GameObject.Find(_splHitLineComponent.transform.GetChild(randomLine).name);

                if (randomScaleSide == 0)
                {
                    _splHitLineComponent.ScaleSide = -1;
                }
                else
                {
                    _splHitLineComponent.ScaleSide = 1;
                }
            }
        }
    }
}