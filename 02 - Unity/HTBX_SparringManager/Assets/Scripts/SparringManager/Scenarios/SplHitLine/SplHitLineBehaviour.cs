using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
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

        //Specific variables of SplHitLine
        private GameObject _lineToHit;
        private int _scaleSide; //-1 ou 1

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
            SetLineToHit();
            _initScale = _lineToHit.transform.localScale;
            _initScale.x = _initScale.x * _scaleSide;
            _lineToHit.transform.localScale = _initScale;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //time update
            
            //RandomizeObjectMovement(structScenari.AccelerationMax, structScenari.DeltaTimeMin, structScenari.DeltaTimeMax);
            MoveObject(fixPosHitValue * objectVelocity);
            SetHit(_lineToHit);
        }

        protected override void SetHit(GameObject LineObject)
        {
            Vector3 newScale;
            Vector3 linePos3d; //Because the scale over move the object

            newScale.x = LineObject.transform.localScale.x;
            newScale.y = LineObject.transform.localScale.y;
            newScale.z = LineObject.transform.localScale.z;
            
            linePos3d.x = LineObject.transform.localPosition.x;
            linePos3d.y = LineObject.transform.localPosition.y;
            linePos3d.z = LineObject.transform.localPosition.z;

            if (TimeToHit && hitted == false)
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

                if (randomScaleSide == 0)
                {
                    _scaleSide = -1;
                }
                else
                {
                    _scaleSide = 1;
                }
            }
        }

    }
}
