using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment
{
    public class InputCheckerService
    {
        public event Action<KeyCode> OnKeyPressed;
        private bool isActive = false;

        public IEnumerator Check()
        {
            while (isActive)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        OnKeyPressed?.Invoke(key);
                    }
                }
                yield return null;
            }
        }

        public void Activate() => isActive = true;
        public void Deactivate() => isActive = false;
    }
}
