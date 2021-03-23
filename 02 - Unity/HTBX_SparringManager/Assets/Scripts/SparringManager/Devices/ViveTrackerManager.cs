using System.Collections;
using System.Collections.Generic;
using SparringManager.Structures;
using Valve.VR;
using UnityEngine;

namespace SparringManager.Device
{
    public class ViveTrackerManager : DeviceBehaviour
    {
        GameObject bag;
        GameObject player;

        StructViveTracker structViveTracker;

        Vector3 _bagDir;


        public float angle = 0;

        Vector3 playerPosInit;
        bool calibrate = false;

        public Vector3 playerPos
        {
            get 
            { 
                if (calibrate)
                {
                    return player.transform.position;
                }
                else
                {
                    return playerPosInit;
                }
            }
        }

        private void Awake()
        {

        }

        private void Start()
        { 

        }
        void FixedUpdate()
        {
            
            _bagDir = bag.transform.position - player.transform.position; //Computing vector between player and bag
            Vector3 _bagDirNormalized = Vector3.Normalize(new Vector3(_bagDir.x, 0, _bagDir.z)); //Getting rid of the height for the vector and normalizing
            Vector3 _playerOrientation = Vector3.Normalize(transform.up); //Normalizing player orientation

            angle = Vector3.SignedAngle(_bagDirNormalized, -bag.transform.up, bag.transform.right);
            Debug.Log(angle);
            calibratePosPlayer();
        }

        public void calibratePosPlayer()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float deltaAngle;

                Vector3 bagDirInit = bag.transform.position - player.transform.position;
                Vector3 _bagDirNormalized = Vector3.Normalize(new Vector3(bagDirInit.x, 0, bagDirInit.z));

                deltaAngle = Vector3.SignedAngle(_bagDirNormalized, -bag.transform.up, bag.transform.forward);
                bag.transform.Rotate(new Vector3(0, 0, -deltaAngle+180));

                playerPosInit = player.transform.position;
                bag.transform.position = player.transform.position;

                calibrate = true;
            }
        }
        public override void Init(IStructDevice structure, string id)
        {
            base.Init(structure, id);
            structViveTracker = (StructViveTracker)structure;
            bag = GameObject.Find("BagTracker");
            player = GameObject.Find("Player");

            bag.GetComponent<SteamVR_TrackedObject_OnlyTilt>().index = (SteamVR_TrackedObject_OnlyTilt.EIndex)structViveTracker.indexBag;
            player.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)structViveTracker.indexPlayer;
        }


    }

}