using SparringManager;
using SparringManager.DataManager;
using SparringManager.DataManager.HitLine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;
using UnityEngine;

namespace SparringManager.HitLine
{
    //Classe du Controller du scenario HitLine
    public class HitLineController : MonoBehaviour
    {
        private StructScenarios controllerStruct;
        private ScenarioController scenarioControllerComponent;
        
        [SerializeField]
        private GameObject _scenarioComposant;

        private float _reactTime;
        private float _startScenario;

        private HitLineStruct hitLineControllerStruct;
        private HitLineDataStruct hitLineData;
        public static bool _hitted = false;

        void Start()
        {
            scenarioControllerComponent = GetComponent<ScenarioController>();
            controllerStruct = scenarioControllerComponent._controllerStruct;
            hitLineControllerStruct = controllerStruct.HitLineStruct;
            _startScenario = Time.time;

            Vector3 _pos3d;
            _pos3d.x = this.gameObject.transform.position.x;
            _pos3d.y = this.gameObject.transform.position.y;
            _pos3d.z = this.gameObject.transform.position.z + 100f;

            SessionManager.InstantiateAndBuildScenario(controllerStruct, this.gameObject, _pos3d, _scenarioComposant);
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
            float _timeBeforeHit = hitLineControllerStruct.TimeBeforeHit;
            float _deltaHit = hitLineControllerStruct.DeltaHit;

            RaycastHit hit;
            Vector3 rayCastOrigin = new Vector3(position2d_.x, position2d_.y, this.gameObject.transform.position.z);
            Vector3 rayCastDirection = new Vector3(0, 0, 1);

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

        void OnDestroy()
        {
            GetData();
            DataManager.DataManager.ToCSV(hitLineData.HitLineDataBase, "C:\\Users\\IIFR\\Documents\\GitHub\\Hitbox_Test\\HTBX_SparringManager\\_data\\Tableau.csv");
            //DataManager.DataManager.ToCSV(hitLineData.HitLineDataBase, "..\\..\\..\\_data\\Tableau.csv");
            _reactTime = 0;
            _hitted = false;
            Debug.Log(this.gameObject.name + "has been destroyed");
        }

        void GetData()
        {
            hitLineData = new HitLineDataStruct(_hitted, _reactTime, HitLine.mouvementConsign, HitLine.timeListScenario);
            DataManager.DataManager.DataBase.Add(hitLineData);            
            Debug.Log(DataManager.DataManager.DataBase);
        }


        

    }
}