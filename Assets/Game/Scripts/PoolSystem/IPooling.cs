public interface IPooling
{
    public void InstantiatePoolObject(IPoolObject poolObject);
    public bool TryPoolObject(out IPoolObject result);
    public void PoolObject(IPoolObject poolObject);
}
