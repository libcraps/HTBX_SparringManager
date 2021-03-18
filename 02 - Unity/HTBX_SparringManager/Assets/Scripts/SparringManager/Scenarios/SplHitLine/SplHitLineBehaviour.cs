using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Scenarios
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
    public class SplHitLineBehaviour : Scenario1DLineDisplay
    {
        //General variables of a MovingLine
        private SplHitLineStruct structScenari;
        private ScenarioSplHitLine scenario;

        //Variables of an Hitting Line
        //Variables of an Hitting Line
        private bool _hitted;
        private int _fixPosHitValue = 1; // if fix Pos hit == true we fix the value to 0 in order to have an acceleration null
        public float DeltaHit { get { return structScenari.DeltaHit; } }
        public float TimeBeforeHit { get { return structScenari.TimeBeforeHit; } }
        public bool FixPosHit { get { return structScenari.FixPosHit; } } //Boolean to indicate if the line continue to move when the hit is setted 
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

        //Global Time variable
        private float _startTimeScenario;
        private float _tTime;

        //Specific variables of SplHitLine

        private GameObject _lineToHit;
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
        public int ScaleMaxValue { get { return structScenari.ScaleMaxValue; } }
        public float ScaleSpeed { get { return structScenari.ScaleSpeed; } }

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
            ObjectInCameraRange();
            MoveLine(_fixPosHitValue * _lineAcceleration);
            SetHit(_lineToHit);
        }
        public override void Init(IStructScenario structScenari)
        {
            this.structScenari = (SplHitLineStruct)structScenari;
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

            bool canHit = (_tTime > TimeBeforeHit && (_tTime - TimeBeforeHit) < DeltaHit);

            newScale.x = LineObject.transform.localScale.x;
            newScale.y = LineObject.transform.localScale.y;
            newScale.z = LineObject.transform.localScale.z;

            linePos3d.x = LineObject.transform.localPosition.x;
            linePos3d.y = LineObject.transform.localPosition.y;
            linePos3d.z = LineObject.transform.localPosition.z;

            if (canHit && _hitted == false)
            {
                LineObject.GetComponent<MeshRenderer>().material.color = Color.red;

                if (Mathf.Abs(LineObject.transform.localScale.x) < ScaleMaxValue)
                {
                    newScale.x += _scaleSide * ScaleSpeed;
                    linePos3d.x += _scaleSide * ScaleSpeed / 2;
                }
                if (FixPosHit == true)
                {
                    _fixPosHitValue = 0;
                }
            }
            else
            {
                LineObject.GetComponent<MeshRenderer>().material.color = Color.white;
                if (Mathf.Abs(LineObject.transform.localScale.x) > Mathf.Abs(_initScale.x))
                {
                    newScale.x -= ScaleSpeed * _scaleSide;
                    linePos3d.x -= _scaleSide * ScaleSpeed / 2;
                }
                _fixPosHitValue = 1;
            }

            LineObject.transform.localScale = newScale;
            LineObject.transform.localPosition = linePos3d;
        }


        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
