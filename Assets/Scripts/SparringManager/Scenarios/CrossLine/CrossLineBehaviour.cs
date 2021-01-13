using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.DataManager.CrossLine;
using UnityEngine;

namespace SparringManager.CrossLine
{
     /* Class nof the CrossLine Scenario
     * 
     *  Summary :
     *  This Scenario is similar to HitLin except that it represents a cross that can move right/left and upside/down
     *  This class animate the cross
     *  
     *  Importants Attributs :
     *      scenariocontroller scenariocontrollercomponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, delta hit, etc...) -> it is in the structure controllerstruct
     *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure crossLineStruct that contains the structure CrossLineStruct)
     *      CrossLineStruct crossLineControllerStruct : It is the structure that contain ONLY the CrossLineScenario's parameters
     *      
     *  Methods :
     *  void Start() :
     *  void onDestroy() :
     *  void FixedUpdate() :
     *  void MoveLine() :
     *  void GetConsigne() :
     *  void SetHit() :
     *  void RandomizeLineMovement() :
     *  Void LineInCameraRange() :
     */
    public class CrossLineBehaviour : MonoBehaviour
    {
        private Rigidbody _lineRigidComponent;

        private float _acceleration;
        void Start()
        {
            _lineRigidComponent = new Rigidbody();
            _lineRigidComponent = this.gameObject.GetComponent<Rigidbody>(); //this component allows us to move the line
        }

        void FixedUpdate()
        {
            LineInCameraRange();
        }

        public void MoveLine(float lineHorizontalAcceleration, float lineVerticalAcceleration)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (lineHorizontalAcceleration, lineVerticalAcceleration, 0);
        }

        public void SetHit(float tTime, float timeBeforeHit, float deltaHit, bool hitted)
        {
            //change the color of the line if the player have to hit
            bool canHit = (tTime > timeBeforeHit && (tTime - timeBeforeHit) < deltaHit);
            GameObject VertLineObject = GameObject.Find(this.gameObject.transform.GetChild(0).name);
            GameObject HorizLineObject = GameObject.Find(this.gameObject.transform.GetChild(1).name);

            if (canHit && hitted == false)
            {
                VertLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
                HorizLineObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                VertLineObject.GetComponent<MeshRenderer>().material.color = Color.white;
                HorizLineObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }


        
        void LineInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject cameraObject = GameObject.Find("RenderCamera_Hitbox1");
            Camera renderCamera = cameraObject.GetComponent<Camera>();
            float rangeSize = renderCamera.GetComponent<Camera>().orthographicSize;

            renderCameraPos3d.x = renderCamera.transform.position.x;
            renderCameraPos3d.y = renderCamera.transform.position.y;
            renderCameraPos3d.z = renderCamera.transform.position.z;
            
            linePos3d.x = this.gameObject.transform.position.x;
            linePos3d.y = this.gameObject.transform.position.y;
            linePos3d.z = this.gameObject.transform.position.z;

            //Instruction whether the line gets out of the render camera range
            if (linePos3d.x > renderCameraPos3d.x + rangeSize)
            {
                linePos3d.y -= 2* rangeSize;
            } 
            else if (linePos3d.x < renderCameraPos3d.x - rangeSize)
            {
                linePos3d.y += 2* rangeSize;
            }
            //Instruction whether the line gets out of the render camera range
            if (linePos3d.y > renderCameraPos3d.y + rangeSize)
            {
                linePos3d.y -= 2 * rangeSize;
            }
            else if (linePos3d.y < renderCameraPos3d.y - rangeSize)
            {
                linePos3d.y += 2 * rangeSize;
            }
            this.gameObject.transform.position = linePos3d;
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
