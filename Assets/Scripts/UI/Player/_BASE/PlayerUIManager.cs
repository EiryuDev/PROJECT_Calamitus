using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerUIManager : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        [Header("GUI DATA")]
        public GameObject crosshairObject;
        public GameObject levelUpInfo;
        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>();    
        }
    }
}
