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
     *      float _lineVelocity : Acceleration at a tTime of the 
     *      int _deltaTimeChangeVelocity : Time during which the line will keep tis acceleration
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

    /// <summary>
    /// Manage the behaviour of the SplHitLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class SplHitLineBehaviour : ScenarioDisplayBehaviour
    {
        //General variables of a MovingLine

        //Variables of an Hitting Line
        public float DeltaHit { get; set; }
        public float TimeBeforeHit { get; set; }
        public bool FixPosHit { get { return structScenari.FixPosHit; } } //Boolean to indicate if the line continue to move when the hit is setted 
        public bool Hitted
        {
            get
            {
                return hitted;
            }
            set
            {
                hitted = value;
            }
        }

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

        [SerializeField]
        private int _scaleMaxValue = 45;
        [SerializeField]
        private float _scaleSpeed = 2;

        // -> other usefull variables
        private Vector3 _initScale;

        protected override void Start()
        {
            base.Start();
            //Initialisation of the scale
            _initScale = LineToHit.transform.localScale;
            _initScale.x = _initScale.x * _scaleSide;
            LineToHit.transform.localScale = _initScale;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //time update
            ObjectInCameraRange();
            //RandomizeObjectMovement(structScenari.AccelerationMax, structScenari.DeltaTimeMin, structScenari.DeltaTimeMax);
            MoveObject(fixPosHitValue * objectVelocity);
            SetHit(_lineToHit);
        }

        public void SetHit(GameObject LineObject)
        {
            Vector3 newScale;
            Vector3 linePos3d;

            bool canHit = (tTime > TimeBeforeHit && (tTime - TimeBeforeHit) < DeltaHit);

            newScale.x = LineObject.transform.localScale.x;
            newScale.y = LineObject.transform.localScale.y;
            newScale.z = LineObject.transform.localScale.z;

            linePos3d.x = LineObject.transform.localPosition.x;
            linePos3d.y = LineObject.transform.localPosition.y;
            linePos3d.z = LineObject.transform.localPosition.z;

            if (canHit && base.hitted == false)
            {
                LineObject.GetComponent<MeshRenderer>().material.color = Color.red;

                if (Mathf.Abs(LineObject.transform.localScale.x) < _scaleMaxValue)
                {
                    newScale.x += _scaleSide * _scaleSpeed;
                    linePos3d.x += _scaleSide * _scaleSpeed / 2;
                }
                if (FixPosHit == true)
                {
                    fixPosHitValue = 0;
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
                fixPosHitValue = 1;
            }

            LineObject.transform.localScale = newScale;
            LineObject.transform.localPosition = linePos3d;
        }

    }
}
