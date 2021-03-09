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
        public int accelerationMax { get { return _structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return _structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return _structScenario.DeltaTimeMin; } }
        public float deltaHit { get { return _structScenario.DeltaHit; } }
        public float timeBeforeHit { get { return _structScenario.TimeBeforeHit; } }
        public float timerScenario { get; set;  }
        public float startTimeScenario { get; set; }



        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new CrossLineStruct();
            _structScenario = structScenarios.CrossLineStruct;

            timerScenario = structScenarios.TimerScenario;
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

        //Usefull parameters of the scenario, they are in the crossLineStructure
        public int accelerationMax { get { return _structScenario.AccelerationMax; } }
        public int deltaTimeMax { get { return _structScenario.DeltaTimeMax; } }
        public int deltaTimeMin { get { return _structScenario.DeltaTimeMin; } }
        public float deltaHit { get { return _structScenario.DeltaHit; } }
        public float timeBeforeHit { get { return _structScenario.TimeBeforeHit; } }

        public float timerScenario { get; set; }


        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new HitLineStruct();
            _structScenario = structScenarios.HitLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }

}