using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.DataManager.CrossLine;
using UnityEngine;

namespace SparringManager.CrossLine
{
    /* Class nof the CrossLine Prefab
    * 
    *  Summary :
     *  This class leads the behaviour of the CrossLine prefab.
     *  The CrossLine moves lateraly and vertically and it instantiates the hit after _timeBeforeHit seconds. 
    *  
    *  Attributs :
    *      float[2] _lineAcceleration : Acceleration at a tTime of the crossline ([0] is ax and [1] i ay)
    *      int _deltaTimeChangeAcceleration : Time during which the line will keep tis acceleration
    *      float _timeBeforeHit : Time when the hit will be setted
    *      float _deltaHit : Time during which the player will be able to hit the line
    *      bool _hitted : Boolean that indicates fi the line is hitted or not
    *      bool _fixPosHit : Boolean that indicates if the line stop during the hit
    *      int _fixPosHitValue : if the boolean _fixPoshit is true we fix the value to 0 in order to have an acceleration null
    *      float _startTimeScenario : absolut time of the beginning of the scenario
    *      float _tTime : tTime
    *      
    *  Methods :
    *      void MoveLine(int lineAcceleration) : moves the line at the lineAcceleration
    *      Void LineInCameraRange() : Verifie that the line stay in the camera range
    *      void SetHit() : Indicates when the playe can hit by changing the color of the line
    */
    public class CrossLineBehaviour : MonoBehaviour
    {
        //General variables of a MovingLine
        private float[] _lineAcceleration;
        private int _deltaTimeChangeAcceleration;
        public float[] LineAcceleration
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

        void Start()
        {
            //Initialisation of the time
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;

            _lineAcceleration = new float[2];
        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            LineInCameraRange();
            MoveLine(_fixPosHitValue * _lineAcceleration[0], _fixPosHitValue * _lineAcceleration[1]);
            SetHit();
        }

        public void MoveLine(float lineHorizontalAcceleration, float lineVerticalAcceleration)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (lineHorizontalAcceleration, lineVerticalAcceleration, 0);
        }

        public void SetHit()
        {
            //change the color of the line if the player have to hit
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);
            GameObject VertLineObject = GameObject.Find(this.gameObject.transform.GetChild(0).name);
            GameObject HorizLineObject = GameObject.Find(this.gameObject.transform.GetChild(1).name);

            if (canHit && _hitted == false)
            {
                VertLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                HorizLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                if (_fixPosHit == true)
                {
                    _fixPosHitValue = 0;
                }
            }
            else
            {
                VertLineObject.GetComponent<MeshRenderer>().material.color = Color.white;
                HorizLineObject.GetComponent<MeshRenderer>().material.color = Color.white; 
                _fixPosHitValue = 1;
            }
        }

        void LineInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject cameraObject = GameObject.Find("RenderCamera_Hitbox1");
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
                linePos3d.y -= 2* rangeSize;
            } 
            else if (linePos3d.x < renderCameraPos3d.x - rangeSize)
            {
                linePos3d.y += 2* rangeSize;
            }
            //Instruction whether the line gets out of the render camera range
            if (linePos3d.y > renderCameraPos3d.y + rangeSize)
            {
                linePos3d.y -= 2 * rangeSize;
            }
            else if (linePos3d.y < renderCameraPos3d.y - rangeSize)
            {
                linePos3d.y += 2 * rangeSize;
            }
            this.gameObject.transform.position = linePos3d;
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
