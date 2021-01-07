using SparringManager.DataManager.CrossLine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.CrossLine
{
   /* Class of the CrossLineController
    * 
    *  Summary :
    *  This class is the  controller of the crossLineScenario
    *  
    *  Importants Attributs :
    *      scenariocontroller scenariocontrollercomponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, delta hit, etc...) -> it is in the structure controllerstruct
    *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure crossLineStruct that contains the structure CrossLineStruct)
    *      CrossLineStruct crossLineControllerStruct : It is the structure that contain ONLY the CrossLineScenario's parameters
    *      
    *  Methods :
    */
    public class CrossLineController : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;

        private float _timerScenario;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;
        private float _reactTime;
        private float _startTimeScenario;
        private float[] _lineAcceleration;
        private bool _hitted = false;

        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private CrossLineStruct _crossLineControllerStruct;
        private CrossLineDataStruct _crossLineData;

        //list of the data that we will export
        private List<float> mouvementConsign;
        private List<float> timeListScenario;

        [SerializeField]
        private GameObject _scenarioComposant;
        private CrossLine _crossLineComponent;

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _crossLineControllerStruct = _controllerStruct.CrossLineStruct;

            _timerScenario = _controllerStruct.TimerScenario;
            _accelerationMax = _crossLineControllerStruct.AccelerationMax;
            _deltaTimeMax = _crossLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = _crossLineControllerStruct.DeltaTimeMin;
            _timeBeforeHit = _crossLineControllerStruct.TimeBeforeHit;
            _deltaHit = _crossLineControllerStruct.DeltaHit;

            _hitted = false;
            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();
            _lineAcceleration = new float[2];

            Debug.Log(this.gameObject.name + " timer " + _timerScenario);
            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

            System.Random random = new System.Random();
            _deltaTime = random.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration[0] = random.Next(-_accelerationMax, _accelerationMax);
            _lineAcceleration[1] = random.Next(-_accelerationMax, _accelerationMax);

        }

        void Start()
        {

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);
            _crossLineComponent = GetComponentInChildren<CrossLine>();
            _crossLineComponent.SetLineAcceleration(5);
        }

        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_tTime, ref _previousTime, ref _deltaTime, ref _lineAcceleration, _accelerationMax, _deltaTimeMin, _deltaTimeMax);

            _crossLineComponent.SetHit(_tTime, _timeBeforeHit, _deltaHit, _hitted);
            _crossLineComponent.MoveLine(_lineAcceleration[0], _lineAcceleration[1]);

            GetConsigne(_tTime, _crossLineComponent.transform.position.x);
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
            float tTime = Time.time - _startTimeScenario;
            float _timeBeforeHit = _crossLineControllerStruct.TimeBeforeHit;
            float _deltaHit = _crossLineControllerStruct.DeltaHit;

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = tTime - _timeBeforeHit;
                _hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

        void OnDestroy()
        {
            //GetData();
            //DataManager.DataManager.ToCSV(_crossLineData.DataBase, "C:\\Users\\IIFR\\Documents\\GitHub\\Hitbox_Test\\HTBX_SparringManager\\_data\\Tableau.csv");
            _reactTime = 0;
            _hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        void GetData()
        {
            
        }
        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
        }
        void RandomizeParametersLineMovement(float tTime, ref float previousTime, ref float deltaTime, ref float[] lineAcceleration, int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > deltaTime)
            {
                lineAcceleration[0] = random.Next(-accelerationMax, accelerationMax);
                lineAcceleration[1] = random.Next(-accelerationMax, accelerationMax);
                previousTime = tTime;
                deltaTime = random.Next(deltaTimeMin, deltaTimeMax);
            }
        }

    }
}