using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float movementSpeed = 3f;
    private string animalName;

    void Update()
    {
        // Calculate the motion direction and normalize it to have consistent speed
        Vector3 motionDirection = new Vector3(0, 0, movementSpeed);
        // Move the animal
        transform.Translate(movementSpeed * Time.deltaTime * motionDirection);
    }

    void Start()
    {
        animalName = gameObject.name;
        Debug.Log($"Hello mom, {animalName} is controlled!");
    }
}
