using UnityEngine;
using UnityEngine.UI;

namespace Nutbusterz.Calamitus
{
    public class UI_StaminaBar : MonoBehaviour
    {
        [HideInInspector] public Slider slider; // Reference to the Stamina Bar bossHealthSlider
        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxStamina(float maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
        public void SetCurrentStamina(float currentStamina)
        {
            slider.value = currentStamina;
        }
    }
}