using System;
using System.Collections;
using System.Collections.Generic;
using SparringManager.Scenarios;
using UnityEngine;


namespace SparringManager.Device
{

    /// <summary>
    /// Class that manage a basic projection of an Hitbox session's with Line scenarios on the gymnase connecté
    /// </summary>
    /// <remarks>It needs one GymnaseBasicProjection per Player</remarks>
    /// <remarks>To add another projetcion you have to create it</remarks>
    public class GymnaseBasicProjection : DeviceBehaviour
    {
        [SerializeField]
        private Material glowRedMaterial;
        [SerializeField]
        private Material glowWhiteMaterial;
        [SerializeField]
        private Material glowBlueMaterial;
        [SerializeField]
        private Material glowGreenMaterial;

        private GameObject _playerZone;
        private GameObject _vectorBagPlayer;
        private GameObject _bagZone;
        private GameObject _vectorConsigne;

        private GameObject playerPrefab;

        private PlayerSceneController playerSceneController;
        private SessionManager sessionManager;
        private LineScenarioController _scenarioPlayedController 
        { 
            get 
            { 
                if (Convert.ToBoolean(sessionManager.scenarioPlayed.GetComponent<LineScenarioController>()))
                {
                    return sessionManager.scenarioPlayed.GetComponent<LineScenarioController>();
                } 
                else
                {
                    return null;
                }
            } 
        }

        private ScenarioBehaviourBase _scenarioPlayedDisplay;

        bool _isScenarioRunning;

        IEnumerator currentCoroutine;

        Vector3 posPlayerCircle;
        Vector3 posBagCircle;
        Vector3 posVectorPlayer;
        Vector3 posVectorConsigne;

        Vector3 _bagDir;
        Vector3 _bagDirNormalized;
        Vector3 vectorAngle;
        float radius;
        float consignePlayedScenario
        {
            get
            {
                if (Convert.ToBoolean(_scenarioPlayedController))
                {
                    return _scenarioPlayedController.consigne;
                }
                else
                {
                    return 0f;
                }
            }
        }

        float bpm
        {
            get
            {
                if (Convert.ToBoolean(playerSceneController.polar))
                {
                    return playerSceneController.polar.GetComponent<Polar>().oscData.bpm;
                } 
                else
                {
                    return 0f;
                }
            }
        }

        public float _bpm;

        void Awake()
        {
            _isScenarioRunning = false;
            _bpm = 120;
            _playerZone = this.gameObject.transform.Find("ShapeGymnaseCirclePlayerZone").gameObject;
            _vectorBagPlayer = this.gameObject.transform.Find("ShapeGymnaseVectorPlayerBag").gameObject;
            _bagZone = this.gameObject.transform.Find("ShapeGymnaseCircleBagZone").gameObject;
            _vectorConsigne = this.gameObject.transform.Find("ShapeGymnaseVectorConsigne").gameObject;
        }

        private void Start()
        {
            playerPrefab = this.gameObject.transform.parent.parent.gameObject;
            sessionManager = playerPrefab.GetComponent<SessionManager>();
            playerSceneController = playerPrefab.GetComponent<DeviceManager>().PlayerScene.GetComponent<PlayerSceneController>(); ;
            
        }
        

        void FixedUpdate()
        {
            ///When a scenario is available we subscribe to his events
            if (Convert.ToBoolean(_scenarioPlayedController) && _isScenarioRunning == false)
            {
                _isScenarioRunning = true;
                _scenarioPlayedController.scenarioBehaviour.setHitEvent += DisplayTarget;
                _scenarioPlayedController.scenarioBehaviour.missedTargetEvent += DisplayTargetMissed;
                _scenarioPlayedController.scenarioBehaviour.unsetHitEvent += UndisplayTarget;
                _scenarioPlayedController.scenarioBehaviour.targetHittedEvent += DisplayTargetHitted;
            }
            else if (!Convert.ToBoolean(_scenarioPlayedController) && _isScenarioRunning == true)
            {
                SetDefaultMaterial();
                _isScenarioRunning = false;
                _scenarioPlayedController.scenarioBehaviour.targetHittedEvent -= DisplayTargetHitted;
                _scenarioPlayedController.scenarioBehaviour.missedTargetEvent -= DisplayTargetMissed;
                _scenarioPlayedController.scenarioBehaviour.setHitEvent -= DisplayTarget;
                _scenarioPlayedController.scenarioBehaviour.unsetHitEvent -= UndisplayTarget;
            }

            //makeBeat();
            posPlayerCircle = new Vector3(playerSceneController.player.transform.localPosition.x, playerSceneController.ground.transform.localPosition.y, playerSceneController.player.transform.localPosition.z);
            posBagCircle = new Vector3(playerSceneController.bag.transform.localPosition.x, playerSceneController.ground.transform.localPosition.y - 0.02f, playerSceneController.bag.transform.localPosition.z);
            posVectorPlayer = new Vector3(playerSceneController.bag.transform.localPosition.x, playerSceneController.ground.transform.localPosition.y - 0.015f, playerSceneController.bag.transform.localPosition.z);
            posVectorConsigne = new Vector3(playerSceneController.bag.transform.localPosition.x, playerSceneController.ground.transform.localPosition.y - 0.015f, playerSceneController.bag.transform.localPosition.z);

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
          
            _vectorConsigne.transform.localEulerAngles = new Vector3(0, consignePlayedScenario, 0);
            _vectorBagPlayer.transform.localEulerAngles = new Vector3(0, Vector3.SignedAngle(_bagDirNormalized, -playerSceneController.bag.transform.right, -playerSceneController.bag.transform.forward), 0);
        }

        void makeBeat()
        {

            _playerZone.transform.localScale *= (_bpm - 120) / 250;
            Debug.Log(new Vector3((_bpm - 120) / 250, (_bpm - 120) / 250, (_bpm - 120) / 250));
        }

        IEnumerator displayOnBagZoneHit()
        {
            MeshRenderer bagMesh = _bagZone.transform.Find("ExteriorCircle").gameObject.GetComponent<MeshRenderer>();

            bagMesh.material = glowGreenMaterial;
            yield return new WaitForSeconds(0.25f);
            bagMesh.material = glowWhiteMaterial;
        }

        IEnumerator displayTargetMissedHit()
        {
            MeshRenderer bagMesh = _bagZone.transform.Find("ExteriorCircle").gameObject.GetComponent<MeshRenderer>();

            bagMesh.material = glowRedMaterial;
            yield return new WaitForSeconds(0.5f);
            bagMesh.material = glowWhiteMaterial;
        }

        void DisplayTargetHitted()
        {
            StartCoroutine(displayOnBagZoneHit());
        }

        void DisplayTargetMissed()
        {
            StartCoroutine(displayTargetMissedHit());
        }

        void DisplayTarget(GameObject display)
        {
            MeshRenderer vectMesh = _vectorConsigne.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            vectMesh.material = glowRedMaterial;
        }

        void UndisplayTarget(GameObject display)
        {
            MeshRenderer vectMesh = _vectorConsigne.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            vectMesh.material = glowBlueMaterial;
        }

        void SetDefaultMaterial()
        {
            _vectorConsigne.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = glowBlueMaterial;
            _bagZone.transform.Find("ExteriorCircle").gameObject.GetComponent<MeshRenderer>().material = glowWhiteMaterial;
        }

    }


}