using SparringManager;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

namespace SparringManager.SplHitLine
{
    /*
     * Class of the SplHitLineController, it manage the HitLine and is in interaction with the Session Manager and the DataManager
     */
    public class SplHitLineController : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;
        private bool _fixPosHit; //Boolean to indicate if the line continue to move when the hit is setted

        private float _previousTime;
        private float _tTime;
        private float _deltaTime;
        private float _reactTime;
        private float _startTimeScenario;
        private float _timerScenario;
        private bool _hitted;
        private float _lineAcceleration;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private SplHitLineStruct _splHitLineControllerStruct;
        private HitLineDataStruct _splHitLineData;

        [SerializeField]
        private GameObject _scenarioComposant; //splHitLine
        private SplHitLineBehaviour _splHitLineComponent;

        //List of the data that we will export 
        private List<float> mouvementConsign;
        private List<float> timeListScenario;

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _splHitLineControllerStruct = _controllerStruct.SplHitLineStruct;
            SetControllerVariables();

            _hitted = false;
            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();

            Debug.Log(this.gameObject.name + " timer " + _timerScenario);

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            System.Random random = new System.Random();
            _deltaTime = random.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = random.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }
        void Start()
        {
            Vector3 _pos3d = new Vector3();
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _splHitLineComponent = this.gameObject.GetComponentInChildren<SplHitLineBehaviour>();
            SetComponentVariables();
            SetLineToHit(); // We define at the beginning of the scenario which line will be scale and in which direction
        }

        private void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement( _accelerationMax, _deltaTimeMin, _deltaTimeMax);
            GetConsigne(_tTime, _splHitLineComponent.transform.position.x);
        }

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
                _splHitLineComponent.Hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

        void OnDestroy()
        {
            _reactTime = 0;
            _hitted = false; 
            _splHitLineComponent.Hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
        }
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
        private void SetComponentVariables()
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
            _fixPosHit = _splHitLineControllerStruct.FixPosHit;
        }

    }
}