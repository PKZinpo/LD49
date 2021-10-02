using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed;

    private float xMovement = 0f;
    private float yMovement = 0f;
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;

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
            collision.GetComponent<BoxCollider2D>().enabled = false;
            // Check for parcel entry in ParcelSpawner dictionary
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Building").Length; i++) {
                // Remove parcel entry from dictionary in ParcelSpawner
                if (GameObject.FindGameObjectsWithTag("Building")[i] == collision.transform.parent.gameObject) {
                    StopCoroutine(ParcelSpawner.parcelDictionary[i]);
                    ParcelSpawner.parcelDictionary.Remove(i);
                    // Destroy timer bar
                    Destroy(collision.transform.GetChild(0).gameObject);
                    // Add entry to parcelDestination dictionary
                    GameManager.parcelDestinations.Add(collision.GetInstanceID(), collision.GetComponent<Parcel>().destination);
                }
            }
            collision.transform.parent = transform;
        }
        else {
            string collisionName = collision.transform.parent.tag;
            switch (collisionName) {
                case "PostOffice": // When touching drop off at post office
                    if (transform.childCount > 0) {
                        for (int i = 0; i < transform.childCount; i++) {
                            if (transform.GetChild(i).GetComponent<Parcel>().destination.name.Contains(collisionName)) {
                                Destroy(transform.GetChild(i).gameObject);
                                // Add score for each dropoff
                                GameManager.AddScore();
                            }
                        }
                    }
                    break;
                
                case "Building": // When touching drop off at respective building
                    if (transform.childCount > 0) {
                        for (int i = 0; i < transform.childCount; i++) {
                            if (transform.GetChild(i).GetComponent<Parcel>().destination.name.Contains(collisionName)) {
                                Destroy(transform.GetChild(i).gameObject);
                                // Add score for each dropoff
                                GameManager.AddScore();
                            }
                        }
                    }
                    break;
            }
        

        }
    }
}