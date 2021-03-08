using UnityEngine;

namespace SparringManager.Scenarios
{
    public class Scenario : MonoBehaviour
    {
        /*
         * Class that will be a component of every prefab, it allows us to stock parameters of our scenario
         */
        [SerializeField]
        private StructScenarios _controllerStruct;
        public StructScenarios ControllerStruct
        {
            get
            {
                return _controllerStruct;
            }
            set
            {
                _controllerStruct = value;
            }
        }

    }
    public abstract class Scenario<StructScenario> where StructScenario: IStructScenario
    {
        public abstract StructScenario structScenario { get; set; }

        public static T CreateScenarioObject<T>() where T: Scenario<StructScenario>, new()
        {
            T myObj = new T();
            return myObj;
        }

        public abstract void Init(StructScenarios structScenarios);
    }


    public class ScenarioCrossLine : Scenario<CrossLineStruct>
    {

        private CrossLineStruct _structScenario;
        public override CrossLineStruct structScenario
        {
            get
            {
                return _structScenario;
            }
            set
            {
                _structScenario = value;
            }
        }

        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax;
        public int deltaTimeMax;
        public int deltaTimeMin;
        public float deltaHit;
        public float timeBeforeHit;

        public float timerScenario;

        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new CrossLineStruct();
            _structScenario = structScenarios.CrossLineStruct;

            SetControllerVariables();

        }
        private void SetControllerVariables()
        {
            accelerationMax = _structScenario.AccelerationMax;
            deltaTimeMax = _structScenario.DeltaTimeMax;
            deltaTimeMin = _structScenario.DeltaTimeMin;
            deltaHit = _structScenario.DeltaHit;
            timeBeforeHit = _structScenario.TimeBeforeHit;
            timerScenario = _structScenario.TimerScenario;
        }
    }



    public class ScenarioHitLine : Scenario<HitLineStruct>
    {
        private HitLineStruct _structScenario;
        public override HitLineStruct structScenario
        {
            get
            {
                return _structScenario;
            }
            set
            {
                _structScenario = value;
            }
        }

        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new HitLineStruct();
            _structScenario = structScenarios.HitLineStruct;
        }

    }

}