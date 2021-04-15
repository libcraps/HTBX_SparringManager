using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

/// <summary>
/// Namesace relative to C# objects that help to manage the SimpleHit scenario
/// </summary>
namespace SparringManager.Scenarios.SimpleHit
{
    /// <summary>
    /// Manage the scenario HitLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class SimpleHitController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Scenario

        public override float consigne { get { return 0; } }

        #endregion

        //---------- METHODS ----------
        protected override void Awake()
        {
            base.Awake();
            //INITIALISATION OF VARIABLES 
        }
        //General Methods
        protected override void Start()
        {
            GetDevices();
            //Initialisation of the time and the acceleration
            tTime = Time.time - startTimeScenario;
            previousTime = tTime;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData
        }
        protected override void OnDestroy()
        {
            dataManagerComponent.DataBase.Add(dataSessionPlayer.DataTable);
            dataManagerComponent.EndScenarioForData = true;

            GetComponentInParent<SessionManager>().EndScenario = true;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        //Method for an hitting object
        protected override void OnEnable()
        {
            ImpactManager.onInteractPoint += SetImpactPosition;
            
        }
        protected override void OnDisable()
        {
            ImpactManager.onInteractPoint -= SetImpactPosition;
            
        }

        /// <summary>
        /// Insantiate an impact on the bag when it is hitted.
        /// </summary>
        /// <remarks>It is called by the event ImpactManager.onInteractPoint</remarks>
        /// <param name="position2d_">Position of the hit</param>
        public void SetImpactPosition(Vector2 position2d_)
        {
            Vector3 pos3d_ = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z + 20f);
            Instantiate(_prefabScenarioComposant, pos3d_, Quaternion.identity, this.gameObject.transform);
            scenarioBehaviour.onHit();
        }
    }
}
