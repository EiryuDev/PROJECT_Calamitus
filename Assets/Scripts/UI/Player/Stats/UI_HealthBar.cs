using UnityEngine;
using UnityEngine.UI;

namespace Nutbusterz.Calamitus
{
    public class UI_HealthBar : MonoBehaviour
    {
        [HideInInspector] public Slider healthSlider; // Reference to health bar's bossHealthSlider

        private void Awake() 
        {
            healthSlider = GetComponent<Slider>();    
        }
        
        public void SetMaxHealth(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
        public void SetCurrentHealth(int currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }
}
