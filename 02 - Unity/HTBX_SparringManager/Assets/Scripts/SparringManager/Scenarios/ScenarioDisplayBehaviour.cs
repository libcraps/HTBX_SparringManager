using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Abstract class for sccenarioBehaviour component, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
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

        public Vector3 objectVelocity = new Vector3(0,0,0);
        public int DeltaTimeChangeMovement;
        public float rangeSize;
        public GameObject renderCamera;
        #endregion

        #region Methods

        /// <summary>
        /// Initialize parameters of the scenario.
        /// </summary>
        /// <remarks>It is called after his instantiation.</remarks>
        /// <param name="structScenarios">Structure that parameterize different settings of a scenario</param>
        public virtual void Init(IStructScenario structScenarios)
        {

        }

        /// <summary>
        /// Function to say what to do if the Object get out of the camera range.
        /// </summary>
        protected virtual void ObjectInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;


            renderCameraPos3d.x = renderCamera.transform.localPosition.x;
            renderCameraPos3d.y = renderCamera.transform.localPosition.y;
            renderCameraPos3d.z = renderCamera.transform.localPosition.z;

            linePos3d.x = this.gameObject.transform.localPosition.x;
            linePos3d.y = this.gameObject.transform.localPosition.y;
            linePos3d.z = this.gameObject.transform.localPosition.z;

            float area = operationalArea / (float)360.0 * rangeSize;

            if ((int)area == rangeSize)
            {
                //Instruction whether the line gets out of the render camera range
                if (linePos3d.x > renderCameraPos3d.x + area)
                {
                    linePos3d.x -= 2 * area;
                }
                else if (linePos3d.x < renderCameraPos3d.x - area)
                {
                    linePos3d.x += 2 * area;
                }

                //Instruction whether the line gets out of the render camera range
                if (linePos3d.y > renderCameraPos3d.y + area)
                {
                    linePos3d.y -= 2 * area;
                }
                else if (linePos3d.y < renderCameraPos3d.y - area)
                {
                    linePos3d.y += 2 * area;
                }
            }
            else
            {
                //Instruction whether the line gets out of the render camera range
                if ((linePos3d.x > renderCameraPos3d.x + area) || (linePos3d.x < renderCameraPos3d.x - area))
                {
                    objectVelocity.x = -objectVelocity.x;
                }

                //Instruction whether the line gets out of the render camera range
                if ((linePos3d.y > renderCameraPos3d.x + area) || (linePos3d.y < renderCameraPos3d.y - area))
                {
                    objectVelocity.y = -objectVelocity.y;
                }

            }


            this.gameObject.transform.localPosition = linePos3d;
        }

        /// <summary>
        /// Move this object by changing his velocity
        /// </summary>
        /// <param name="objectVelocity"></param>
        protected virtual void MoveObject(Vector3 objectVelocity)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = objectVelocity;
        }


        protected virtual void Awake() 
        {
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            rangeSize = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().rangeSize;
            renderCamera = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().RenderCameraObject;
        }

        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
        #endregion
    }

}