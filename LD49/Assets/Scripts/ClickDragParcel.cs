using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickDragParcel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [HideInInspector] public int position;
    [HideInInspector] public GameObject temp = null;
    private Transform returnTo = null;
    private RectTransform rectTransform;
    

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        position = transform.GetSiblingIndex();
        // Create a temp object to hold parcel position in UI
        temp = new GameObject();
        temp.transform.SetParent(transform.parent);
        // Copy parcel icon data to temp
        LayoutElement iconSize = temp.AddComponent<LayoutElement>();
        iconSize.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        iconSize.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        iconSize.flexibleWidth = 0;
        iconSize.flexibleHeight = 0;
        
        temp.transform.SetSiblingIndex(transform.GetSiblingIndex());
        returnTo = transform.parent;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        // Position of icon follows mouse
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        // Returns to inventory if not at dropoff
        transform.SetParent(returnTo);
        transform.SetSiblingIndex(temp.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(temp);
        temp = null;
    }
}
