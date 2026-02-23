using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public class ReactiveList <T>
    {
        public event Action OnAdded;
        public event Action OnRemoved;
        public event Action OnCleared;

        private List<T> _values;

        public ReactiveList(List<T> values) => _values = values;

        public ReactiveList() => _values = new List<T>();

        public IReadOnlyList<T> Values => _values;

        public void Add(T value)
        {
            _values.Add(value);
            OnAdded?.Invoke();
        }

        public void Remove(T value)
        {
            _values.Remove(value);
            OnRemoved?.Invoke();
        }

        public void Clear()
        {
            _values.Clear();
            OnCleared?.Invoke();
        }
    }
}
