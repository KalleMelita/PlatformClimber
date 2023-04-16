using UnityEngine;
using System.Collections.Generic;

public interface ITemplate
{
    ITemplate generateMap(uint height);

    void DestroyAllInstantiables();

    uint GetElementHeight();

    uint GetElementPositionHeight();

    List<Instantiable> GetInstantiables();
}