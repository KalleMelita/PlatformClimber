using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FloorTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator FloorTestWithEnumeratorPasses()
    {
        GameObject gameObject = new GameObject();
        CharacterScript character = gameObject.AddComponent<CharacterScript>();
        character.speed = 1;
        gameObject.AddComponent<Rigidbody2D>();

        float deltaTime = 2;
        Vector3 start = new Vector3(0, 0, 0);
        Vector3 movement = new Vector3(1, 0, 0);

        character.move(start,movement,deltaTime);
        yield return new WaitForSeconds(deltaTime);


        Assert.AreEqual(2, gameObject.transform.position.x);    
    }
}
