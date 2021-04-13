using UnityEngine;
using System.Collections;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Manage the behaviour of the SplHitLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class SplHitLineBehaviour : ScenarioDisplayBehaviour
    {
        [SerializeField]
        public static int scaleMaxValue = 45;
        [SerializeField]
        private float _scaleSpeed = 2;

        //Specific variables of SplHitLine
        private GameObject _lineToHit;
        private int _scaleSide; //-1 ou 1
        private int _oldScaleSide;
        // -> other usefull variables
        private Vector3 _initScale;

        private GameObject _barUp;
        private GameObject _barDown;

        protected override void Awake()
        {
            base.Awake();
            _barUp = GameObject.Find(this.gameObject.transform.GetChild(0).name);
            _barDown = GameObject.Find(this.gameObject.transform.GetChild(1).name);
        }


        protected override void Start()
        {
            base.Start();
            //Initialisation of the scale
            SetLineToHit();
            _oldScaleSide = _scaleSide;
            _initScale = _lineToHit.transform.localScale;

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //time update
            
            //RandomizeObjectMovement(structScenari.AccelerationMax, structScenari.DeltaTimeMin, structScenari.DeltaTimeMax);
            MoveObject(fixPosHitValue * objectVelocity);
            SetHit(_lineToHit);
        }

        /// <summary>
        /// Show the hit on the displayObject
        /// </summary>
        /// <param name="DisplayObject">Object that show the hit</param>
        protected override void DisplayHit(GameObject DisplayObject)
        {
            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.red;

            if (DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine != null)
            {
                StopCoroutine(DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine);
            }

            DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine = DisplayObject.GetComponentInChildren<TargetMovingBar>().ScaleLine(_scaleSide, _scaleSpeed);
            StartCoroutine(DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine);
            Debug.Log(DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine);
        }

        /// <summary>
        /// Unshow the hit on the display object
        /// </summary>
        /// <param name="DisplayObject"></param>
        protected override void UndisplayHit(GameObject DisplayObject)
        {
            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.white;

            if (DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine != null)
            {
                StopCoroutine(DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine);
            }

            DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine = DisplayObject.GetComponentInChildren<TargetMovingBar>().UnScaleLine(_oldScaleSide, _scaleSpeed);
            StartCoroutine(DisplayObject.GetComponentInChildren<TargetMovingBar>().currentCoroutine);
        }

        // ---> Specific method of the splHitLine scenario
        /// <summary>
        /// Methode that defines which part of the line the player will have to hit and in which direction it will scale
        /// </summary>
        private void SetLineToHit()
        {
            int randomLine = Random.Range(0, 2);
            int randomScaleSide = Random.Range(0, 2);

            _oldScaleSide = _scaleSide;

            if (randomLine == 0)
                _lineToHit = _barUp;
            else
                _lineToHit = _barDown;

            if (randomScaleSide == 0)
                _scaleSide = -1;
            else
                _scaleSide = 1;

        }

        public override void HitManager()
        {
            base.HitManager();
            SetLineToHit();
        }


    }
}
