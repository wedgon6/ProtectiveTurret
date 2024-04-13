using System;

public interface IPoolObject
{
    public event Action <IPoolObject>PoolReturned;
    public void ReturnToPool();
}
