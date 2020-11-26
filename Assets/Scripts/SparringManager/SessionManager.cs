using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject scenarioSimpleHit;

        void Start()
        {
            Instantiate(scenarioSimpleHit, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
        }
    }
}
