using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    Rigidbody2D m_Rigidbody;

    /// <summary>
    /// Player control speed for character.
    /// </summary>
    public float Speed = 2;

    public float startingVelocity = 2;

    private float earthAcceleration = 0 ;
    public float EarthAcceleration
    {
        set
        {
            earthAcceleration = value;
        }
    }

    public float actualTime = 0;

    public void SetRigidbody(Rigidbody2D rigidbody2D)
    {
        m_Rigidbody = rigidbody2D;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        GetGravity();
        Jump(startingVelocity);
    }

    private void GetGravity()
    {
        earthAcceleration = Physics2D.gravity.y;
    }

    public double GetJumpTime(float velocity)
    {
        return -1 * velocity / earthAcceleration;
    }

    public void Jump(float velocity)
    {
        m_Rigidbody.velocity = new Vector2(0, velocity);
        actualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        move(transform.position, new Vector3(movement, 0, 0), Time.deltaTime);
        actualTime += Time.deltaTime;
    }

    public void move(Vector3 currentPosition, Vector3 movement, float deltaTime)
    {
        transform.position = computePosition(currentPosition, movement, deltaTime);
    }

    public Vector3 computePosition(Vector3 currentPosition, Vector3 movement, float deltaTime)
    {
        currentPosition += movement * deltaTime * Speed;
        return correctLimitation(currentPosition);
    }

    private Vector3 correctLimitation(Vector3 inputVector)
    {
        if (inputVector.x < StaticInfomration.cameraLeftCoordinates)
        {
            return new Vector3(StaticInfomration.cameraLeftCoordinates, inputVector.y, 0);
        }

        if (inputVector.x > StaticInfomration.cameraRightCoordinates)
        {
            return new Vector3(StaticInfomration.cameraRightCoordinates, inputVector.y, 0);
        }

        return inputVector;
    }
}
