using UnityEngine;
using System.Collections.Generic;

public interface ITemplate
{
    ITemplate generateMap(uint height);

    void destroyAllInstantiables();

    uint getElementHeight();

    uint getElementPositionHeight();

    List<Instantiable> getInstantiables();
}