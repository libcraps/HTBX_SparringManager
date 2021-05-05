using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{
    /// <summary>
    /// Abstract class for a scenariobehaviour
    /// </summary>
    /// <remarks>Globally it contains global information</remarks>
    public abstract class ScenarioBehaviourBase : MonoBehaviour
    {
        protected ScenarioControllerBase scenarioController;
        protected Scenario scenario;

        /// <summary>
        /// Dictionary that contains every scenario objects
        /// </summary>
        protected Dictionary<string, GameObject> dictGameObjects;

        /// <summary>
        /// Part of the bag that is operational
        /// </summary>
        protected int operationalArea;

        public virtual void Init()
        {
            scenarioController = GetComponentInParent<ScenarioControllerBase>();
            operationalArea = scenarioController.sessionManagerComponent.OperationalArea;
            scenario = scenarioController.scenario;

            //Get every objects of the scenario
            dictGameObjects = new Dictionary<string, GameObject>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject go = gameObject.transform.GetChild(i).gameObject;
                dictGameObjects[go.name] = go;
            }
        }

        /// <summary>
        /// Get the hit of the player
        /// </summary>
        /// <param name="position2d_">Position of the hit</param>
        protected abstract void GetHit(Vector2 position2d_);

        #region Usefull methods
        /// <summary>
        /// Move object by changing his velocity
        /// </summary>
        /// <param name="objectVelocity"></param>
        protected virtual void MoveObject(GameObject obj, Vector3 objectVelocity)
        {
            obj.GetComponent<Rigidbody>().velocity = objectVelocity;
        }

        #endregion
    }

}