namespace x3e.simulation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DictionaryComponent = System.Collections.Generic.Dictionary<System.Type, SimulatorObject>;
    public abstract class SimulatorObject : IDisposable
    {
        public virtual Guid UID { get; } = Guid.NewGuid();

        protected SimulatorObject() => SimulationStorage.Register(this);

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Stop() { }
        public virtual void Clear() { }


        public SimulatorObject Parent { get; private set; }

        public TObject GetComponent<TObject>() where TObject : SimulatorObject => (TObject)GetComponent(typeof(TObject));

        public object GetComponent(Type typeOfComponent)
        {
            var result = childrens.ContainsKey(typeOfComponent) ?
            childrens[typeOfComponent] : null;

            if (result == null && Parent?.GetType() == typeOfComponent)
                result = Parent;
            return result;
        }
            

        public void AddComponent<TObject>() where TObject : SimulatorObject, new()
        {
            var act = Activator.CreateInstance<TObject>();
            act.Parent = this;
            childrens.Add(typeof(TObject), act);
        }
            

        public void Dispose() => Clear();

        private DictionaryComponent childrens { get; } = new DictionaryComponent();
        public DictionaryComponent getChildrens() => childrens;
        public bool IsChildrens => childrens.Any();
    }
}