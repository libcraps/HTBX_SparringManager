using UnityEngine;

namespace SparringManager.Scenarios
{
    /*
     * Class that allows us tu control scenarios
     * Mother class : Scenario<StructScenario>
     * 
     * Scenario created by inheretent of Scenario<StructScenario>:
     *      ScenarioCrossLine : Scenario<CrossLineStruct>
     *      ScenarioHitLine : Scenario<HitLineStruct>
     *      ScenarioSimpleLine : Scenario<SimpleLineStruct>
     *      ScenarioSplHitLine : Scenario<SplHitLineStruct>
     *      ScenarioSimpleHit : Scenario<SimpleHitStruct>
     */
    public abstract class Scenario<StructScenario> where StructScenario: IStructScenario
    {
        public static T CreateScenarioObject<T>() where T : Scenario<StructScenario>, new()
        {
            T myObj = new T();
            return myObj;
        }
        public abstract StructScenario structScenario { get; set; }
        public abstract float timerScenario { get; set; }
        public abstract float startTimeScenario { get; set; }
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
        public override float timerScenario { get; set;  }
        public override float startTimeScenario { get; set; }

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

        public override float timerScenario { get; set; }
        public override float startTimeScenario { get; set; }

        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new HitLineStruct();
            _structScenario = structScenarios.HitLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSimpleLine : Scenario<SimpleLineStruct>
    {
        private SimpleLineStruct _structScenario;
        public override SimpleLineStruct structScenario
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

        public override float timerScenario { get; set; }
        public override float startTimeScenario { get; set; }
        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new SimpleLineStruct();
            _structScenario = structScenarios.SimpleLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSplHitLine : Scenario<SplHitLineStruct>
    {
        private SplHitLineStruct _structScenario;
        public override SplHitLineStruct structScenario
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
        public int ScaleMaxValue { get { return _structScenario.ScaleMaxValue; } }
        public float ScaleSpeed { get { return _structScenario.ScaleSpeed; } }

        public override float timerScenario { get; set; }
        public override float startTimeScenario { get; set; }

        public override void Init(StructScenarios structScenarios)
        {
            //_structScenario = new SplHitLineStruct();
            _structScenario = structScenarios.SplHitLineStruct;

            timerScenario = structScenarios.TimerScenario;
        }

    }
    public class ScenarioSimpleHit : Scenario<SimpleHitStruct>
    {
        private SimpleHitStruct _structScenario;
        public override SimpleHitStruct structScenario
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

        public override float timerScenario { get; set; }
        public override float startTimeScenario { get; set; }

        public override void Init(StructScenarios structScenarios)
        {
            _structScenario = new SimpleHitStruct();
            _structScenario = structScenarios.SimpleHitStruct;

            timerScenario = structScenarios.TimerScenario;
        }
    }
}