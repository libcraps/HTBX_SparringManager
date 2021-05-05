using SparringManager.Data;
using SparringManager.Device;
using SparringManager.Scenarios;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

/// <summary>
/// Namespace relative to the scenario SplHitLine
/// </summary>
namespace SparringManager.SplHitLine
{

    /// <summary>
    /// Manage the scenario SplHitLine.
    /// </summary>
    /// <inheritdoc cref="ScenarioControllerBehaviour"/>
    public class SplHitLineController : LineScenarioController
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        private new SplHitLineBehaviour scenarioBehaviour
        {
            get
            {
                return (SplHitLineBehaviour)_scenarioBehaviour;
            }
        }
        public override float consigne { get { return scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }
        #endregion

        //--------------------------    METHODS     ----------------------------------------
        // ---> General Methods
        protected override void Start()
        {
            base.Start();

        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //Stock Data
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}