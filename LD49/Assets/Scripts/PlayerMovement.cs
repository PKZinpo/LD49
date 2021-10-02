using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject collidingWith;

    [SerializeField] private float movementSpeed;
    [SerializeField] private int canHold;

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

        // Sets child parcel transform positions to same position as player
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
                // Adds parcel entry to UI list
                ParcelManager.UpdateParcelList(collision.GetComponent<Parcel>().destination);
                // Remove parcel entry from dictionary in ParcelSpawner
                StopCoroutine(ParcelSpawner.parcelDictionary[collision.transform.position]);
                ParcelSpawner.parcelDictionary.Remove(collision.transform.position);
                // Destroy timer bar
                Destroy(collision.transform.GetChild(0).gameObject);

                collision.transform.parent = transform;
            }
        }
        else if (collision.name.Contains("EnergyBar")) {
            //StartCoroutine(collision.GetComponentInParent<Building>().SpawnEnergyBar());
            collision.GetComponentInParent<Building>().canSpawnBar = true;
            Destroy(collision.gameObject);
            StaminaManager.AddStamina();
        }
        else {
            // Writes game object that collision happens with
            collidingWith = collision.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Resets variable for object collision
        collidingWith = null;
    }
}