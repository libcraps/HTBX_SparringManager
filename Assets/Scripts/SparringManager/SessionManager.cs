using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject scenario;

        void Start()
        {
            Instantiate(scenario, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
        }
    }
}
