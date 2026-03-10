namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeyStorage
{
    public interface IDataKeyStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}
