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
    public class CrossLine : MonoBehaviour
    {

        //Usefull parameters of the scenario, they are in the crossLineStructure
        private int _deltaTimeMax;
        private int _deltaTimeMin;
        private float _deltaHit;
        private float _timeBeforeHit;
        private float _previousTime;
        private float _tTime;
        private float _deltaTime;
        private int _accelerationMax;

        private float _startScenario;
        private float[] _lineAcceleration;
        private System.Random randomTime = new System.Random();
        private System.Random randomAcceleration = new System.Random();
        private Rigidbody lineRigidComponent;
        private ScenarioController scenarioControllerComponent;
        private StructScenarios controllerStruct;

        //list of the data that we will export
        public static List<float> mouvementConsign;
        public static List<float> timeListScenario;

        private CrossLineStruct crossLineControllerStruct;
        public static bool _hitted = false;

        void Start()
        {
            //Initialize the scene
            lineRigidComponent = GetComponent<Rigidbody>(); //this component allows us to move the line

            scenarioControllerComponent = GetComponent<ScenarioController>();
            controllerStruct = scenarioControllerComponent.ControllerStruct;
            crossLineControllerStruct = controllerStruct.CrossLineStruct;

            mouvementConsign = new List<float>();
            timeListScenario = new List<float>();
            _lineAcceleration = new float[2];

            //Initialisation of the parameters
            float _timer = controllerStruct.TimerScenario;
            _accelerationMax = crossLineControllerStruct.AccelerationMax;
            _deltaTimeMax = crossLineControllerStruct.DeltaTimeMax;
            _deltaTimeMin = crossLineControllerStruct.DeltaTimeMin;
            _deltaHit = crossLineControllerStruct.DeltaHit;
            _timeBeforeHit = crossLineControllerStruct.TimeBeforeHit;

            _startScenario = Time.time;

            Debug.Log(this.gameObject.name + " timer " + _timer);

            //Initialisation of the time and the acceleration
            _tTime = Time.time - _startScenario;
            _previousTime = _tTime;
            _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
            _lineAcceleration[0] = randomAcceleration.Next(-_accelerationMax, _accelerationMax);
            _lineAcceleration[1] = randomAcceleration.Next(-_accelerationMax, _accelerationMax);

            Debug.Log("Deta T : " + _deltaTime);
        }

        void FixedUpdate()
        {
            //Update the "situation" of the line
            _tTime = Time.time - _startScenario;
            SetHit(_tTime);
            GetConsigne(_tTime, this.gameObject.transform.position.x);
            RandomizeLineMovement(_tTime);
            MoveLine(_lineAcceleration[0], _lineAcceleration[1]);
            LineInCameraRange();
        }

        void MoveLine(float lineHorizontalAcceleration, float lineVerticalAccelearation)
        {
            //_lineRigidComponent.AddForce(new Vector3 (lineHorizontalAcceleration, 0, 0), ForceMode.Acceleration);
            lineRigidComponent.velocity = new Vector3 (lineHorizontalAcceleration, lineVerticalAccelearation, 0);
        }

        private void GetConsigne(float time, float pos)
        {
            mouvementConsign.Add(pos);
            timeListScenario.Add(time);
        }
        void SetHit(float _tTime)
        {
            //change the color of the line if the player have to hit
            bool canHit = (_tTime > _timeBeforeHit && (_tTime - _timeBeforeHit) < _deltaHit);
            GameObject VertLineObject = GameObject.Find(this.gameObject.transform.GetChild(0).name);
            GameObject HorizLineObject = GameObject.Find(this.gameObject.transform.GetChild(1).name);

            if (canHit && CrossLineController._hitted == false) //TODO revoir orga avec adrien car comment c'est fait => d'aller chercher dans classe parente
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
        void RandomizeLineMovement(float tTime)
        {
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - _previousTime) > _deltaTime)
            {
                _lineAcceleration[0] = randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _lineAcceleration[1] = randomAcceleration.Next(-_accelerationMax, _accelerationMax);
                _previousTime = tTime;
                _deltaTime = randomTime.Next(_deltaTimeMin, _deltaTimeMax);
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
