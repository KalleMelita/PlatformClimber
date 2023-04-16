using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateThreeLOne : BasicTemplate, ITemplate
{
   private readonly uint elementHeight = 20;

    private uint elementPositionHeight = 20;

    protected override void fillInstantiables(uint height)
    {
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(0, 2 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(0, 12 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(-5, 18 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(5, 18 + height, 0), Quaternion.identity));
    }

    override public uint getElementHeight()
    {
        return elementHeight;
    }

    override public uint getElementPositionHeight()
    {
        return elementPositionHeight;
    }

    public ITemplate generateMap(uint height)
    {
        assignPrefab();
        fillInstantiables(height);
        base.generateMap();
        elementPositionHeight = height + elementHeight;
        return Instantiate(this);
    }
}
