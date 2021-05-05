using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Abstract class for ScenarioBehaviour component, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
    /// <remarks>By default it is adapted for Line scenarios</remarks>
    public abstract class LineDisplayBehaviour : MonoBehaviour
    {
        #region Attributs
        protected GeneriqueScenarioStruct structScenari;
        protected LineScenarioController scenarioController;
        protected Scenario scenario;

        protected GameObject _objectToHit;

        /// <summary>
        /// Dictionary that contains every scenario objects
        /// </summary>
        protected Dictionary<string, GameObject> _dictGameObjects;

        /// <summary>
        /// Velocity of this object
        /// </summary>
        public Vector3 objectVelocity;

        /// <summary>
        /// Boolean to indicates if the target is hitted or not
        /// </summary>
        public bool hitted;
        public float timeBeforeHit;
        public int deltaHit = 2;
        public float reactTime;
        private int _fixPosHitValue;

        protected float previousTime;
        protected float tTime;
        protected float startTimeScenario;

        /// <summary>
        /// Part of the bag that is operational
        /// </summary>
        protected int operationalArea;
        /// <summary>
        /// Duration before the movement of the object change
        /// </summary>
        public int deltaTimeChangeMovement;
        #endregion


        #region Properties
        public Dictionary<string, GameObject> dictGameObjects { get { return _dictGameObjects; } }
        public GameObject objectToHit
        {
            get
            {
                return _objectToHit;
            }
        }
        public bool timeToHit { get { return tTime > timeBeforeHit && (tTime - timeBeforeHit) < deltaHit; } }
        /// <summary>
        /// Boolean to indicate if the line continue to move when the hit is setted 
        /// </summary>
        protected virtual bool fixPosHit { get { return structScenari.FixPosHit; } }
        public int fixPosHitValue
        {
            get
            {
                if (timeToHit && hitted == false && fixPosHit == true) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
                {
                    _fixPosHitValue = 0;
                }
                else
                {
                    _fixPosHitValue = 1;
                }
                return _fixPosHitValue;
            }

        }// if fix Pos hit == true we fix the value to 0 in order to have an acceleration null
        #endregion

        #region Methods

        #region Unity Methods
        protected virtual void Awake()
        {
            scenarioController = this.gameObject.GetComponentInParent<LineScenarioController>();
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            scenario = scenarioController.Scenario;


            tTime = Time.time - scenario.startTimeScenario;
            previousTime = tTime;

            deltaTimeChangeMovement = 1;
            objectVelocity = new Vector3(scenario.speed, 0, 0);
            timeBeforeHit = tTime + 1/(1+scenario.rythme)*100;

            //Get every objects of the scenario
            _dictGameObjects = new Dictionary<string, GameObject>();
            for (int i=0; i< gameObject.transform.childCount; i++)
            {
                GameObject go = gameObject.transform.GetChild(i).gameObject;
                _dictGameObjects[go.name] = go;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// Default : actualize time, keep object in camera range and hitted = false
        /// </summary>
        protected virtual void FixedUpdate()
        {
            tTime = Time.time - scenario.startTimeScenario;
            ObjectInCameraRange(this.gameObject);
            hitted = false;
        }

        protected virtual void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        /// <summary>
        /// Subsribe to differents event
        /// </summary>
        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
            targetHittedEvent += TargetTouched;
            missedTargetEvent += TargetMissed;
            setHitEvent += DisplayHit;
            unsetHitEvent += UndisplayHit;
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
            targetHittedEvent -= TargetTouched;
            missedTargetEvent -= TargetMissed;
            setHitEvent -= DisplayHit;
            unsetHitEvent -= UndisplayHit;
        }
        #endregion 



        /// <summary>
        /// Initialize parameters of the scenario.
        /// </summary>
        /// <remarks>It is called after his instantiation.</remarks>
        /// <param name="structScenarios">Structure that parameterize different settings of a scenario</param>
        public virtual void Init(GeneriqueScenarioStruct structScenari)
        {
            this.structScenari = structScenari;
        }

        /// <summary>
        /// Keep the object in the camera range.
        /// </summary>
        protected virtual void ObjectInCameraRange(GameObject obj)
        {
            /* 
             * This method keeps the line in the camera range
             */
            Vector3 objPos3d;
            Vector3 renderCameraPos3d;


            renderCameraPos3d.x = scenarioController.renderCameraObject.transform.localPosition.x;
            renderCameraPos3d.y = scenarioController.renderCameraObject.transform.localPosition.y;
            renderCameraPos3d.z = scenarioController.renderCameraObject.transform.localPosition.z;

            objPos3d.x = obj.transform.localPosition.x;
            objPos3d.y = obj.transform.localPosition.y;
            objPos3d.z = obj.transform.localPosition.z;

            float area = operationalArea / 360.0f * scenarioController.rangeSize;

            if ((int)area == scenarioController.rangeSize)
            {
                //Instruction whether the line gets out of the render camera range
                if (objPos3d.x > (renderCameraPos3d.x + area))
                {
                    objPos3d.x -= 2 * area;
                }
                else if (objPos3d.x < renderCameraPos3d.x - area)
                {
                    objPos3d.x += 2 * area;
                }

                //Instruction whether the line gets out of the render camera range
                if (objPos3d.y > renderCameraPos3d.y + area)
                {
                    objPos3d.y -= 2 * area;
                }
                else if (objPos3d.y < renderCameraPos3d.y - area)
                {
                    objPos3d.y += 2 * area;
                }
                obj.transform.localPosition = objPos3d;
            }
            else
            {
                //Instruction whether the line gets out of the render camera range
                if ((objPos3d.x > renderCameraPos3d.x + area) || (objPos3d.x < renderCameraPos3d.x - area))
                {
                    objectVelocity.x = -objectVelocity.x;
                }

                //Instruction whether the line gets out of the render camera range
                if ((objPos3d.y > renderCameraPos3d.x + area) || (objPos3d.y < renderCameraPos3d.y - area))
                {
                    objectVelocity.y = -objectVelocity.y;
                }

            }

            
        }

        /// <summary>
        /// Move object by changing his velocity
        /// </summary>
        /// <param name="objectVelocity"></param>
        protected virtual void MoveObject(GameObject obj, Vector3 objectVelocity)
        {
            obj.GetComponent<Rigidbody>().velocity = objectVelocity;
        }

        /// <summary>
        /// Randomize the movement of the object every deltaTime seconds
        /// </summary>
        /// <param name="speedAverage">Maximum velocity of the line</param>
        /// <param name="deltaTimeAverage">Minimum time before the ine change his velocity</param>
        /// <param name="deltaTimeAmplitude">Maximum time before the ine change his velocity</param>
        protected virtual void RandomizeObjectMovement(int speedAverage, int speedAmplitude, int deltaTimeAverage, int deltaTimeAmplitude)
        {
            
            //Randomize the movement of the line every deltaTime seconds
            if ((tTime - previousTime) > deltaTimeChangeMovement)
            {

                int deltaTimeMin = deltaTimeAverage - deltaTimeAmplitude;
                int deltaTimeMax = deltaTimeAverage + deltaTimeAmplitude;

                int speedMax = speedAverage + speedAmplitude;

                deltaTimeChangeMovement = Random.Range(deltaTimeMin, deltaTimeMax);
                objectVelocity.x = Random.Range(-speedMax, speedMax);
                objectVelocity.y = Random.Range(-speedMax, speedMax);

                previousTime = tTime;
            }
        }



        #region Hitting Methods

        public delegate void TargetHittedEvent();
        /// <summary>
        /// Event notified when the target is hitted
        /// </summary>
        public event TargetHittedEvent targetHittedEvent;

        public delegate void UnsetHitEvent(GameObject DisplayObject);
        /// <summary>
        /// Event notified when the Hit has to be removed
        /// </summary>
        public event UnsetHitEvent unsetHitEvent;

        public delegate void SetHitEvent(GameObject DisplayObject);
        /// <summary>
        /// Event notified when the Hit has to be setted
        /// </summary>
        public event SetHitEvent setHitEvent;

        public delegate void TargetMissedEvent();
        /// <summary>
        /// EVent notified when the target isn't hit on time
        /// </summary>
        public event TargetMissedEvent missedTargetEvent;

        /// <summary>
        /// Method that calls the sethit event
        /// </summary>
        /// <param name="DisplayObject">Object where the hit will be</param>
        public void SetHit(GameObject DisplayObject)
        {
            if (setHitEvent != null)
                setHitEvent(DisplayObject);
        }
        /// <summary>
        /// Method that calls the Unsethit event
        /// </summary>
        /// <param name="DisplayObject">Object where the hit was</param>
        public void UnsetHit(GameObject DisplayObject)
        {
            if (unsetHitEvent != null)
                unsetHitEvent(DisplayObject);
        }

        /// <summary>
        /// Method that calls the targetHitted event
        /// </summary>
        public void TargetHitted()
        {
            if (targetHittedEvent != null)
                targetHittedEvent();
        }

        /// <summary>
        /// Method that calls the missedTarget event
        /// </summary>
        public void MissedTarget()
        {
            if (missedTargetEvent != null)
                missedTargetEvent();
        }

        /// <summary>
        /// Indicates wich objects you have to hit
        /// </summary>
        protected virtual void SetObjectToHit()
        {

        }

        /// <summary>
        /// Manage the display of an hitting moment
        /// </summary>
        /// <param name="DisplayObject">Object that show/Unshow the hit</param>
        protected virtual void HitManager(GameObject DisplayObject)
        {
            if (timeToHit && hitted == false)
            {
                SetHit(DisplayObject);
            }
            else if (tTime >= timeBeforeHit + deltaHit && hitted == false)
            {
                UnsetHit(DisplayObject);
                MissedTarget();
            }
            else
            {
                UnsetHit(DisplayObject);
            }
        }

        /// <summary>
        /// Get the hit of the player
        /// </summary>
        /// <param name="position2d_">Position of the hit</param>
        protected virtual void GetHit(Vector2 position2d_)
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, scenarioController.renderCameraObject.transform.position.z - 10);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 1000);

            if (rayOnTarget && timeToHit && hitted == false && hit.collider.gameObject == objectToHit)
            {
                reactTime = tTime - timeBeforeHit;
                TargetHitted();

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + reactTime);
            }
        }

        /// <summary>
        /// Show the hit on the displayObject
        /// </summary>
        /// <remarks>By default we only change its color</remarks>
        /// <param name="DisplayObject">Object that show the hit</param>
        protected virtual void DisplayHit(GameObject DisplayObject)
        {
            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        /// <summary>
        /// Unshow the hit on the display object
        /// </summary>
        /// <remarks>By default we only change its color</remarks>
        /// <param name="DisplayObject"></param>
        protected virtual void UndisplayHit(GameObject DisplayObject)
        {
            DisplayObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }

        /// <summary>
        /// Manage actions when an hit is recieved
        /// </summary>
        public virtual void TargetTouched()
        {
            hitted = true;
            timeBeforeHit = tTime + 1 / (1 + scenario.rythme) * 100;
        }

        /// <summary>
        /// Manage actions when an hit isn't hitted in time
        /// </summary>
        public virtual void TargetMissed()
        {
            timeBeforeHit = tTime + 1 / (1 + scenario.rythme) * 100;
        }
        #endregion
        #endregion
    }

}