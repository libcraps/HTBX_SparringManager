using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.CrossLine
{
    public class CrossLine : MonoBehaviour
    {
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
        private StructScenarios crossLineControllerStruct;

        public static List<float> mouvementConsign;
        public static List<float> timeListScenario;

        void Start()
        {
            lineRigidComponent = GetComponent<Rigidbody>();
            scenarioControllerComponent = GetComponent<ScenarioController>();
            crossLineControllerStruct = scenarioControllerComponent._controllerStruct;

            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();
        
            float _timer = crossLineControllerStruct._timerScenario;

            _accelerationMax = crossLineControllerStruct._accelerationMax;
            _deltaTimeMax = crossLineControllerStruct._deltaTimeMax;
            _deltaTimeMin = crossLineControllerStruct._deltaTimeMin;
            _deltaHit = crossLineControllerStruct._deltaHit;
            _timeBeforeHit = crossLineControllerStruct._timeBeforeHit;

            _startScenario = Time.time;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //initialisation de l'accélération et du temps
            _tTime = Time.time - _startScenario;
            _previousTime = _tTime;
            _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {
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
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (canHit && CrossLineController._hitted == false) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
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
            if ((_tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration = randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            }
        }
        void LineInCameraRange()
        {
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject cameraObject = GameObject.Find("RenderCamera_Hitbox1");
            Debug.Log("caemrag,e   " + cameraObject);
            Camera renderCamera = cameraObject.GetComponent<Camera>();
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
