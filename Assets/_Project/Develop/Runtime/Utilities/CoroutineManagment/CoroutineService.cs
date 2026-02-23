using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutineManagment
{
    public class CoroutineService : MonoBehaviour, ICoroutineService
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutine) => StartCoroutine(coroutine);

        public void StopPerform(Coroutine coroutine) => StopCoroutine(coroutine);
    }
}
