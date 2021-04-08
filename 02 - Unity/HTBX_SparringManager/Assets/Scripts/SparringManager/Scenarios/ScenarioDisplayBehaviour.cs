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

        /// <summary>
        /// Velocity of this object
        /// </summary>
        public Vector3 objectVelocity;

        /// <summary>
        /// Duration before the movement of the object change
        /// </summary>
        public int DeltaTimeChangeMovement;

        public bool hitted;

        public int fixPosHitValue = 1; // if fix Pos hit == true we fix the value to 0 in order to have an acceleration null

        protected float previousTime;
        protected float tTime;
        protected float startTimeScenario;
        /// <summary>
        /// Range of the RenderCamera of the PlayerScene of this object
        /// </summary>
        public float rangeSize;

        /// <summary>
        /// RenderCamera of the PlayerScene of this object
        /// </summary>
        public GameObject renderCamera;
        #endregion

        #region Methods

        #region Unity Methods
        protected virtual void Awake()
        {
            scenario = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().scenario;
            startTimeScenario = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().scenario.startTimeScenario;
            operationalArea = this.gameObject.GetComponentInParent<SessionManager>().OperationalArea;
            rangeSize = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().RangeSize;
            renderCamera = this.gameObject.GetComponentInParent<ScenarioControllerBehaviour>().RenderCameraObject;

            tTime = Time.time - startTimeScenario;
            previousTime = tTime;

            DeltaTimeChangeMovement = 1;
            objectVelocity = new Vector3(scenario.speed, 0, 0);
        }

        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {
            tTime = Time.time - startTimeScenario;
        }

        void OnDestroy()
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

        #endregion
    }

}