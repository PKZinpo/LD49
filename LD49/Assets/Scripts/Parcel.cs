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
        if (transform.parent.name.Contains("Building")) {
            destination = GameObject.FindGameObjectWithTag("PostOffice");
        }
        else if (transform.parent.name.Contains("PostOffice")) {
            int randomBuilding = Random.Range(0, GameObject.FindGameObjectsWithTag("Building").Length);
            destination = GameObject.FindGameObjectsWithTag("Building")[randomBuilding];
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
        Debug.Log("ParcelDestroy");
        Destroy(transform.gameObject);
        StaminaManager.LoseStamina(0.05f);
    }



}
