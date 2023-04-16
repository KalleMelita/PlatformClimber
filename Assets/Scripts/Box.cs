using UnityEngine;

public class Box : MonoBehaviour
{

    public float jumpVelocity = StaticInfomration.velocity;


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject boxGameObject = gameObject;
        Collision(collision, gameObject);
    }

    private void Collision(Collision2D collision, GameObject boxGameObject){
        if (collision.gameObject.tag != null && collision.gameObject.tag.Equals(TagEnum.Player))
        {
            GameObject player = collision.gameObject;

            if (player.transform.position.y > boxGameObject.transform.position.y)
            {
                CharacterScript character = (CharacterScript)player.GetComponent(typeof(CharacterScript));
                character.Jump(jumpVelocity);
            }
        }
    }
}
