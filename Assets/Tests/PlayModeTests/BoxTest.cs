using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoxTest
{

GameObject character ;
GameObject toTest;

float jumptime;
Vector3 positionBox;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        character = Resources.Load<GameObject>("Character");
        CharacterScript charScript = character.GetComponent<CharacterScript>();
        charScript.EarthAcceleration = Physics2D.gravity.y;
        charScript.setRigidbody(character.GetComponent<Rigidbody2D>());
        jumptime = (float)charScript.getJumpTime(StaticInfomration.velocity);

        toTest = Resources.Load<GameObject>("Floor");
        positionBox = new Vector3(0, 0, 0);
        toTest = GameObject.Instantiate(toTest, positionBox, Quaternion.identity);

        yield return new EnterPlayMode();
    }

    [UnityTest]
    // Character is above Box - Character has to jump.
    public IEnumerator BoxTestWithEnumeratorDoJump()
    {
        Vector3 positionCharacter = new Vector3(0, 1, 0);
        character = GameObject.Instantiate(character, positionCharacter, Quaternion.identity);

        yield return new WaitForSeconds(jumptime);
        Assert.Greater(character.transform.position.y, positionCharacter.y);
    }

    [UnityTest]
    // Character is under the Box - Character has not to jump.
    public IEnumerator BoxTestWithEnumeratorDoNotJump()
    {
        Vector3 positionCharacter = new Vector3(0, -1, 0);
        character = GameObject.Instantiate(character, positionCharacter, Quaternion.identity);

        yield return new WaitForSeconds(jumptime);
        Assert.Less(character.transform.position.y, positionCharacter.y);
    }

    [UnityTearDown]
    public IEnumerator UnityTearDown()
    {
        Object.Destroy(character);
        Object.Destroy(toTest);

        yield return new ExitPlayMode();
    }
}
