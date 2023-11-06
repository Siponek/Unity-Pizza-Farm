using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float playerSpeed = 20f;
    public float xMin = -10f;
    public float xMax = 10f;
    public float zMin = -15f;
    public float zMax = -10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        // Get the input from the keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the motion direction and normalize it to have consistent speed
        Vector3 motionDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the player
        transform.Translate(playerSpeed * Time.deltaTime * motionDirection);

        // Clamp the player's position within the bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, xMin, xMax),
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin, zMax)
        );
    }

    void Start()
    {
        Debug.Log("Hello mom, player is controlled!");
    }
}
