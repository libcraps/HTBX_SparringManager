using SparringManager.DataManager;
using SparringManager.Device;
using System.Collections;
using System;
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

        public override float consigne { get { return 0f; } }

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
            base.Start();
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


    }
}
