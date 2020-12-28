using SparringManager;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
using System.Collections.Generic;
using UnityEngine;

namespace SparringManager.HitLine
{
    //Classe du Controller du scenario HitLine
    public class HitLineController : MonoBehaviour
    {
        private StructScenarios _controllerStruct;
        private HitLineDataStruct hitLineData;

        private ScenarioController _scenarioControllerComponent;
        

        [SerializeField]
        private GameObject _scenarioPrefab;

        private float _reactTime;
        private float _startScenario;

        public static bool _hitted = false;

        void Start()
        {
            _scenarioControllerComponent = GetComponent<ScenarioController>();
            _controllerStruct = _scenarioControllerComponent._controllerStruct;
            _startScenario = Time.time;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(_controllerStruct, this.gameObject, _pos3d, _scenarioPrefab);
        }

        void OnDestroy()
        {
            GetData();
            _reactTime = 0;
            _hitted = false;

            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        void GetData()
        {
            hitLineData = new HitLineDataStruct(_hitted, _reactTime, HitLine.mouvementConsign, HitLine.timeListScenario);
            DataManager.DataManager.DataBase.Add(hitLineData);
        }

        private void OnEnable()
        {
            ImpactManager.onInteractPoint += GetHit;
        }

        private void OnDisable()
        {
            ImpactManager.onInteractPoint -= GetHit;
        }

        public void GetHit(Vector2 position2d_)
        {
            float tTime = Time.time - _startScenario;
            float _timeBeforeHit = _controllerStruct._timeBeforeHit;
            float _deltaHit = _controllerStruct._deltaHit;

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3 (position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3 (0,0,1);

            bool rayOnTarget = Physics.Raycast(rayCastOrigin, rayCastDirection, out hit, 250);
            bool canHit = (tTime > _timeBeforeHit && (tTime - _timeBeforeHit) < _deltaHit);
            
            if (rayOnTarget && canHit && _hitted == false)
            {
                _reactTime = tTime - _timeBeforeHit;
                _hitted = true;

                Debug.Log("Line touched : " + position2d_);
                Debug.Log("React time : " + _reactTime);
            }
        }

    }
}