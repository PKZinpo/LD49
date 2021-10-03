using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelManager : MonoBehaviour {

    public static List<GameObject> parcelList = new List<GameObject>();
    public static int parcelCount;

    public AudioManager audioManager;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parcelIconPrefab;

    private static GameObject parcelIcon;
    private static Transform transformList;

    private void Awake() {
        // Assigning variables
        parcelIcon = parcelIconPrefab;
        transformList = transform;
        parcelCount = 0;
        parcelList.Clear();
    }
    private void Update() {
        if (!PauseMenu.isPaused) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (player.GetComponent<PlayerMovement>().collidingWith != null) {
                    Debug.Log(player.GetComponent<PlayerMovement>().collidingWith);
                    Debug.Log(parcelList[0]);
                    if (player.GetComponent<PlayerMovement>().collidingWith == parcelList[0]) {
                        // Adds to score if it is correct destination
                        GameManager.AddScore();
                        audioManager.Play("CorrectDropOff");
                    }
                    else {
                        // Otherwise penalizes
                        GameManager.RemoveScore();
                        StaminaManager.LoseStamina(0.1f);
                        audioManager.Play("IncorrectDropOff");
                    }
                    // Destroy player child entry and first parcel list entry
                    parcelList.RemoveAt(0);
                    Destroy(player.transform.GetChild(0).gameObject);
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }
    }
    public static void UpdateParcelList(GameObject destination) {
        // Add destination of parcel picked up to list
        parcelList.Add(destination);
        GameObject temp = Instantiate(parcelIcon, transformList, false);
        // Assign parcel sprite to icon
        temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Parcel");
        temp.transform.GetComponentInChildren<Text>().text = destination.name;
    }
}
