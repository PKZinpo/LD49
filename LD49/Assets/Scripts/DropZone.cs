using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {

    [SerializeField] private GameObject player;

    public void OnDrop(PointerEventData eventData) {
        // When dropped in area, remove entry from list
        ClickDragParcel draggedParcel = eventData.pointerDrag.GetComponent<ClickDragParcel>();
        if (player.GetComponent<PlayerMovement>().collidingWith != null) {
            if (player.GetComponent<PlayerMovement>().collidingWith == ParcelManager.parcelList[draggedParcel.position]) {
                // Adds to score if it is correct destination
                GameManager.AddScore();
            }
            else {
                // Otherwise penalizes
                GameManager.RemoveScore();
                StaminaManager.LoseStamina(0.1f);
            }
            ParcelManager.parcelList.RemoveAt(draggedParcel.position);
            // Destroy temp object in UI, player child entry, and object itself
            Destroy(player.transform.GetChild(draggedParcel.position).gameObject);
            Destroy(draggedParcel.temp);
            Destroy(eventData.pointerDrag);
        }
    }
}
