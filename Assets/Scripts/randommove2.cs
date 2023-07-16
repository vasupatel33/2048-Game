using UnityEngine;

public class randommove2 : MonoBehaviour
{
    public float moveRange = 2f;
    public float moveSpeed = 1f;

    private float initialPosition;

    void Start()
    {
        initialPosition = transform.position.y;
    }

    void Update()
    {
        float yMovement = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = new Vector3(transform.position.x, initialPosition + yMovement, transform.position.z);
    }
}
