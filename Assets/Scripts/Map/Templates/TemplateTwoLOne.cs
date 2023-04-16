using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Template for Level One.
/// </summary>
public class TemplateTwoLOne : BasicTemplate, ITemplate
{
    private readonly uint elementHeight = 30;

    private uint elementPositionHeight = 30;

    protected override void fillInstantiables(uint height)
    {
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(3, 1 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(5, 7 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(2, 15 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(-5, 15 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(-5, 23 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(0, 27 + height, 0), Quaternion.identity));
    }

    override public uint GetElementHeight()
    {
        return elementHeight;
    }

    override public uint GetElementPositionHeight()
    {
        return elementPositionHeight;
    }

    public ITemplate generateMap(uint height)
    {
        assignPrefab();
        fillInstantiables(height);
        base.GenerateMap();
        elementPositionHeight = height + elementHeight;
        return Instantiate(this);
    }
}
