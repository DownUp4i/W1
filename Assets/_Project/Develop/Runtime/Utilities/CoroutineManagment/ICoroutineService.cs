using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutineManagment
{
    public interface ICoroutineService
    {
        public Coroutine StartPerform(IEnumerator coroutineFunction);

        public void StopPerform(Coroutine coroutine);
    }
}
