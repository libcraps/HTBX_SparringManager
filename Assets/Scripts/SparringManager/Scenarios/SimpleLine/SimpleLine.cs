using System.Collections;
using SparringManager;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleLine
{
    /* Class nof the CrossLine Scenario
     * 
     *  Summary :
     *  This Scenario represents a line that can move right/left.
     *  
     *  Importants Attributs :
     *      RigidBody _LineRigidComponent : GIt Gives to us acces to the RigidBody Component. It allows us to use physics fonction of unity to move the line
     *      
     *  Methods :
     *  void Start() :
     *  void onDestroy() :
     *  void FixedUpdate() :
     *  void MoveLine() :
     *  Void LineInCameraRange() :
     */
    public class SimpleLine : MonoBehaviour
    {

        private Rigidbody _lineRigidComponent;

        void Start()
        {
            _lineRigidComponent = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            LineInCameraRange();
        }

        public void MoveLine(float lineHorizontalAcceleration)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            _lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, 0, 0);
        }
        void LineInCameraRange()
        {
           /* 
            * This method keeps the line in the camera range
            */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject _SimpleLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject _Camera = GameObject.Find(_SimpleLineController.gameObject.transform.parent.name);
            Camera renderCamera = _Camera.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            renderCameraPos3d.x = renderCamera.transform.position.x;
            renderCameraPos3d.y = renderCamera.transform.position.y;
            renderCameraPos3d.z = renderCamera.transform.position.z;
            
            linePos3d.x = this.gameObject.transform.position.x;
            linePos3d.y = this.gameObject.transform.position.y;
            linePos3d.z = this.gameObject.transform.position.z;

            //Instruction whether the line get out of the render camera range
            if (linePos3d.x > renderCameraPos3d.x + rangeSize)
            {
                linePos3d.x -= 2* rangeSize;
            } 
            else if (linePos3d.x < renderCameraPos3d.x - rangeSize)
            {
                linePos3d.x += 2* rangeSize;
            }
            this.gameObject.transform.position = linePos3d;
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
    }
}
