using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BasicTemplate : MonoBehaviour
{
    abstract public uint getElementHeight();

    abstract public uint getElementPositionHeight();

    abstract protected void fillInstantiables(uint height);

    protected GameObject floorPrefab = null;

    protected List<Instantiable> instantiables = new List<Instantiable>();

    public List<Instantiable> getInstantiables()
    {
        return instantiables;
    }

    public virtual void generateMap()
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

    public void destroyAllInstantiables(){
        foreach(Instantiable instantiable in instantiables){
            Destroy(instantiable.prefab);
        }
    }

    protected void assignPrefab(){
        floorPrefab = createFloorPrefab();
    }

    private GameObject createFloorPrefab()
    {
        GameObject floor  = Resources.Load<GameObject>("Floor");
        return floor;
    }
}
