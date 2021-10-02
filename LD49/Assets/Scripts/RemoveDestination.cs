using UnityEngine;

public class RemoveDestination : MonoBehaviour {

    [SerializeField] private float timeDestroy;

    void Start() {
        Invoke("DestroyDestination", timeDestroy);
    }

    private void DestroyDestination() {
        Destroy(gameObject);
    }
}
