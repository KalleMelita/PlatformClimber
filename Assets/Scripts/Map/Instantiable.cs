using UnityEngine;
using System;

public class Instantiable : IComparable<Instantiable>
{
    /// <summary>
    /// Prefab this Instatiable contains.
    /// </summary>
    public GameObject prefab { get; set;}
    /// <summary>
    /// Position of the Instatiable.
    /// </summary>
    public Vector3 position { get; }
    /// <summary>
    /// Rotation of the Instatiable.
    /// </summary>
    public Quaternion quad { get; }

    /// <summary>
    /// Constructor for <see cref="Instantiable"/>.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="quad"></param>
    public Instantiable(GameObject prefab, Vector3 position, Quaternion quad)
    {
        this.prefab = prefab;
        this.position = position;
        this.quad = quad;
    }

    /// <summary>
    ///  Compares an <see cref="Instantiable"/> Object with this Object.
    /// </summary>
    /// <param name="instantiable">Other object</param>
    /// <returns>greater zero if this object is 'bigger' than the comapred object.
    ///          Zero if both object are 'equal'.
    ///          Less than zero if the other object is 'greater'.</returns>
    /// <exception cref="ArgumentException">Exception if the compared object is not of type <see cref="Instantiable"/>.</exception>
    public int CompareTo(Instantiable instantiable)
    {
        if (instantiable == null) return 1;

        if (instantiable != null)
        {
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
        }
        else
        {
            throw new ArgumentException("Object is not of class " + nameof(Instantiable));
        }
    }
}
