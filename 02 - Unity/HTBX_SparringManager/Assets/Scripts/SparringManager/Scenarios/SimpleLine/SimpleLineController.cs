using SparringManager.DataManager;
using SparringManager.Device;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Namesace relative to C# objects that help to manage the SimpleLine scenario
/// </summary>
namespace SparringManager.Scenarios.SimpleLine
{
    /* Class nof the SimpleLine Scenario Controller
     * 
     *  Summary :
     *  This class manage the behaviour of the SimpleLine prefab, and the data handling of the scenario.
     *  
     *  Attributs :
     *      public ScenarioSimpleLine scenario { get; set; } : scenario object inherited from Scenario<Hitline>
     *      public SimpleLineBehaviour scenarioBehaviour : HitlineBehaviour componnent of the scenario
     *      protected override float startTimeScenario : startTime of the scenario;
     *      protected override object consigne : get the consigne of the session
     *      
     *  Methods :
     *      protected virtual void Awake() : Unity Function launch a the instance of the script     -> base.Awake()
     *      protected virtual void Start() : Unity Function launch for the firts frame              -> base.Start(), instantiation of the behaviour
     *      protected virtual void FixedUpdate() : Unity Function called at each phyical iteration  -> base.FixedUpdate(), RandomizeLineMovement() ?
     *      public virtual void Init(StructScenarios structScenarios) : Function that is called after the instantiation of the scenario controller, it initialised parameters of the scenario
     *      void RandomizeParametersLineMovement(int accelerationMax, int deltaTimeMin, int deltaTimeMax) : Randomize the movement of the cross
     *      
     */


    /// <summary>
    /// Manage the scenario SimpleLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class SimpleLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Usefull parameters of the scenario, they are in the SimpleLineStructure
        //Object that contain datas (structures)

        private new SimpleLineBehaviour scenarioBehaviour
        {
            get
            {
                return (SimpleLineBehaviour)base.scenarioBehaviour;
            }
            set
            {
                base.scenarioBehaviour = value;
            }
        }

        protected override object consigne { get { return Scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); }}
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        protected override void Awake()
        {
            base.Awake();
            hit = " ";
        }
        protected override void Start()
        {
            base.Start();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion
    }
}