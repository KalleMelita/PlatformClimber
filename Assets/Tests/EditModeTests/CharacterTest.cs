using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterTest
{

    CharacterScript charScript;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        charScript = gameObject.AddComponent<CharacterScript>();
    }

    [Test]
    public void CharacterTestMoveRight()
    {
        charScript.speed = 1;

        Vector3 start = new Vector3(0, 0, 0);
        Vector3 movement = new Vector3(1, 0, 0);

        Assert.AreEqual(new Vector3(1, 0, 0), charScript.computePosition(start, movement, 1));
    }

    [Test]
    public void CharacterTestMoveLeft()
    {
        charScript.speed = 1;

        Vector3 start = new Vector3(0, 0, 0);
        Vector3 movement = new Vector3(-1, 0, 0);

        Assert.AreEqual(new Vector3(-1, 0, 0), charScript.computePosition(start, movement, 1));
    }

    [Test]
    public void CharacterTestMoveTooFarRight()
    {
        charScript.speed = 1;

        Vector3 start = new Vector3(4, 0, 0);
        float tooFar = (StaticInfomration.cameraRightCoordinates - 4) * 2;
        Vector3 movement = new Vector3(tooFar, 0, 0);

        Assert.AreEqual(new Vector3(StaticInfomration.cameraRightCoordinates, 0, 0), charScript.computePosition(start, movement, 1));
    }

    [Test]
    public void CharacterTestMoveTooFarLeft()
    {
        charScript.speed = 1;

        Vector3 start = new Vector3(-4, 0, 0);
        float tooFar = (StaticInfomration.cameraLeftCoordinates - 4) * 2;
        Vector3 movement = new Vector3(tooFar, 0, 0);

        Assert.AreEqual(new Vector3(StaticInfomration.cameraLeftCoordinates, 0, 0), charScript.computePosition(start, movement, 1));
    }

    [Test]
    public void CharacterTestComputeGravity()
    {
        float velocity = 100;
        float earthAcceleration = -10;

        charScript.EarthAcceleration = earthAcceleration;

        GameObject test = new GameObject();
        test.AddComponent<Rigidbody2D>();
        Rigidbody2D testBody = test.GetComponent<Rigidbody2D>();
        testBody.mass = 10;

        Assert.AreEqual(10, charScript.getJumpTime(velocity));
    }
}
