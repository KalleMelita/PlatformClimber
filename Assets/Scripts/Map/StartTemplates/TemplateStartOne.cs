using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starttemplate. This can be used to start the game.
/// </summary>
public class TemplateStartOne : BasicTemplate, ITemplate
{
    private readonly uint elementHeight = 8;

    private uint elementPositionHeight = 8;

    protected override void fillInstantiables(uint height)
    {
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(0, -5 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(5, 7 + height, 0), Quaternion.identity));
        instantiables.Add(new Instantiable(floorPrefab, new Vector3(5, 1 + height, 0), Quaternion.identity));
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
