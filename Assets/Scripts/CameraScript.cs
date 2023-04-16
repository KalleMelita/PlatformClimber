using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject character;

    void Start()
    {
        character = GameObject.FindWithTag(TagEnum.Player);
    }

    void Update()
    {
        Vector3 characterPosition = character.transform.position;
        float computedVerticalPos = computeNewPosition(characterPosition.y, transform.position.y);
        transform.position = new Vector3(transform.position.x, computedVerticalPos, transform.position.z);
    }

    public float computeNewPosition(float charVerticalPos, float cameraVerticalPos)
    {
        if(charVerticalPos > cameraVerticalPos){
            return charVerticalPos;
        }

        return cameraVerticalPos;
    }
}
