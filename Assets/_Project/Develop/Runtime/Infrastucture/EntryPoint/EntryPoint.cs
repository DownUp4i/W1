using System.Collections;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Infrastucture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutineManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastucture.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            DIContainer projectContainer = new DIContainer();

            ProjectContextRegistration.Process(projectContainer);

            projectContainer.Resolve<ICoroutineService>().StartPerform(Init(projectContainer));
        }

        private IEnumerator Init(DIContainer container)
        {
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}