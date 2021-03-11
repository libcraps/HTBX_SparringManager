using System.Collections;
using System.Collections.Generic;
using SparringManager.DataManager;
using SparringManager.Device;
using UnityEngine;

namespace SparringManager.Scenarios
{
    public abstract class ScenarioControllerBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _prefabScenarioComposant;
        public virtual GameObject PrefabScenarioComposant
        {
            get
            {
                return _prefabScenarioComposant;
            }
            set
            {
                _prefabScenarioComposant = value;
            }
        }
        //Scenario
        //TOCHECK

        //Data
        protected DataSessionPlayer dataSessionPlayer;
        protected DataController dataManagerComponent;

        //Devices
        protected Movuino[] movuino;
        protected Polar polar;
        protected ViveTrackerManager viveTrackerManager;
        protected int NbMovuino;
        protected GameObject cameraObject;

        //time
        protected float previousTime;
        protected float tTime;
        protected float reactTime;

        protected virtual void Start()
        {
            Debug.Log("------------" + " ScenarioControllerBehaviour Start" + "---------------");
        }
        protected virtual void FixedUpdate()
        {
            Debug.Log("------------" + " ScenarioControllerBehaviour Fixed" + "---------------");
        }
        public virtual void Init(StructScenarios structScenarios)
        {
        }
        protected virtual void GetDevices()
        {
            //Search other devices
            //movuino Part
            int NbMovuino = this.gameObject.GetComponentInParent<DeviceManager>().NbMovuino;
            movuino = new Movuino[NbMovuino]; //TODO know how many movuino we have
            for (int i = 0; i < NbMovuino; i++)
            {
                movuino[i] = GameObject.FindGameObjectsWithTag("Movuino")[i].GetComponent<Movuino>();
            }

            //Polar part
            polar = GameObject.FindGameObjectWithTag("Polar").GetComponent<Polar>();
            //ViveTrackerPart
            viveTrackerManager = GameObject.Find("ViveTrackerManager(Clone)").GetComponent<ViveTrackerManager>();
        }

        protected virtual void StockData()
        {

        }

    }
}

