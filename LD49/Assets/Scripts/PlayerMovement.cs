using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed;

    private float xMovement = 0f;
    private float yMovement = 0f;
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {

        // Getting movement input
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        // Moving character
        if (xMovement != 0 || yMovement != 0) {
            direction = new Vector3(xMovement, yMovement, transform.position.z);
            direction.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movementSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}