using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.HitLine
{
    /* Class nof the CrossLine Scenario
     * 
     *  Summary :
     *  This Scenario represents a line that can move lateraly and set a hit for the player
     *  This class animate the line
     *  
     *  Importants Attributs :
     *      scenariocontroller scenariocontrollercomponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, delta hit, etc...) -> it is in the structure controllerstruct
     *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure hitLineStruct that contains the structure HitLineStruct)
     *      HitLineStruct hitLineControllerStruct : It is the structure that contain ONLY the HitLineScenario's parameters
     *      
     *  Methods :
     *  void Start() :
     *  void onDestroy() :
     *  void FixedUpdate() :
     *  void MoveLine() :
     *  void GetConsigne() :
     *  void SetHit() :
     *  void RandomizeLineMovement() :
     *  Void LineInCameraRange() :
     */
    public class HitLine : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are in the crossLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;

        private float _startScenario;
        private float _lineAcceleration;
        private System.Random randomTime = new System.Random();
        private System.Random randomAcceleration = new System.Random();
        private Rigidbody lineRigidComponent;

        private ScenarioController scenarioControllerComponent;
        private StructScenarios controllerStruct;
        private HitLineStruct hitLineControllerStruct;

        //List of the data that we will export 
        public static List<float> mouvementConsign;
        public static List<float> timeListScenario;

        void Start()
        {

            //Initialize the scene
            lineRigidComponent = GetComponent<Rigidbody>();
            scenarioControllerComponent = GetComponent<ScenarioController>();
            controllerStruct = scenarioControllerComponent._controllerStruct;
            hitLineControllerStruct = controllerStruct.HitLineStruct;

            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();
        
            float _timer = controllerStruct.TimerScenario;

            //Initialisation of the parameters
            _accelerationMax = hitLineControllerStruct.AccelerationMax;
            _deltaTimeMax = hitLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = hitLineControllerStruct.DeltaTimeMin;
            _deltaHit = hitLineControllerStruct.DeltaHit;
            _timeBeforeHit = hitLineControllerStruct.TimeBeforeHit;

            _startScenario = Time.time;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //Initialisation of the time and the acceleration
            _tTime = Time.time - _startScenario;
            _previousTime = _tTime;
            _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {

            //Update the "situation" of the line
            _tTime = Time.time - _startScenario;
            SetHit(_tTime);
            GetConsigne(_tTime, this.gameObject.transform.position.x);
            RandomizeLineMovement(_tTime);
            MoveLine(_lineAcceleration);
            LineInCameraRange();
        }

        void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }

        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
        }
        void SetHit(float _tTime)
        {
            //change the color of the line if the player have to hit
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (canHit && HitLineController._hitted == false) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        void RandomizeLineMovement(float _tTime)
        {
            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration = randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            }
        }
        void LineInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject _HitLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject gameObject = GameObject.Find(_HitLineController.gameObject.transform.parent.name);
            Camera renderCamera = gameObject.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            renderCameraPos3d.x = renderCamera.transform.position.x;
            renderCameraPos3d.y = renderCamera.transform.position.y;
            renderCameraPos3d.z = renderCamera.transform.position.z;
            
            linePos3d.x = this.gameObject.transform.position.x;
            linePos3d.y = this.gameObject.transform.position.y;
            linePos3d.z = this.gameObject.transform.position.z;

            //Instruction whether the line gets out of the render camera range
            if (linePos3d.x > renderCameraPos3d.x + rangeSize)
            {
                linePos3d.x -= 2* rangeSize;
            } 
            else if (linePos3d.x < renderCameraPos3d.x - rangeSize)
            {
                linePos3d.x += 2* rangeSize;
            }

            this.gameObject.transform.position = linePos3d;
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
