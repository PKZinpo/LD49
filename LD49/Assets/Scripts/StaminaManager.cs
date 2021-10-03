using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour {

    [SerializeField] private Image staminaBar;
    [SerializeField] private GameObject player;
    [SerializeField] private float multiplier;
    [SerializeField] private float maxStamina;
    private static float stamina;
    
    public static float staminaLeft;
    public static bool isOver = false;

    private void Start() {
        staminaLeft = maxStamina;
        stamina = maxStamina;
        isOver = false;
    }

    private void Update() {
        if (player.transform.childCount > 0) {
            if (staminaLeft > 0) {
                staminaLeft -= Time.deltaTime * multiplier * player.transform.childCount;
            }
        }
        staminaBar.fillAmount = staminaLeft / maxStamina;
        if (staminaLeft <= 0) {
            isOver = true;
        }
    }

    public static void AddStamina() {
        float bonusStamina = 0.2f * stamina;
        staminaLeft += bonusStamina;
        staminaLeft = Mathf.Min(staminaLeft, stamina);
    }
    public static void LoseStamina(float amountLost) {
        float lostStamina = amountLost * stamina;
        staminaLeft -= lostStamina;
        staminaLeft = Mathf.Max(0, staminaLeft);
    }
}
