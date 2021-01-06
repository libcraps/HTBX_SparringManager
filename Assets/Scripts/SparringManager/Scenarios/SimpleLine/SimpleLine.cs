using System.Collections;
using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    /* Class nof the CrossLine Scenario
     * 
     *  Summary :
     *  This Scenario represents a line that can move right/left.
     *  This class animate the line
     *  
     *  Importants Attributs :
     *      scenariocontroller scenarioControllerComponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, etc...) -> it is in the structure controllerstruct
     *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure SimpleLineStruct that contains the structure SimpleLineStruct)
     *      SimpleLineStruct simpleLineControllerStruct : It is the structure that contain ONLY the SimpleLineScenario's parameters
     *      
     *  Methods :
     *  void Start() :
     *  void onDestroy() :
     *  void FixedUpdate() :
     *  void MoveLine() :
     *  void GetConsigne() :
     *  void RandomizeLineMovement() :
     *  Void LineInCameraRange() :
     */
    public class SimpleLine : MonoBehaviour
    {
        //Usefull parameters of the scenario, they are specified in the simpleLineStructure
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;

        private float _lineAcceleration;
        private float _startScenario;
        private System.Random _randomTime = new System.Random();
        private System.Random _randomAcceleration = new System.Random();

        private Rigidbody _lineRigidComponent;
        private ScenarioController _scenarioControllerComponent;
        private StructScenarios controllerStruct;
        private SimpleLineStruct simpleLineControllerStruct;

        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();

            _scenarioControllerComponent = GetComponent<ScenarioController>();
            controllerStruct = _scenarioControllerComponent._controllerStruct;
            simpleLineControllerStruct = controllerStruct.SimpleLineStruct;

            //Initialisation of the parameters
            float _timer = controllerStruct._timerScenario;
            _accelerationMax = simpleLineControllerStruct._accelerationMax;
            _deltaTimeMax = simpleLineControllerStruct._deltaTimeMax;
            _deltaTimeMin = simpleLineControllerStruct._deltaTimeMin;
            _startScenario = Time.time;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //Initialisation of the time and the acceleration
            _tTime = Time.time - _startScenario;
            _previousTime = _tTime;
            _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startScenario;
            RandomizeLineMovement(_tTime);
            MoveLine(_lineAcceleration);
            LineInCameraRange();
        }

        void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            _lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }

        void LineInCameraRange()
        {
           /* 
            * This method keeps the line in the camera range
            */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject _SimpleLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject _Camera = GameObject.Find(_SimpleLineController.gameObject.transform.parent.name);
            Camera renderCamera = _Camera.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            renderCameraPos3d.x = renderCamera.transform.position.x;
            renderCameraPos3d.y = renderCamera.transform.position.y;
            renderCameraPos3d.z = renderCamera.transform.position.z;
            
            linePos3d.x = this.gameObject.transform.position.x;
            linePos3d.y = this.gameObject.transform.position.y;
            linePos3d.z = this.gameObject.transform.position.z;

            //Instruction whether the line get out of the render camera range
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
        void RandomizeLineMovement(float _tTime)
        {

            //Randomize the movement of the line every deltaTime seconds
            if ((_tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            }
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
    }
}