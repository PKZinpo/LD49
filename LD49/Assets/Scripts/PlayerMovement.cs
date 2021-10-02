using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed;
    [SerializeField] private int canHold;

    private float xMovement = 0f;
    private float yMovement = 0f;
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {

        // Getting movement input
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        if (transform.childCount > 0) {
            foreach (GameObject parcel in GameObject.FindGameObjectsWithTag("Parcel")) {
                if (parcel.transform.parent.gameObject == gameObject) {
                    parcel.transform.position = transform.position;
                }
            }
        }
    }

    private void FixedUpdate() {
        // Moving player
        if (xMovement != 0 || yMovement != 0) {
            direction = new Vector3(xMovement, yMovement, transform.position.z);
            direction.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Contains("Parcel")) {// When parcel picked up by player
            if (transform.childCount < canHold) {
                collision.GetComponent<BoxCollider2D>().enabled = false;
                StopCoroutine(ParcelSpawner.parcelDictionary[collision.transform.position]);
                // Remove parcel entry from dictionary in ParcelSpawner
                ParcelSpawner.parcelDictionary.Remove(collision.transform.position);
                // Destroy timer bar
                Destroy(collision.transform.GetChild(0).gameObject);

                collision.transform.parent = transform;
            }
        }
        else {
            if (transform.childCount > 0) {
                // Check each child
                for (int i = 0; i < transform.childCount; i++) {
                    // Destroy parcel if destination matches
                    if (transform.GetChild(i).GetComponent<Parcel>().destination.name == collision.transform.parent.name) {
                        Destroy(transform.GetChild(i).gameObject);
                        // Add score for each dropoff
                        GameManager.AddScore();
                    }
                }
            }
        }
    }
}