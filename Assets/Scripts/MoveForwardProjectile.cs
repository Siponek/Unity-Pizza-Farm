using UnityEngine;

public class MoveForwardProjectile : MonoBehaviour
{
    public float projectileSpeed = 1f;
    public float xMin = -15f;
    public float xMax = 15f;
    public float zMin = -15f;
    public float zMax = 15f;

    void Update()
    {
        // Calculate the motion direction and normalize it to have consistent speed
        Vector3 motionDirection = new Vector3(0, 0, projectileSpeed);
        // Move the player
        transform.Translate(projectileSpeed* Time.deltaTime * motionDirection);

        // Clamp the player's position within the bounds
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin, zMax)
        );
    }

    void Start()
    {
        Debug.Log("Hello mom, pizza is controlled!");
    }
}
