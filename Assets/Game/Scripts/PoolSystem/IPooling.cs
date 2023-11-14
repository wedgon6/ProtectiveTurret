using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooling 
{
    public int Capasity { get; }
    public bool TryPoolObject(out IPoolObject result);
    public void PoolObject(IPoolObject poolObject);
}
