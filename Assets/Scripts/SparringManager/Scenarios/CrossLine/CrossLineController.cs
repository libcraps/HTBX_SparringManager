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

        private float _previousTime;
        private float _tTime;
        private float _reactTime;
        private float _startTimeScenario;
        private float _timerScenario;
        private bool _hitted = false;

        //Object that contain datas (structures)
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios _controllerStruct;
        private CrossLineStruct _crossLineControllerStruct;
        private CrossLineDataStruct _crossLineData;

        [SerializeField]
        private GameObject _scenarioComposant;
        private CrossLineBehaviour _crossLineComponent;

        //list of the data that we will export
        private List<float> mouvementConsign;
        private List<float> timeListScenario;

        private void Awake()
        {
            //INITIALISATION OF VARIABLES 
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent.ControllerStruct;
            _crossLineControllerStruct = _controllerStruct.CrossLineStruct;
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
            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioComposant, _pos3d, Quaternion.identity, this.gameObject.transform), _timerScenario);

            _crossLineComponent = GetComponentInChildren<CrossLineBehaviour>();
            SetComponentVariables();
        }

        private void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            RandomizeParametersLineMovement(_accelerationMax, _deltaTimeMin, _deltaTimeMax);
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
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = _tTime - _timeBeforeHit;
                _hitted = true;
                _crossLineComponent.Hitted = true;

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
            _crossLineComponent.Hitted = true;
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
        void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax)
        {
            System.Random random = new System.Random();
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _crossLineComponent.DeltaTimeChangeAcceleration)
            {
                _crossLineComponent.LineAcceleration[0] = random.Next(-accelerationMax, accelerationMax);
                _crossLineComponent.LineAcceleration[1] = random.Next(-accelerationMax, accelerationMax);
                _crossLineComponent.DeltaTimeChangeAcceleration= random.Next(deltaTimeMin, deltaTimeMax);

                _previousTime = _tTime;

            }
        }
        private void SetComponentVariables()
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

    }
}