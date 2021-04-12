using SparringManager.DataManager;
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
    public class HitLineController : ScenarioControllerBehaviour
    {
        #region Attributs
        //----------- ATTRIBUTS ---------------------- 

        //Scenario
        private new HitLineBehaviour scenarioBehaviour { get 
            { 
                return (HitLineBehaviour)base.scenarioBehaviour; 
            }
            set
            {
                base.scenarioBehaviour = value;
            }
        }
        protected override object consigne { get { return Scenario.PosToAngle(rangeSize, base.scenarioBehaviour.transform.localPosition.x); } }

        #endregion

        #region Methods
        //------------ METHODS -------------------
        //General Methods
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
            base.FixedUpdate(); //StockData and time update

            //Behaviour Management
            
            //RandomizeParametersLineMovement(scenario.accelerationMax, scenario.deltaTimeMin, scenario.deltaTimeMax);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

    }
}