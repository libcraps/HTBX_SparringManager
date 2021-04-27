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
        private int _scaleSide; //-1 ou 1
        // -> other usefull variables
        private Vector3 _initScale;

        private GameObject _barUp;
        private GameObject _barDown;

        protected override void Awake()
        {
            base.Awake();
            _barDown = this.gameObject.transform.GetChild(1).gameObject;
            _barUp = this.gameObject.transform.GetChild(0).gameObject;

        }

        protected override void Start()
        {
            base.Start();
            SetObjectToHit();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //time update
            MoveObject(this.gameObject, fixPosHitValue * objectVelocity);


            if (_objectToHit == null || _objectToHit.GetComponent<TargetMovingBar>().activated == false)
            {
                SetObjectToHit();
            }

            HitManager(_objectToHit);
        }

        /// <summary>
        /// Show the hit on the displayObject
        /// </summary>
        /// <param name="DisplayObject">Object that show the hit</param>
        protected override void DisplayHit(GameObject DisplayObject)
        {
            TargetMovingBar movingBar = DisplayObject.GetComponent<TargetMovingBar>();

            if (movingBar.activated == true && hitted == false)
            {
                DisplayObject.GetComponent<MeshRenderer>().material.color = Color.red;
                movingBar.ScaleLine(_scaleSpeed);
            }
        }

        /// <summary>
        /// Unshow the hit on the display object
        /// </summary>
        /// <param name="DisplayObject"></param>
        protected override void UndisplayHit(GameObject DisplayObject)
        {
            TargetMovingBar movingBar = DisplayObject.GetComponent<TargetMovingBar>();

            if (movingBar.activated == true)
            {
                DisplayObject.GetComponent<MeshRenderer>().material.color = Color.white;
                movingBar.UnScaleLine(_scaleSpeed);
            }
        }

        // ---> Specific method of the splHitLine scenario
        /// <summary>
        /// Methode that defines which part of the line the player will have to hit and in which direction it will scale
        /// </summary>
        protected override void SetObjectToHit()
        {
            int randomLine = Random.Range(0, 2);
            int randomScaleSide = Random.Range(0, 2);

            int scaleSide;

            if (randomLine == 0)
            {
                _objectToHit = _barUp;
            }
            else
            {
                _objectToHit = _barDown;
            }

            if (randomScaleSide == 0)
            {
                scaleSide = -1;
            }
            else
            {
                scaleSide = 1;
            }

            _objectToHit.GetComponent<TargetMovingBar>().activated = true;
            _objectToHit.GetComponent<TargetMovingBar>().scaleSide = scaleSide;
            _objectToHit.transform.localScale *= scaleSide;
        }

        public override void TargetTouched()
        {
            base.TargetTouched();
        }


    }
}
