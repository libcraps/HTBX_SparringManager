using UnityEngine;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Manage the behaviour of the SplHitLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="ScenarioDisplayBehaviour"/>
    public class SplHitLineBehaviour : ScenarioDisplayBehaviour
    {
        //General variables of a MovingLin



        [SerializeField]
        private int _scaleMaxValue = 45;
        [SerializeField]
        private float _scaleSpeed = 2;

        //Specific variables of SplHitLine
        private GameObject _lineToHit;
        private int _scaleSide; //-1 ou 1
        private int _oldScaleSide;
        // -> other usefull variables
        private Vector3 _initScale;

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
            Vector3 newScale;
            Vector3 linePos3d; //Because the scale over move the object

            newScale.x = DisplayObject.transform.localScale.x;
            newScale.y = DisplayObject.transform.localScale.y;
            newScale.z = DisplayObject.transform.localScale.z;

            linePos3d.x = DisplayObject.transform.localPosition.x;
            linePos3d.y = DisplayObject.transform.localPosition.y;
            linePos3d.z = DisplayObject.transform.localPosition.z;

            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.red;

            if (Mathf.Abs(DisplayObject.transform.localScale.x) < _scaleMaxValue)
            {
                newScale.x += _oldScaleSide * _scaleSpeed;
                linePos3d.x += _oldScaleSide * _scaleSpeed / 2;
            }

            DisplayObject.transform.localScale = newScale;
            DisplayObject.transform.localPosition = linePos3d;
            
        }

        /// <summary>
        /// Unshow the hit on the display object
        /// </summary>
        /// <param name="DisplayObject"></param>
        protected override void UndisplayHit(GameObject DisplayObject)
        {
            Vector3 newScale;
            Vector3 linePos3d; //Because the scale over move the object

            newScale.x = DisplayObject.transform.localScale.x;
            newScale.y = DisplayObject.transform.localScale.y;
            newScale.z = DisplayObject.transform.localScale.z;

            linePos3d.x = DisplayObject.transform.localPosition.x;
            linePos3d.y = DisplayObject.transform.localPosition.y;
            linePos3d.z = DisplayObject.transform.localPosition.z;

            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.white;

            if (Mathf.Abs(DisplayObject.transform.localScale.x) > Mathf.Abs(_initScale.x))
            {
                newScale.x -= _scaleSpeed * _oldScaleSide;
                linePos3d.x -= _oldScaleSide * _scaleSpeed / 2;

            }

            DisplayObject.transform.localScale = newScale;
            DisplayObject.transform.localPosition = linePos3d;
        }

        // ---> Specific method of the splHitLine scenario
        /// <summary>
        /// Methode that defines which part of the line the player will have to hit and in which direction it will scale
        /// </summary>
        private void SetLineToHit()
        {
            /*
             * Methode that defines which part of the line the player will have to hit and in which direction it will scale
             * 
             */
            if (_lineToHit == null)
            {
                int randomLine = Random.Range(0, 2);
                int randomScaleSide = Random.Range(0, 2);

                _lineToHit = GameObject.Find(this.gameObject.transform.GetChild(randomLine).name);
                _oldScaleSide = _scaleSide;
                if (randomScaleSide == 0)
                {
                    _scaleSide = -1;
                }
                else
                {
                    _scaleSide = 1;
                }
            }
            _lineToHit.transform.localScale *= _scaleSide;
        }

        public override void HitManager()
        {
            base.HitManager();
            SetLineToHit();
        }

    }
}
