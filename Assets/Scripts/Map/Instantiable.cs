using UnityEngine;
using System;

public class Instantiable : IComparable<Instantiable>
{
    public GameObject prefab { get; set;}
    public Vector3 position { get; }
    public Quaternion quad { get; }

    public Instantiable(GameObject prefab, Vector3 position, Quaternion quad)
    {
        this.prefab = prefab;
        this.position = position;
        this.quad = quad;
    }

    public int CompareTo(Instantiable instantiable)
    {
        if (instantiable == null) return 1;

        if (instantiable != null)
            if (instantiable.position.y < position.y)
            {
                return -1;
            }
            else if (instantiable.position.y == position.y)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        else
            throw new ArgumentException("Object is not a Temperature");
    }
}
