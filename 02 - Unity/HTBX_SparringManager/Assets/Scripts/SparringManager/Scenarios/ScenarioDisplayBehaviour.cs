using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioDisplayBehaviour : MonoBehaviour
    {
        /*
         * Abstract class for sccenarioBehaviour component, each scenario controller will dispose this attributs and methods (public and protected) 
         * Attributs :
         *      protected int operationalArea : Arc where the hitbox is operational (from the center to +/- operationalArc/2)
         *      
         * Methods : 
         *      public virtual void Init(IStructScenario structScenarios) : Init componenets variables of the scenario
         */

        #region Attributs
        protected int operationalArea;
        #endregion

        #region Methods
        public virtual void Init(IStructScenario structScenarios)
        {

        }

        protected abstract void ObjectInCameraRange(); //function to say what to do if the Object get out of the camera range

        protected virtual void Awake() 
        {
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
        }
        #endregion
    }

    public class Scenario1DLineDisplay : ScenarioDisplayBehaviour
    {
        /*
         * Intermediate clas that allows us to control the behaviour of a 1D line
         * Attributs : 
         *      protected float _lineVelocity : Velocity of the Line
         *      protected int _deltaTimeChangeVelocity : For random movement, it is the time that says if the velocity change 
         *      
         * Methods :
         *      protected override void ObjectInCameraRange(): Keep the line in the range of the camera
         *      
         */
        #region Attributs
        protected float _lineVelocity;
        protected int _deltaTimeChangeVelocity = 0;
        public float LineVelocity
        {
            get
            {
                return _lineVelocity;
            }
            set
            {
                _lineVelocity = value;
            }
        }
        public int DeltaTimeChangeVelocity
        {
            get
            {
                return _deltaTimeChangeVelocity;
            }
            set
            {
                _deltaTimeChangeVelocity = value;
            }
        }
        #endregion

        #region Methods
        protected override void ObjectInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            //-----------
            GameObject _SimpleLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject _Camera = _SimpleLineController.transform.GetComponentInParent<DeviceManager>().RenderCamera;
            Camera renderCamera = _Camera.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;
            //------------- a enlever

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
                    _lineVelocity = -_lineVelocity;
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
                    _lineVelocity = -_lineVelocity;
                }
            }

            this.gameObject.transform.localPosition = linePos3d;
        }
        #endregion
    }
}