using System.Collections;
using System.Collections.Generic;
using System;
using SparringManager.SimpleLine;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private StructScenarios[] scenarios;

        [SerializeField]
        public float _timer;

        [SerializeField]
        private GameObject scenario;

        //[SerializeField]
        //private StructScenarios[] scenar;

        void Start()
        {
            float _tTime = Time.time;
            Debug.Log("SessionManager timer " + _timer);
            Destroy(Instantiate(scenario, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform), 5); //remplacer 5 par _timer
        }
    }
}
//Utiliser update() pour actualiser la génération de nouveaux scénarios quand un est finie