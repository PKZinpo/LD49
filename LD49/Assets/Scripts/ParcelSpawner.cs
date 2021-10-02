using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelSpawner : MonoBehaviour {

    public static Dictionary<int, Coroutine> parcelDictionary = new Dictionary<int, Coroutine>();

    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private GameObject parcelPrefab;
    private bool spawnIsRunning = false;
    

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
            // If building does not have parcel and is ready to spawn, spawn parcel; otherwise, loop again
            if (GameObject.FindGameObjectsWithTag("Building")[randomBuilding].transform.childCount < 2 && GameObject.FindGameObjectsWithTag("Building")[randomBuilding].GetComponent<Building>().GetCanSpawn()) {
                GameObject parcel = Instantiate(parcelPrefab, GameObject.FindGameObjectsWithTag("Building")[randomBuilding].transform, false);
                parcel.transform.position = GameObject.FindGameObjectsWithTag("Building")[randomBuilding].transform.GetChild(0).transform.position;
                // Add building ID and coroutine ID to dictionary for later access
                StartCoroutine(GameObject.FindGameObjectsWithTag("Building")[randomBuilding].GetComponent<Building>().CanSpawnParcel());
                Coroutine parcelCoroutine = StartCoroutine(parcel.GetComponent<Parcel>().ParcelTimer());
                parcelDictionary.Add(randomBuilding, parcelCoroutine);
                spawnIsRunning = false;
            }
        }
        
    }
}
