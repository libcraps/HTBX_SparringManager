using UnityEngine;

namespace SparringManager.Scenarios.CrossLine
{

    /// <summary>
    /// Manage the behaviour of the CrossLine.
    /// </summary>
    /// <remarks>Essentialy it moves the line, instantiates the hit and it makes sure that the line stays in the range of the camera</remarks>
    /// <inheritdoc cref="LineScenarioBehaviour"/>
    public class CrossLineBehaviour : LineScenarioBehaviour
    {
        //General variables of a MovingLine
        GameObject VertLineObject;
        GameObject HorizLineObject;

        protected override void Start()
        {
            base.Start();

            SetObjectToHit();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveObject(this.gameObject, fixPosHitValue * _objectVelocity);
            HitManager(null);
        }

        /// <summary>
        /// Display when the player has to hit the object
        /// </summary>
        protected override void HitManager(GameObject lineObject)
        {

            if (timeToHit && hitted == false)
            {
                SetHit(VertLineObject);
                SetHit(HorizLineObject);
            }
            else if (tTime >= _timeBeforeHit && hitted == false)
            {
                UnsetHit(VertLineObject);
                UnsetHit(HorizLineObject);
                MissedTarget();
            }
            else
            {
                UnsetHit(VertLineObject);
                UnsetHit(HorizLineObject);
            }
        }

        protected override void SetObjectToHit()
        {
            _objectToHit = this.gameObject;
        }

        public override void Init()
        {
            base.Init();
            VertLineObject = dictGameObjects["VerticalLine"];
            HorizLineObject = dictGameObjects["HorizontalLine"];
        }

    }
}
