using UnityEngine;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Abstract class for ScenarioBehaviour component, each scenario controller will dispose this attributs and methods (public and protected) 
    /// </summary>
    public abstract class ScenarioDisplayBehaviour : MonoBehaviour
    {
        #region Attributs
        protected GeneriqueScenarioStruct structScenari;
        protected Scenario scenario;

        /// <summary>
        /// Part of the bag that is operational
        /// </summary>
        protected int operationalArea;

        protected GameObject _objectToHit;
        public GameObject objectToHit
        {
            get
            {
                return _objectToHit;
            }
        }

        /// <summary>
        /// Velocity of this object
        /// </summary>
        public Vector3 objectVelocity;

        /// <summary>
        /// Duration before the movement of the object change
        /// </summary>
        public int DeltaTimeChangeMovement;

        /// <summary>
        /// Boolean to indicates if the target is hitted or not
        /// </summary>
        public bool hitted;

        public bool hittedChangement;
        public bool TimeToHit { get { return tTime > timeBeforeHit && (tTime - timeBeforeHit) < deltaHit; } }

        public float timeBeforeHit;

        public int deltaHit = 2;

        float reactTime;

        /// <summary>
        /// Boolean to indicate if the line continue to move when the hit is setted 
        /// </summary>
        protected virtual bool FixPosHit { get { return structScenari.FixPosHit; } }
        private int _fixPosHitValue;
        public int fixPosHitValue
        {
            get
            {
                if (TimeToHit && hitted == false && FixPosHit == true) // warning if hitLine controller == instantiated 2 times -> problem need to be solved
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

        protected float previousTime;
        protected float tTime;
        protected float startTimeScenario;
        /// <summary>
        /// Range of the RenderCamera of the PlayerScene of this object
        /// </summary>
        public float rangeSize;

        /// <summary>
        /// RenderCamera of the PlayerPrefab of this object
        /// </summary>
        public GameObject renderCamera;
        #endregion

        #region Methods


        #region Unity Methods
        protected virtual void Awake()
        {
            scenario = this.gameObject.GetComponentInParent<ScenarioController>().Scenario;
            startTimeScenario = this.gameObject.GetComponentInParent<ScenarioController>().Scenario.startTimeScenario;
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            rangeSize = this.gameObject.GetComponentInParent<ScenarioController>().RangeSize;
            renderCamera = this.gameObject.GetComponentInParent<ScenarioController>().RenderCameraObject;

            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            DeltaTimeChangeMovement = 1;
            objectVelocity = new Vector3(scenario.speed, 0, 0);
            timeBeforeHit = tTime + 1/(1+scenario.rythme)*100;

            hittedChangement = false;
        }

        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {
            tTime = Time.time - startTimeScenario;
            ObjectInCameraRange();
            hitted = false;
        }



        protected virtual void OnDestroy()
        {
            Debug.Log(this.gameObject.name + "has been destroyed");
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
                if (linePos3d.x > (renderCameraPos3d.x + area))
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
                this.gameObject.transform.localPosition = linePos3d;
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

            
        }

        /// <summary>
        /// Move this object by changing his velocity
        /// </summary>
        /// <param name="objectVelocity"></param>
        protected virtual void MoveObject(Vector3 objectVelocity)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = objectVelocity;
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
            if ((tTime - previousTime) > DeltaTimeChangeMovement)
            {

                int deltaTimeMin = deltaTimeAverage - deltaTimeAmplitude;
                int deltaTimeMax = deltaTimeAverage + deltaTimeAmplitude;

                int speedMax = speedAverage + speedAmplitude;

                DeltaTimeChangeMovement = Random.Range(deltaTimeMin, deltaTimeMax);
                objectVelocity.x = Random.Range(-speedMax, speedMax);
                objectVelocity.y = Random.Range(-speedMax, speedMax);

                previousTime = tTime;
            }
        }

        #region Hitting Methods

        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
            targetHittedEvent += TargetTouched;
            setHitEvent += DisplayHit;
            unsetHitEvent += UndisplayHit;
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
            targetHittedEvent -= TargetTouched;
            setHitEvent -= DisplayHit;
            unsetHitEvent -= UndisplayHit;
        }

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

        public void SetHit(GameObject DisplayObject)
        {
            if (setHitEvent != null)
                setHitEvent(DisplayObject);
        }

        public void TargetHitted()
        {
            if (targetHittedEvent != null)
                targetHittedEvent();
        }

        public void UnsetHit(GameObject DisplayObject)
        {
            if (unsetHitEvent != null)
                unsetHitEvent(DisplayObject);
        }

        public void MissedTarget()
        {
            if (missedTargetEvent != null)
                missedTargetEvent();
        }

        protected virtual void SetObjectToHit()
        {

        }

        /// <summary>
        /// Manage the display of an hitting moment
        /// </summary>
        /// <param name="DisplayObject">Object that show/Unshow the hit</param>
        protected virtual void HitManager(GameObject DisplayObject)
        {
            if (TimeToHit && hitted == false)
            {
                SetHit(DisplayObject);
            }
            else if (tTime >= timeBeforeHit && hitted == false)
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
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, renderCamera.transform.position.z - 10);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 1000);

            if (rayOnTarget && TimeToHit && hitted == false && hit.collider.gameObject == objectToHit)
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
        /// Manage actions when an hit i recieved
        /// </summary>
        public virtual void TargetTouched()
        {
            hitted = true;
            timeBeforeHit = tTime + 1 / (1 + scenario.rythme) * 100;
        }

        public virtual void TargetMissed()
        {
            timeBeforeHit = tTime + 1 / (1 + scenario.rythme) * 100;
        }
        #endregion
        #endregion
    }

}