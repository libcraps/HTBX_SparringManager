using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Scenarios
{

    public abstract class ScenarioBehaviourBase : MonoBehaviour
    {
        protected GeneriqueScenarioStruct structScenari;
        protected ScenarioControllerBase scenarioController;
        protected Scenario scenario;

        /// <summary>
        /// Dictionary that contains every scenario objects
        /// </summary>
        protected Dictionary<string, GameObject> dictGameObjects;


        public virtual void Init(GeneriqueScenarioStruct scenarioStruct)
        {
            scenarioController = GetComponentInParent<ScenarioControllerBase>();
            scenario = scenarioController.scenario;

            //initialisation
            this.structScenari = scenarioStruct;

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