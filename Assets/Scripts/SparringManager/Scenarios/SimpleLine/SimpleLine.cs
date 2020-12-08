using System.Collections;
using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    public class SimpleLine : MonoBehaviour
    {
        
        private GameObject _simpleLine;
        private int _accelerationMax;
        private int _deltaTimeMax;
        private int _deltaTimeMin;
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
            GameObject _SimpleLineController = GameObject.Find("Scenario_" + this.gameObject.name);
            SimpleLineController simpleLineController = _SimpleLineController.GetComponent<SimpleLineController>();

            _accelerationMax = simpleLineController._accelerationMax;
            _deltaTimeMax = simpleLineController._deltaTimeMax;
            _deltaTimeMin = simpleLineController._deltaTimeMin;

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

            GameObject _Camera = GameObject.Find(this.gameObject.transform.parent.name);
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
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
    }
}