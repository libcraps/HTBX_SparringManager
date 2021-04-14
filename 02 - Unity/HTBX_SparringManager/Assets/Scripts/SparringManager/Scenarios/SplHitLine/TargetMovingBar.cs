using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{

    public class TargetMovingBar : MonoBehaviour
    {
        public bool activated;

        public int scaleSide;
        // -> other usefull variables
        private Vector3 _initScale;

        private void Awake()
        {
            _initScale = this.gameObject.transform.localScale;
        }

        public void ScaleLine(float scaleSpeed)
        {
            Vector3 newScale;
            Vector3 linePos3d; //Because the scale over move the object

            newScale.x = transform.localScale.x;
            newScale.y = transform.localScale.y;
            newScale.z = transform.localScale.z;

            linePos3d.x = transform.localPosition.x;
            linePos3d.y = transform.localPosition.y;
            linePos3d.z = transform.localPosition.z;

            if (Mathf.Abs(transform.localScale.x) < Mathf.Abs(SplHitLineBehaviour.scaleMaxValue)) 
            { 
                newScale.x += scaleSide * scaleSpeed;
                linePos3d.x += scaleSide * scaleSpeed / 2;

                transform.localScale = newScale;
                transform.localPosition = linePos3d;
            }

        }

        public void UnScaleLine(float scaleSpeed)
        {
            Vector3 newScale;
            Vector3 linePos3d; //Because the scale over move the object

            newScale.x = transform.localScale.x;
            newScale.y = transform.localScale.y;
            newScale.z = transform.localScale.z;

            linePos3d.x = transform.localPosition.x;
            linePos3d.y = transform.localPosition.y;
            linePos3d.z = transform.localPosition.z;

            if (Mathf.Abs(transform.localScale.x) > Mathf.Abs(_initScale.x))
            {
                newScale.x -= scaleSide * scaleSpeed;
                linePos3d.x -= scaleSide * scaleSpeed / 2;

                transform.localScale = newScale;
                transform.localPosition = linePos3d;

                if (Mathf.Abs(transform.localScale.x) <= Mathf.Abs(_initScale.x))
                {
                    activated = false;
                    transform.localScale *= scaleSide;
                }

            }

    }
}

}