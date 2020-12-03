using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.HitLine
{
    public class HitLine : MonoBehaviour
    {
        private GameObject _hitLine;
        private int _accelerationMax;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private int _deltaHit;
        private int _timeBeforeHit;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;
        private float _lineAcceleration; 
        private System.Random _randomTime = new System.Random();
        private System.Random _randomAcceleration = new System.Random();
        private Rigidbody _lineRigidComponent;

        // Start is called before the first frame update
        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();

            //We get the component SessionsManager from the render camera to gte access to the timer
            GameObject _Session = GameObject.Find(this.gameObject.transform.parent.name);
            SessionManager session = _Session.GetComponent<SessionManager>();

            //We get the component SimpleLineController from the render camera to gte access to the timer
            GameObject _HitLineController = GameObject.Find("Scenario_" + this.gameObject.name);
            HitLineController hitLineController = _HitLineController.GetComponent<HitLineController>();

            _accelerationMax = hitLineController._accelerationMax;
            _deltaTimeMax = hitLineController._deltaTimeMax;
            _deltaTimeMin = hitLineController._deltaTimeMin;

            _deltaHit = hitLineController._deltaHit;
            _timeBeforeHit = hitLineController._timeBeforeHit;

            Debug.Log(_accelerationMax);
            float _timer = session._timer;
            
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
            _tTime = Time.time;

            if ((_tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration = _randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = _tTime;
                _deltaTime = _randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            }

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

            GameObject gameObject = GameObject.Find(this.gameObject.transform.parent.name);
            Camera renderCamera = gameObject.GetComponent<Camera>();
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
        voi
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
