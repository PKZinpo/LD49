using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private static int score = 0;
    public static Dictionary<int, GameObject> parcelDestinations = new Dictionary<int, GameObject>();

    public static void AddScore() { // Score counter
        score++;
        Debug.Log(score);
    }
}
