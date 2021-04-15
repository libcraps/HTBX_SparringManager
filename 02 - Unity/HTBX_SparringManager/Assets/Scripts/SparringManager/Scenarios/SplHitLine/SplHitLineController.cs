using SparringManager.DataManager;
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
    public class SplHitLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ----------------------
        private new SplHitLineBehaviour scenarioBehaviour
        {
            get
            {
                return (SplHitLineBehaviour)base.scenarioBehaviour;
            }
            set
            {
                base.scenarioBehaviour = value;
            }
        }
        public override float consigne { get { return Scenario.PosToAngle(rangeSize, scenarioBehaviour.transform.localPosition.x); } }
        #endregion

        //--------------------------    METHODS     ----------------------------------------
        // ---> General Methods
        protected override void Awake()
        {
            base.Awake();
            //INITIALISATION OF VARIABLES 
        }
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