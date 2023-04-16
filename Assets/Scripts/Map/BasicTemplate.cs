using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic Template to build a part of the map. Contains a list of <see cref="Instantiable"/> and the positions on which they shall be created.
/// </summary>
abstract public class BasicTemplate : MonoBehaviour
{
    /// <summary>
    /// Get Height of this part of the Map.
    /// </summary>
    /// <returns>Element Height.</returns>
    abstract public uint GetElementHeight();

    /// <summary>
    /// Get Elements position Height  on the x axis.
    /// </summary>
    /// <returns>Elemts position height on the x axis.</returns>
    abstract public uint GetElementPositionHeight();

    /// <summary>
    /// Get all Instantiables and compute their X-Axis position. 
    /// </summary>
    /// <param name="height">Current height on the X-Axis.</param>
    abstract protected void fillInstantiables(uint height);

    /// <summary>
    /// Prefab to be created on the instantiables place.
    /// </summary>
    protected GameObject floorPrefab = null;

    /// <summary>
    /// List of the instantiables of this Template.
    /// </summary>
    protected List<Instantiable> instantiables = new List<Instantiable>();

    /// <summary>
    /// Get list of all instantiables of this template.
    /// </summary>
    /// <returns><see cref="instantiables"/></returns>
    public List<Instantiable> GetInstantiables()
    {
        return instantiables;
    }

    /// <summary>
    /// runs through all instantiables and instantiate them at heir given poition.
    /// Remains the instantiated Game Object in the <see cref="Instantiable"/> object.
    /// </summary>
    public virtual void GenerateMap()
    {
        foreach (Instantiable instantiable in instantiables)
        {
            Debug.Log(System.String.Format("Create Instantiable at vertikal Position:{0}.", instantiable.position.y));
            GameObject prefab = Instantiate(instantiable.prefab,
                                            instantiable.position,
                                            instantiable.quad);
            instantiable.prefab = prefab;
        }
    }

    /// <summary>
    /// Destroys the instantiated Game Object.
    /// The Game Object were instantiated in <see cref="GenerateMap"/> and were saved in the <see cref="Instantiable"/> object.
    /// </summary>
    public void DestroyAllInstantiables(){
        foreach(Instantiable instantiable in instantiables){
            Destroy(instantiable.prefab);
        }
    }

    /// <summary>
    /// Assigne Prefab.
    /// </summary>
    protected void assignPrefab(){
        floorPrefab = createFloorPrefab();
    }

    /// <summary>
    /// Load the prefab from the Resources. This Prefab wil be instantiated at the instantiables position.
    /// </summary>
    /// <returns>Prefab Game Object from Resources</returns>
    private GameObject createFloorPrefab()
    {
        GameObject floor  = Resources.Load<GameObject>("Floor");
        return floor;
    }
}
