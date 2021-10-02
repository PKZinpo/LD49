using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelManager : MonoBehaviour {

    public static List<GameObject> parcelList = new List<GameObject>();

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parcelIconPrefab;

    private static GameObject parcelIcon;
    private static Transform transformList;

    private void Awake() {
        // Assigning variables
        parcelIcon = parcelIconPrefab;
        transformList = transform;
    }
    public static void UpdateParcelList(GameObject destination) {
        // Add destination of parcel picked up to list
        parcelList.Add(destination);
        GameObject temp = Instantiate(parcelIcon, transformList, false);
        if (destination.name == "PostOffice") { // Assign normal parcel sprite to icon
            temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Parcel Icon (Placeholder)");
        }
        else { // Assign PO parcel sprite to icon
            temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("PO Parcel Icon (Placeholder)");
        }
    }
}
