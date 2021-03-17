using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioDisplayBehaviour : MonoBehaviour
    {
        protected int operationalArea;

        public virtual void Init(IStructScenario structScenarios)
        {

        }

        protected abstract void ObjectInCameraRange();

        protected virtual void Awake() 
        {
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
        }
    }

    public class Scenario1DLineDisplay : ScenarioDisplayBehaviour
    {
        protected float _lineAcceleration;
        protected int _deltaTimeChangeAcceleration = 0;
        public float LineAcceleration
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

        protected override void ObjectInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject _SimpleLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject _Camera = _SimpleLineController.transform.GetComponentInParent<DeviceManager>().RenderCamera;
            Camera renderCamera = _Camera.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            float area = operationalArea / (float)360.0 * rangeSize;

            renderCameraPos3d.x = renderCamera.transform.localPosition.x;
            renderCameraPos3d.y = renderCamera.transform.localPosition.y;
            renderCameraPos3d.z = renderCamera.transform.localPosition.z;

            linePos3d.x = this.gameObject.transform.localPosition.x;
            linePos3d.y = this.gameObject.transform.localPosition.y;
            linePos3d.z = this.gameObject.transform.localPosition.z;

            //Instruction whether the line gets out of the render camera range
            if (linePos3d.x > renderCameraPos3d.x + area)
            {
                if (operationalArea == 360)
                {
                    linePos3d.x -= 2 * area;
                }
                else
                {
                    _lineAcceleration = -_lineAcceleration;
                }
            }
            else if (linePos3d.x < renderCameraPos3d.x - area)
            {
                if (operationalArea == 360)
                {
                    linePos3d.x += 2 * rangeSize;
                }
                else
                {
                    _lineAcceleration = -_lineAcceleration;
                }
            }

            this.gameObject.transform.localPosition = linePos3d;
        }
    }
}