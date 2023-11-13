using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooling 
{
    public Queue<IPoolObject> PoolObjects { get; }
    public void InstantiateObject();
}
