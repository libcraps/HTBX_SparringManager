using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.HitLine
{
    /* Class nof the CrossLine Scenario
     * 
     *  Summary :
     *  This Scenario represents a line that can move lateraly and set a hit for the player
     *  This class animate the line
     *  
     *  Importants Attributs :
     *      scenariocontroller scenariocontrollercomponent : It is the component ScenarioController of the prefab object, it allows us to stock specific parameters of the scenario (acceleration, delta hit, etc...) -> it is in the structure controllerstruct
     *      StructScenarios controllerStruct : It is the structure that contains the StructScenarios scenarios[i] (in this structure we can find the structure hitLineStruct that contains the structure HitLineStruct)
     *      HitLineStruct hitLineControllerStruct : It is the structure that contain ONLY the HitLineScenario's parameters
     *      
     *  Methods :
     *  void Start() :
     *  void onDestroy() :
     *  void FixedUpdate() :
     *  void MoveLine() :
     *  void RandomizeLineMovement() :
     *  Void LineInCameraRange() :
     */
    public class HitLineBehaviour : MonoBehaviour
    {

        private Rigidbody _lineRigidComponent;
        private float _lineAcceleration;

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

        public void SetHit(float tTime, float timeBeforeHit, float deltaHit, bool hitted)
        {
            //change the color of the line if the player have to hit
            bool canHit = (tTime > timeBeforeHit && (tTime - timeBeforeHit) < deltaHit);

            if (canHit && hitted == false) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }

        void LineInCameraRange()
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 linePos3d;
            Vector3 renderCameraPos3d;

            GameObject _HitLineController = GameObject.Find(this.gameObject.transform.parent.name);
            GameObject gameObject = GameObject.Find(_HitLineController.gameObject.transform.parent.name);
            Camera renderCamera = gameObject.GetComponent<Camera>();
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
            Debug.Log(this.gameObject.name + "has been destroyed");
        }
    }
}
