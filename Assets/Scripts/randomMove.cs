using UnityEngine;

public class randomMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveRange = 10f;

    private float initialPosition;

    void Start()
    {
        initialPosition = transform.position.x;
    }

    void Update()
    {
        float xMovement = Mathf.PingPong(Time.time * moveSpeed, moveRange);
        transform.position = new Vector3(initialPosition + xMovement, transform.position.y, transform.position.z);
    }
}
