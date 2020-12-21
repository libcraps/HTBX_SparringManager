﻿using UnityEngine;

namespace SparringManager.SimpleHit
{
    public class SimpleHitController : MonoBehaviour
    {
        public StructScenarios _simpleHitStruct;

        private void Start()
        {
            GameObject _Session = GameObject.Find(this.gameObject.transform.parent.name);
            SessionManager session = _Session.GetComponent<SessionManager>();

            _simpleHitStruct = session.InstantiateScenarioStruct();

            session.DisplayDataScenari(_simpleHitStruct);
        }

        private void OnEnable()
        {
            ImpactManager.onInteractPoint += SetImpactPosition;
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= SetImpactPosition;
        }

        public void SetImpactPosition(Vector2 position2d_)
        {
            Vector3 pos3d_ = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z + 20f);
            Instantiate(_simpleHitStruct._hitPrefab, pos3d_, Quaternion.identity, this.gameObject.transform);
        }
    }
}
