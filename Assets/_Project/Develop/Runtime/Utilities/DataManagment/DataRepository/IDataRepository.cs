using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
    public interface IDataRepository
    {
        IEnumerator Read(string key, Action<string> onRead);

        IEnumerator Write(string key, string serializedData);

        IEnumerator Remove(string key);

        IEnumerator Exist(string key, Action<bool> onExist);
    }
}
