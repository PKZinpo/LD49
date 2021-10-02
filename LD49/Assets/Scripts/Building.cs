using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    [SerializeField] private float spawnTime = 6f;
    private bool canSpawn = true;

    public IEnumerator CanSpawnParcel() {
        // Only allow parcel spawn after 6 seconds
        canSpawn = false;
        yield return new WaitForSeconds(spawnTime);
        canSpawn = true;
    }
    public bool GetCanSpawn() {
        return canSpawn;
    }
}
