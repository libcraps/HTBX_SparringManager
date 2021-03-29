using UnityEngine;

namespace SparringManager.Scenarios.CrossLine
{
    /* Class nof the CrossLine Prefab
    * 
    *  Summary :
     *  This class leads the behaviour of the CrossLine prefab.
     *  The CrossLine moves lateraly and vertically and it instantiates the hit after _timeBeforeHit seconds. 
    *  
    *  Attributs :
    *      float[2] _lineVelocity : Acceleration at a tTime of the crossline ([0] is ax and [1] i ay)
    *      int _deltaTimeChangeVelocity : Time during which the line will keep tis acceleration
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

    /// <summary>
    /// Manage the behaviour of the CrossLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class CrossLineBehaviour : ScenarioDisplayBehaviour
    {
        //General variables of a MovingLine
        private CrossLineStruct structScenari;
        private ScenarioCrossLine scenario;

        //Variables of an Hitting Line
        private bool _hitted;
        private int _fixPosHitValue = 1; // if fix Pos hit == true we fix the value to 0 in order to have an acceleration null
        public float DeltaHit { get  {return structScenari.DeltaHit ; } }
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

        protected override void Awake()
        {
            base.Awake();
            //Initialisation of the time
            _startTimeScenario = Time.time;
            _tTime = Time.time - _startTimeScenario;

            objectVelocity = new Vector3(35, 35, 0);
        }

        void FixedUpdate()
        {
            _tTime = Time.time - _startTimeScenario;
            ObjectInCameraRange();
            MoveObject(_fixPosHitValue * objectVelocity);
            SetHit();
        }

        public override void Init(IStructScenario structScenari)
        {
            this.structScenari = (CrossLineStruct)structScenari;
        }
        public void Init(ScenarioCrossLine scenario)
        {
                this.scenario = scenario;
        }


        /// <summary>
        /// Display when the player has to hit the object
        /// </summary>
        public void SetHit()
        {
            //change the color of the line if the player have to hit
            bool canHit = (_tTime > TimeBeforeHit && (_tTime - TimeBeforeHit) < DeltaHit);
            GameObject VertLineObject = GameObject.Find(this.gameObject.transform.GetChild(0).name);
            GameObject HorizLineObject = GameObject.Find(this.gameObject.transform.GetChild(1).name);

            if (canHit && _hitted == false)
            {
                VertLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                HorizLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                if (FixPosHit == true)
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


    }
}
