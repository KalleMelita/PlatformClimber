using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TemplateTest
{

    /// <summary>
    /// Checks if rigid body is working
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TemplateTestPrefabRigitBodyWorks()
    {
        GameObject character = Resources.Load<GameObject>("Character");

        var originalPosition = character.transform.position.y;
        character = GameObject.Instantiate(character, character.transform.position, Quaternion.identity);

        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(originalPosition, character.transform.position.y);


        Object.Destroy(character);
    }

    /// <summary>
    /// Checks if Templates are doable.
    /// 
    /// This test checks if the Player is able to reach the next higher floor-Element in this Template.
    /// If not, this Template is 'undoable'. The Player cannot beat this Template.
    [UnityTest]
    public IEnumerator TemplateTestStartDoability()
    {
        return TemplateTestDoability(MapBuildingModes.START_TEST);
    }

    /// <summary>
    /// Checks if Templates are doable.
    /// 
    /// This test checks if the Player is able to reach the next higher floor-Element in this Template.
    /// If not, this Template is 'undoable'. The Player cannot beat this Template.
    [UnityTest]
    public IEnumerator TemplateTestLevelOneDoability()
    {
        return TemplateTestDoability(MapBuildingModes.LEVEL_ONE_TEST);
    }

    private IEnumerator TemplateTestDoability(MapBuildingModes mode)
    {
        GameObject root = new GameObject();
        GameObject character = Resources.Load<GameObject>("Character");
        character = GameObject.Instantiate(character, new Vector3(0, -20, 0), Quaternion.identity);

        MapBuilder mapBuilder = root.AddComponent<MapBuilder>();
        mapBuilder.Mode = mode;

        // Wait one Frame. Start Method of MapBuilder is called. Mapbuilder generates the instantiables.
        yield return new WaitForFixedUpdate();

        GameObject[] mapTemplates = GameObject.FindGameObjectsWithTag("Map");
        List<Instantiable> instantiables = new List<Instantiable>();

        foreach (GameObject mapTemplate in mapTemplates)
        {
            ITemplate template = mapTemplate.GetComponent<ITemplate>();
            instantiables.AddRange(template.GetInstantiables());
        }

        Debug.Log(System.String.Format("Instatiable Size:{0}!", instantiables.Count));
        instantiables.Sort();
        instantiables.Reverse();

        yield return TestInstantiables(instantiables, character);

        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        foreach(GameObject floor in floors){
            Object.Destroy(floor);
        }
        yield return null;

        foreach(GameObject mapTemplate in mapTemplates){
            Object.Destroy(mapTemplate);
        }
        yield return null;
    }

    /// <summary>
    /// Goes throw all sorted Instantiables. Creates an Instance of the Character above the lowest Instantiable.
    /// Lets Character jump from that until the heighest Jump Height is reached.
    /// Checks if the nearest Instantiable could be reached.
    /// If it could be reached check for the next one.
    /// If it could not be reached the tests fails.
    /// 
    /// </summary>
    /// <param name="instantiables">List of sorted Instantiables.</param>
    /// <param name="character">Character Gameobject</param>
    /// <returns></returns>
    private IEnumerator TestInstantiables(List<Instantiable> instantiables, GameObject character)
    {
        for (int i = 0; i < instantiables.Count - 1; i++)
        {
            // If an Instantiable has the same height as the next instantiable, skip it. 
            if (instantiables[i].position.y >= instantiables[i + 1].position.y)
            {
                Debug.Log(System.String.Format("Continue interation {0}!", i));
                continue;
            }

            yield return new WaitForFixedUpdate();
            //Create Instance of Character
            GameObject characterInst = GameObject.Instantiate(character, instantiables[i].position, Quaternion.identity);
            CharacterScript charScript = characterInst.GetComponent<CharacterScript>();
            charScript.EarthAcceleration = Physics2D.gravity.y;
            Rigidbody2D rigidBody = characterInst.GetComponent<Rigidbody2D>();

            charScript.SetRigidbody(rigidBody);
            float jumptime = (float)charScript.GetJumpTime(StaticInfomration.velocity);
            Debug.Log(System.String.Format("Jump Time:{0}.", jumptime));

            //Position Before the Jump. 
            var originalPosition = characterInst.transform.position.y;
            Debug.Log(System.String.Format("Character Height before Jump:{0}.", characterInst.transform.position.y));

            //Wait for Jump and until the heighest Heigt is reached.
            yield return new WaitForSeconds(jumptime);

            //Test.
            Assert.Greater(characterInst.transform.position.y, originalPosition);

            Debug.Log(System.String.Format("Instantiable Height:{0}/ Character Height:{1}",
                                            instantiables[i + 1].position.y,
                                            characterInst.transform.position.y));
            Assert.GreaterOrEqual(characterInst.transform.position.y, instantiables[i + 1].position.y);

            //Destroy this Instance of the Character Game Object.
            Object.Destroy(characterInst);
        }
    }
}
