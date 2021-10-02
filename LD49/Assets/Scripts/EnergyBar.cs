using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour {

    [SerializeField] private float timer;
    private float timeLeft;
    private float xtimerBarLength;
    private float ytimerBarLength;
    private float ztimerBarLength;

    private void Start() {
        // Get timer bar scale variables
        timeLeft = timer;
        xtimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.x;
        ytimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.y;
        ztimerBarLength = transform.GetChild(0).transform.GetChild(0).transform.localScale.z;
    }

    private void Update() {
        // Change x scale of timer bar according to time left
        timeLeft -= Time.deltaTime;
        float xScale = timeLeft / timer;
        transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector3(xtimerBarLength * xScale, ytimerBarLength, ztimerBarLength);
        if (timeLeft <= 0) {
            transform.GetComponentInParent<Building>().canSpawnBar = true;
            Destroy(gameObject);
        }
    }
}
