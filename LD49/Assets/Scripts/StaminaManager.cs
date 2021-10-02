using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour {

    [SerializeField] private Image staminaBar;
    [SerializeField] private GameObject player;
    [SerializeField] private float multiplier;
    [SerializeField] private float maxStamina;
    private static float staminaLeft;
    private static float stamina;

    private void Start() {
        staminaLeft = maxStamina;
        stamina = maxStamina;
    }

    private void Update() {
        if (player.transform.childCount > 0) {
            if (staminaLeft > 0) {
                staminaLeft -= Time.deltaTime * multiplier * player.transform.childCount;
            }
            else {
                Application.Quit();
            }
        }
        staminaBar.fillAmount = staminaLeft / maxStamina;
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
