using UnityEngine;

public class Parcel : MonoBehaviour {
    
    public void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("COLLISION");
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")) {
            Destroy(gameObject);
        }
    }
}
