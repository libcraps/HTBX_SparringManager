using System.Collections;
using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    public class SimpleLine : MonoBehaviour
    {
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
        private StructScenarios simpleLineControllerStruct;

        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();

            //initialisation des variables du scénario
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            simpleLineControllerStruct = _scenarioControllerComponent._controllerStruct;

            float _timer = simpleLineControllerStruct._timerScenario;
            _accelerationMax = simpleLineControllerStruct._accelerationMax;
            _deltaTimeMax = simpleLineControllerStruct._deltaTimeMax;
            _deltaTimeMin = simpleLineControllerStruct._deltaTimeMin;
            _startScenario = simpleLineControllerStruct._startScenario;

            
            Debug.Log(this.gameObject.name + " timer " + _timer);

            //initialisation de l'accélération et du temps
            _tTime = Time.time;
            _previousTime = _tTime;
            _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {
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