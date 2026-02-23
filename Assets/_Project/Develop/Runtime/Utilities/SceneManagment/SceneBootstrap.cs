using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract IEnumerator Initialize();

        public abstract void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null);

        public abstract void Run();
    }
}
