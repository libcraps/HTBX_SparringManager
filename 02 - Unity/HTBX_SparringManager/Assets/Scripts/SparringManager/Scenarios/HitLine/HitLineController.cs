using SparringManager.Data;
using SparringManager.Device;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Namespace relative to the scenario HitLine
/// </summary>
namespace SparringManager.Scenarios.HitLine
{
    /// <summary>
    /// Manage the scenario HitLine.
    /// </summary>
    public class HitLineController : LineScenarioController
    {
        #region Attributs
        //----------- ATTRIBUTS ---------------------- 

        //Scenario
        private new HitLineBehaviour scenarioBehaviour { get 
            { 
                return (HitLineBehaviour)_scenarioBehaviour; 
            }
        }
        public override float consigne { get { return scenario.PosToAngle(rangeSize, base._scenarioBehaviour.transform.localPosition.x); } }

        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
        #region Unity Methods
        protected override void Start()
        {
            base.Start();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); //StockData and time update
            
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

        #endregion

    }
}