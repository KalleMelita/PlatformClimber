using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

/// <summary>
/// Test static readonly Variables of <see cref="MapBuilder"/>.
/// </summary>
public class MapBuilderTest 
{
    [Test]
    public void CheckGeneratedHeight()
    {
        Assert.IsTrue(MapBuilder.get_GENERATED_HEIGHT >= 50);
    }

    [Test]
    public void CheckUpperLimit()
    {
        Assert.IsTrue(MapBuilder.get_HEIGHT_GENERATING <= (MapBuilder.get_GENERATED_HEIGHT/2));
    }

    [Test]
    public void CheckLowerLimit()
    {
        Assert.IsTrue(MapBuilder.get_HEIGHT_DESTROING <= (MapBuilder.get_GENERATED_HEIGHT/2));
    }
}
