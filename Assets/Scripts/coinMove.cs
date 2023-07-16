using UnityEngine;

public class coinMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // The speed at which the coin rotates

    // Update is called once per frame
    void Update()
    {

        // Rotate the coin on its y-axis
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
  
}
