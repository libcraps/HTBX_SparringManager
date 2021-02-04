using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.SplHitLine
{
    /* Class nof the SplHitLine Prefab
     * 
     *  Summary :
     *  This class leads the behaviour of the SplHitLine prefab.
     *  The SplHitLine only moves lateraly and vertically and it instantiates an hit at the bottom or the top of the line afeter _timeBeforeHit seconds
     *  The part of the line scale that the player have to hit scale itself in an aleatory direction
     *  
     *  Attributs :
     *      float _lineAcceleration : Acceleration at a tTime of the 
     *      int _deltaTimeChangeAcceleration : Time during which the line will keep tis acceleration
     *      float _timeBeforeHit : Time when the hit will be setted
     *      float _deltaHit : Time during which the player will be able to hit the line
     *      bool _hitted : Boolean that indicates fi the line is hitted or not
     *      bool _fixPosHit : Boolean that indicates if the line stop during the hit
     *      int _fixPosHitValue : if the boolean _fixPoshit is true we fix the value to 0 in order to have an acceleration null
     *      float _startTimeScenario : absolut time of the beginning of the scenario
     *      float _tTime : tTime
     *      GameObject _lineToHit : GameObject that representent the part of the line that  will be hitted
     *      int _scaleMaxValue : Maximum scale that the line can have
     *      float _scaleSpeed : Speed of the scale
     *      int _scaleSide : Takes the value -1 or 1 and it indicates in which side the line will scale
     *      Vector3 _initScale : Initial scale of the line
     *      
     *  Methods :
     *  void MoveLine(int lineAcceleration) : moves the line at the lineAcceleration
     *  Void LineInCameraRange() : Verifie that the line stay in the camera range
     *  void SetHit() : Indicates when the playe can hit by changing the color of the line
     */
    public class SplHitLineBehaviour : MonoBehaviour
    {
        //General variables of a MovingLine
        private float _lineAcceleration;
        private int _deltaTimeChangeAcceleration;
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

        //Variables of an Hitting Line
        private float _timeBeforeHit;
        private float _deltaHit;
        private bool _hitted;
        private bool _fixPosHit; //Boolean to indicate if the line continue to move when the hit is setted
        private int _fixPosHitValue = 1; // if fix Pos hit == true we fix the value to 0 in order to have an acceleration null

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
        public bool FixPosHit
        {
            get
            {
                return _fixPosHit;
            }
            set
            {
                _fixPosHit = value;
            }
        }

        //Global Time variable
        private float _startTimeScenario;
        private float _tTime;

        //Specific variables of SplHitLine
        // -> Variables presents in the SplHitLineStructure
        private GameObject _lineToHit;
        private int _scaleMaxValue;
        private float _scaleSpeed;
        private int _scaleSide; //-1 ou 1
        //UseFull only for SplHitLine
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

        // -> other usefull variables
        private Vector3 _initScale;

        void Start()
        {
            //Initialisation of the scale
            _initScale = LineToHit.transform.localScale;
            _initScale.x = _initScale.x * _scaleSide;
            LineToHit.transform.localScale = _initScale;

            //Initialisation of the time
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;

        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            LineInCameraRange();
            MoveLine(_fixPosHitValue * _lineAcceleration);
            SetHit(_lineToHit);
        }

        public void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(lineHorizontalAcceleration, 0, 0);
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
                if (_fixPosHit == true)
                {
                    _fixPosHitValue = 0;
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
                _fixPosHitValue = 1;
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

            GameObject _SimpleLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject _Camera = _SimpleLineController.transform.GetComponentInParent<DeviceManager>().RenderCamera;
            Camera renderCamera = _Camera.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            renderCameraPos3d.x = renderCamera.transform.localPosition.x;
            renderCameraPos3d.y = renderCamera.transform.localPosition.y;
            renderCameraPos3d.z = renderCamera.transform.localPosition.z;

            linePos3d.x = this.gameObject.transform.localPosition.x;
            linePos3d.y = this.gameObject.transform.localPosition.y;
            linePos3d.z = this.gameObject.transform.localPosition.z;

            //Instruction whether the line gets out of the render camera range
            if (linePos3d.x > renderCameraPos3d.x + rangeSize)
            {
                linePos3d.x -= 2 * rangeSize;
            }
            else if (linePos3d.x < renderCameraPos3d.x - rangeSize)
            {
                linePos3d.x += 2 * rangeSize;
            }

            this.gameObject.transform.localPosition = linePos3d;
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
