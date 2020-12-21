﻿using SparringManager;
using UnityEngine;

namespace SparringManager.SimpleLine 
{
    public class SimpleLineController : MonoBehaviour
    {
        public StructScenarios _controllerStruct;

        [SerializeField]
        private GameObject _scenarioPrefab;

        void Start()
        {
            GameObject _Session = GameObject.Find(this.gameObject.transform.parent.name);
            SessionManager session = _Session.GetComponent<SessionManager>();

            _controllerStruct = session.InstantiateScenarioStruct();
            float _timer = _controllerStruct._timerScenario;

            session.DisplayDataScenari(_controllerStruct);

            Debug.Log(this.gameObject.name + " timer " + _timer);
            
            Vector3 _pos3d;
            
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            Destroy(Instantiate(_scenarioPrefab, _pos3d, Quaternion.identity, this.gameObject.transform), _timer);
        }
        void OnDestroy()
        {
            Debug.Log(this.gameObject.name + " has been destroyed");
        }
    }   
}
