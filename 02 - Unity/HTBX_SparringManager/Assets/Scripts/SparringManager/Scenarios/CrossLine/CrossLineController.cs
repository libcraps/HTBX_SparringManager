using SparringManager.Data;
using SparringManager.Device;
using SparringManager.Structures;
using UnityEngine;


/// <summary>
/// Namespace relative to the scenario CrossLine
/// </summary>
namespace SparringManager.Scenarios.CrossLine
{
    /// <summary>
    /// Manage the scenario CrossLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class CrossLineController : LineScenarioController
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        //Scenario
        private new CrossLineBehaviour scenarioBehaviour { get { return (CrossLineBehaviour)_scenarioBehaviour; } }
        public override float consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }
        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        protected override void Start()
        {
            base.Start();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion
    }
}