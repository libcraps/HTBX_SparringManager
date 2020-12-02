using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject scenario;

        [SerializeReference]
        public float _timer;

        private float _tTime;

        void Start()
        {
            _tTime = Time.time;
            float variable = _timer; 
            Debug.Log("SessionManager timer " + _timer);
            Destroy(Instantiate(scenario, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform), _timer);
            
        }

        public float Timer()
        {
            return _timer;
        }

    }
}
