using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    public bool canSpawnBar = true;

    [SerializeField] private GameObject energyBar;
    [SerializeField] private float spawnTime;
    [SerializeField] private float energyBarSpawnTime;
    [SerializeField] private float chance;
    private bool canSpawn = true;

    private void Awake() {
        if (name.Contains("Stamina")) {
            StartCoroutine(SpawnEnergyBar());
        }
    }
    private void Update() {
        if (name.Contains("Stamina")) {
            if (canSpawnBar) {
                StartCoroutine(SpawnEnergyBar());
                canSpawnBar = false;
            }
        }
    }
    public IEnumerator CanSpawnParcel() {
        // Only allow parcel spawn after 6 seconds
        canSpawn = false;
        yield return new WaitForSeconds(spawnTime);
        canSpawn = true;
    }
    public bool GetCanSpawn() {
        return canSpawn;
    }
    public IEnumerator SpawnEnergyBar() {
        yield return new WaitForSeconds(energyBarSpawnTime);
        float randomVal = Random.Range(0f, 100f);
        // If randomVal hits the 30%, spawn energy bar
        if (chance >= randomVal) {
            GameObject bar = Instantiate(energyBar, transform, false);
            bar.transform.position = transform.GetChild(2).transform.position;
        }
        else {
            canSpawnBar = true;
        }
    }
}
