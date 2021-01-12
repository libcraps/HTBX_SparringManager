using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.SplHitLine
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
     *  void RandomizeLineMovement() :
     *  Void LineInCameraRange() :
     */
    public class SplHitLineBehaviour : MonoBehaviour
    {
        private GameObject _lineToHit;
        private Rigidbody _lineRigidComponent;
        private float _lineAcceleration;
        private int _deltaTimeChangeAcceleration;
        private float _timeBeforeHit;
        private float _deltaHit;
        private float _startTimeScenario;

        private float _previousTime;
        private float _tTime;

        private bool _upLineToHit;
        private bool _downLineToHit;
        private bool _hitted;

        private Vector3 _initScale;
        private Vector3 _localPos;
        private int _scaleMaxValue;
        private float _scaleSpeed;
        private int _scaleSide; //-1 ou 1

        public float LineAcceleration
        {
            get
            {
                return _lineAcceleration;
            }
            set
            {
                _lineAcceleration = value;
            }
        }
        public int DeltaTimeChangeAcceleration
        {
            get
            {
                return _deltaTimeChangeAcceleration;
            }
            set
            {
                _deltaTimeChangeAcceleration = value;
            }
        }
        public bool UpLineToHit
        {
            get
            {
                return _upLineToHit;
            }
            set
            {
                _upLineToHit = value;
            }
        }
        public bool DownLineToHit
        {
            get
            {
                return _downLineToHit;
            }
            set
            {
                _downLineToHit= value;
            }
        }
        public bool Hitted
        {
            get
            {
                return _hitted;
            }
            set
            {
                _hitted = value;
            }
        }
        public int ScaleSide
        {
            get
            {
                return _scaleSide;
            }
            set
            {
                _scaleSide = value;
            }
        }
        public int ScaleMaxValue
        {
            get
            {
                return _scaleMaxValue;
            }
            set
            {
                _scaleMaxValue = value;
            }
        }
        public float ScaleSpeed
        {
            get
            {
                return _scaleSpeed;
            }
            set
            {
                _scaleSpeed = value;
            }
        }
        public float DeltaHit
        {
            get
            {
                return _deltaHit;
            }
            set
            {
                _deltaHit = value;
            }
        }
        public float TimeBeforeHit
        {
            get
            {
                return _timeBeforeHit;
            }
            set
            {
                _timeBeforeHit = value;
            }
        }
        public GameObject LineToHit
        {
            get
            {
                return _lineToHit;
            }
            set
            {
                _lineToHit = value;
            }
        }

        void Start()
        {
            _lineRigidComponent = this.gameObject.GetComponent<Rigidbody>();
            _initScale = LineToHit.transform.localScale;
            _initScale.x = _initScale.x * _scaleSide;
            LineToHit.transform.localScale = _initScale;

            //Initialisation of the time and the acceleration
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;
            _previousTime = _tTime;

        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            LineInCameraRange();
            MoveLine(_lineAcceleration);
            SetHit(_lineToHit);
        }

        public void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }

        public void SetHit(GameObject LineObject)
        {
            Vector3 newScale;
            Vector3 linePos3d;

            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);

            newScale.x = LineObject.transform.localScale.x;
            newScale.y = LineObject.transform.localScale.y;
            newScale.z = LineObject.transform.localScale.z;

            linePos3d.x = LineObject.transform.localPosition.x;
            linePos3d.y = LineObject.transform.localPosition.y;
            linePos3d.z = LineObject.transform.localPosition.z;

            if (canHit && _hitted == false)
            {
                LineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                if (Mathf.Abs(LineObject.transform.localScale.x) < _scaleMaxValue)
                {
                    newScale.x += _scaleSide * _scaleSpeed;
                    linePos3d.x += _scaleSide * _scaleSpeed / 2;
                }
            }
            else
            {
                LineObject.GetComponent<MeshRenderer>().material.color = Color.white;
                if (Mathf.Abs(LineObject.transform.localScale.x) > Mathf.Abs(_initScale.x))
                {
                    newScale.x -= _scaleSpeed * _scaleSide;
                    linePos3d.x -= _scaleSide * _scaleSpeed / 2;
                }
            }

            LineObject.transform.localScale = newScale;
            LineObject.transform.localPosition = linePos3d;
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
