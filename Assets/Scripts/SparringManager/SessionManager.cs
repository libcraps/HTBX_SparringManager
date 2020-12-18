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
        private GameObject[] scenarios;

        [SerializeField]
        public float _timer;

        //[SerializeField]
        //private StructScenarios[] scenar;

        void Start()
        {
            float _tTime = Time.time;
            for (int i = 0; i < scenarios.Length; i++)
            {
                Debug.Log("SessionManager timer " + _timer);
                Destroy(Instantiate(scenarios[0], this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform), _timer);
            }
        }
    }
}
