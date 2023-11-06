using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float xMin = -15f;
    public float xMax = 15f;
    public float zMin = -15f;
    public float zMax = 15f;
    private string animalName;

    void Update()
    {
        // Calculate the motion direction and normalize it to have consistent speed
        Vector3 motionDirection = new Vector3(0, 0, movementSpeed);
        // Move the animal
        transform.Translate(movementSpeed * Time.deltaTime * motionDirection);

        // Clamp the player's position within the bounds
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin, zMax)
        );
    }

    void Start()
    {
        animalName = gameObject.name;
        Debug.Log($"Hello mom, {animalName} is controlled!");
    }
}
