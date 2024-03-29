using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelSpawner : MonoBehaviour {

    public static Dictionary<Vector3, Coroutine> parcelDictionary = new Dictionary<Vector3, Coroutine>();

    [SerializeField] private float spawnTimePO;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject parcelPrefab;
    private bool spawnIsRunning = false;

    private void Start() {
        StartCoroutine(SpawnParcelPostOffice());
        parcelDictionary.Clear();
    }
    private void Update() {
        if (!spawnIsRunning) {
            StartCoroutine(SpawnParcel());
        }
    }
    private IEnumerator SpawnParcel() {
        // Spawn parcel
        spawnIsRunning = true;
        yield return new WaitForSeconds(spawnTime);
        while (spawnIsRunning) {
            // Choose random building
            int randomBuilding = Random.Range(0, GameObject.FindGameObjectsWithTag("Building").Length);
            bool ready = true;
            // Check if current building has parcel
            foreach (GameObject parcel in GameObject.FindGameObjectsWithTag("Parcel")) {
                if (parcel.transform.parent.gameObject == GameObject.FindGameObjectsWithTag("Building")[randomBuilding]) {
                    ready = false;
                }
            }
            // If building does not have parcel and is ready to spawn, spawn parcel; otherwise, loop again
            if (ready && GameObject.FindGameObjectsWithTag("Building")[randomBuilding].GetComponent<Building>().GetCanSpawn()) {
                GameObject parcel = Instantiate(parcelPrefab, GameObject.FindGameObjectsWithTag("Building")[randomBuilding].transform, false);
                parcel.transform.position = GameObject.FindGameObjectsWithTag("Building")[randomBuilding].transform.GetChild(0).transform.position;
                // Add building object and coroutine ID to dictionary for later access
                StartCoroutine(GameObject.FindGameObjectsWithTag("Building")[randomBuilding].GetComponent<Building>().CanSpawnParcel());
                Coroutine parcelCoroutine = StartCoroutine(parcel.GetComponent<Parcel>().ParcelTimer());
                parcelDictionary.Add(parcel.transform.position, parcelCoroutine);
                break;
            }
        }
        spawnIsRunning = false;
    }
    private IEnumerator SpawnParcelPostOffice() {
        // Spawns parcel at PO
        yield return new WaitForSeconds(spawnTimePO);
        GameObject parcel = Instantiate(parcelPrefab, GameObject.FindGameObjectWithTag("PostOffice").transform, false);
        parcel.transform.position = GameObject.FindGameObjectWithTag("PostOffice").transform.GetChild(0).transform.position;
        Coroutine parcelCoroutine = StartCoroutine(parcel.GetComponent<Parcel>().ParcelTimer());
        parcelDictionary.Add(parcel.transform.position, parcelCoroutine);
        StartCoroutine(SpawnParcelPostOffice());
    }
}
