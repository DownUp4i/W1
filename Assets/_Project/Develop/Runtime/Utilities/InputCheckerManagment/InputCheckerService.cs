using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.InputCheckerManagment
{
    public class InputCheckerService
    {
        public event Action<char> OnKeyPressed;
        private bool isActive = false;

        public IEnumerator Check()
        {
            while (isActive)
            {
                string input = Input.inputString;

                foreach (char c in input)
                    OnKeyPressed?.Invoke(c);

                yield return null;
            }
        }

        public void Activate() => isActive = true;
        public void Deactivate() => isActive = false;
    }
}
