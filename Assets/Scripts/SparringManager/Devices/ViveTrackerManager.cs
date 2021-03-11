using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.Device
{
    public class ViveTrackerManager : DeviceBehaviour
    {
        GameObject bag;
        GameObject player;

        public float angle = 0;

        private void Awake()
        {
            bag = GameObject.Find("BagTracker");
            player = GameObject.Find("Player");
        }

        private void Start()
        {

        }
        void Update()
        {
            Vector3 _bagDir = bag.transform.position - player.transform.position; //Computing vector between player and bag
            _bagDir = Vector3.Normalize(new Vector3(_bagDir.x, 0, _bagDir.z)); //Getting rid of the height for the vector and normalizing
            Vector3 _playerOrientation = Vector3.Normalize(transform.up); //Normalizing player orientation

            angle = Vector3.SignedAngle(_bagDir, -bag.transform.up, Vector3.up);
        }
    }

}