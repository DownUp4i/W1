using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastucture.DI
{
    public class DIContainer
    {
        private Dictionary<Type, Registration> _container = new();
        private DIContainer _parent;

        public bool IsNonLazy { get; private set; }

        public DIContainer() : this(null) { }

        public DIContainer(DIContainer parent) => _parent = parent;

        public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} already register");

            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);

            return registration;
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_container.TryGetValue(typeof(T), out Registration registration))
                return (T)registration.CreateInstanceFrom(this);

            if(_parent != null)
                return _parent.Resolve<T>();

            throw new InvalidOperationException($"Registration for {typeof(T)} not found");
        }

        public void Initialize ()
        {
            foreach(Registration registration in _container.Values)
            {
                if(registration.IsNonLazy)
                    registration.CreateInstanceFrom(this);
            }
        }
    }
}