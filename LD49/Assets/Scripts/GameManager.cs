using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject scoreText;

    public static int score = 0;
    public static Dictionary<int, GameObject> parcelDestinations = new Dictionary<int, GameObject>();

    private void Awake() {
        score = 0;
    }
    private void Update() {
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    public static void AddScore() { // Adds to score counter
        score++;
        Debug.Log(score);
    }
    public static void RemoveScore() { // Removes from score counter
        score--;
        Debug.Log(score);
    }
}
