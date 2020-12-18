using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.SimpleHit
{
    [System.Serializable]
    public struct SimpleHitStruct
    {
        [SerializeField]
        public GameObject _hitPrefab;

        public SimpleHitStruct(GameObject prefab)
        {
            this._hitPrefab = prefab;
        }
    }
}
