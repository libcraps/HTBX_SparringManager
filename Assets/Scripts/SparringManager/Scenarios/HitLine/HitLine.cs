using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.HitLine
{
    public class HitLine : MonoBehaviour
    {
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;
        private float _previousTime;
        private float _previousHitTime;
        private float _tTime;
        private float _deltaTime;
        private float _startScenario;
        private float _lineAcceleration;
        private System.Random _randomTime = new System.Random();
        private System.Random _randomAcceleration = new System.Random();
        private Rigidbody _lineRigidComponent;

        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();

            //We get the component HitLineController from the render camera to gte access to the != variables
            GameObject _HitLineController = GameObject.Find(this.gameObject.transform.parent.name);
            HitLineController hitLineController = _HitLineController.GetComponent<HitLineController>();

            StructScenarios hitLineControllerStruct = hitLineController._controllerStruct;
            float _timer = hitLineControllerStruct._timerScenario;

            _accelerationMax = hitLineControllerStruct._accelerationMax;
            _deltaTimeMax = hitLineControllerStruct._deltaTimeMax;
            _deltaTimeMin = hitLineControllerStruct._deltaTimeMin;
            _deltaHit = hitLineControllerStruct._deltaHit;
            _startScenario = hitLineController._startScenario;
            _timeBeforeHit = hitLineControllerStruct._timeBeforeHit;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //initialisation de l'accélération et du temps
            _tTime = Time.time - _startScenario;
            _previousTime = _tTime;
            _previousHitTime = _tTime;
            _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Acceleration : " + _lineAcceleration);
            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startScenario;

            SetHit(_tTime);
            RandomizeLineMovement(_tTime);
            MoveLine(_lineAcceleration);
            LineInCameraRange();
        }

        void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            _lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }

        void SetHit(float _tTime)
        {
            GameObject _HitLineController = GameObject.Find("Scenario_" + this.gameObject.name);
            HitLineController hitLineController = _HitLineController.GetComponent<HitLineController>();

            bool _hitted = hitLineController._hitted;
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            if (canHit && _hitted == false)
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
                _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            }
        }
        void LineInCameraRange()
        {
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
