using System.Collections;
using UnityEngine;

public class Parcel : MonoBehaviour {

    public GameObject destination;

    [SerializeField] private float timer;
    private float timeLeft;
    private float xtimerBarLength;
    private float ytimerBarLength;
    private float ztimerBarLength;

    private void Start() {
        // Get timer bar scale variables
        timeLeft = timer;
        xtimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.x;
        ytimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.y;
        ztimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.z;
        // Set destination
        ParcelManager.parcelCount++;
        if (transform.parent.name.Contains("Building")) {
            // For every 5th parcel, destination is random
            if (ParcelManager.parcelCount % 5 == 0) {
                bool spawnIsRunning = true;
                while (spawnIsRunning) {
                    // Choose random building
                    int randomBuilding = Random.Range(0, GameObject.FindGameObjectsWithTag("Building").Length);
                    destination = GameObject.FindGameObjectsWithTag("Building")[randomBuilding];
                    if (!destination.name.Contains("PostOffice")) {
                        spawnIsRunning = false;
                    }
                }
            }
            else {
                destination = GameObject.FindGameObjectWithTag("PostOffice");
            }
        }
        else if (transform.parent.name.Contains("PostOffice")) {
            bool spawnIsRunning = true;
            while (spawnIsRunning) {
                // Choose random building
                int randomBuilding = Random.Range(0, GameObject.FindGameObjectsWithTag("Building").Length);
                destination = GameObject.FindGameObjectsWithTag("Building")[randomBuilding];
                if (!destination.name.Contains("PostOffice")) {
                    spawnIsRunning = false;
                }
            }
        }
    }

    private void Update() {
        // If parent is not player
        if (transform.parent.gameObject != GameObject.FindGameObjectWithTag("Player")) {
            // Change x scale of timer bar according to time left
            timeLeft -= Time.deltaTime;
            float xScale = timeLeft / timer;
            transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3(xtimerBarLength * xScale, ytimerBarLength, ztimerBarLength);
        }
    }

    public IEnumerator ParcelTimer() {
        // Destroys parcel if not picked up
        yield return new WaitForSeconds(timer);
        ParcelSpawner.parcelDictionary.Remove(transform.position);
        Destroy(transform.gameObject);
        StaminaManager.LoseStamina(0.05f);
    }



}
