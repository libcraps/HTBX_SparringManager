using System.Collections;
using System.Collections.Generic;
using SparringManager.Scenarios;
using UnityEngine;


/// <summary>
/// Namespace relative to gymnase application
/// </summary>
namespace SparringManager.Device
{
    /// <summary>
    /// Class that manage a basic projection of an Hitbox session's on the gymnase connecté
    /// </summary>
    /// <remarks>It needs one GymnaseBasicProjection per Player</remarks>
    public class GymnaseBasicProjection : DeviceBehaviour
    {
        private GameObject _playerZone;
        private GameObject _vectorBagPlayer;
        private GameObject _bagZone;
        private GameObject _vectorConsigne;

        public GameObject player { get { return playerSceneController.player;  } }
        private GameObject bag { get { return playerSceneController.bag; } }
        private GameObject ground { get { return playerSceneController.ground; } }

        private PlayerSceneController playerSceneController;
        private GameObject _scenarioPlayedController;


        Vector3 posPlayerCircle;
        Vector3 posBagCircle;
        Vector3 posVectorPlayer;
        Vector3 posVectorConsigne;

        Vector3 _bagDir;
        Vector3 _bagDirNormalized;
        Vector3 vectorAngle;
        float radius;

        // Start is called before the first frame update
        void Awake()
        {
            playerSceneController = GetComponentInParent<PlayerSceneController>();

            _playerZone = this.gameObject.transform.Find("ShapeGymnaseCirclePlayerZone").gameObject;
            _vectorBagPlayer = this.gameObject.transform.Find("ShapeGymnaseVectorPlayerBag").gameObject;
            _bagZone = this.gameObject.transform.Find("ShapeGymnaseCircleBagZone").gameObject;
            _vectorConsigne = this.gameObject.transform.Find("ShapeGymnaseVectorConsigne").gameObject;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameObject.FindObjectOfType<ScenarioControllerBehaviour>())
            {
                _scenarioPlayedController = GameObject.FindObjectOfType<ScenarioControllerBehaviour>().gameObject;
                Debug.Log(_scenarioPlayedController.GetComponent<ScenarioControllerBehaviour>().consigne);
                _vectorConsigne.transform.localEulerAngles = new Vector3(0, _scenarioPlayedController.GetComponent<ScenarioControllerBehaviour>().consigne, 0);

            }
            posPlayerCircle = new Vector3(player.transform.localPosition.x, ground.transform.localPosition.y, player.transform.localPosition.z);
            posBagCircle = new Vector3(bag.transform.localPosition.x, ground.transform.localPosition.y - 0.02f, bag.transform.localPosition.z);
            posVectorPlayer = new Vector3(bag.transform.localPosition.x, ground.transform.localPosition.y - 0.015f, bag.transform.localPosition.z);
            posVectorConsigne = new Vector3(bag.transform.localPosition.x, ground.transform.localPosition.y - 0.015f, bag.transform.localPosition.z);

            _bagDir = posBagCircle - posPlayerCircle;
            _bagDirNormalized = Vector3.Normalize(new Vector3(_bagDir.x, 0, _bagDir.z));
            vectorAngle = _vectorBagPlayer.transform.localEulerAngles;
            radius = _bagDir.magnitude;

            _playerZone.transform.localPosition = posPlayerCircle;
            _bagZone.transform.localPosition = posBagCircle;
            _vectorBagPlayer.transform.localPosition = posVectorPlayer;
            _vectorConsigne.transform.localPosition = posVectorConsigne;

            _bagZone.transform.localScale = new Vector3(radius, _bagZone.transform.localScale.y, radius);
            _vectorBagPlayer.transform.localScale = new Vector3(radius + 0.1f, _vectorBagPlayer.transform.localScale.y, _vectorBagPlayer.transform.localScale.z);
            _vectorConsigne.transform.localScale = new Vector3(radius + 0.1f, _vectorConsigne.transform.localScale.y, _vectorConsigne.transform.localScale.z);

            _vectorBagPlayer.transform.localEulerAngles = new Vector3(0, Vector3.SignedAngle(_bagDirNormalized, -bag.transform.right, -bag.transform.forward), 0);
        }
    }

}