using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_UTILITY_DESTROY_FX : MonoBehaviour
    {
        void Update()
        {
            if (!GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}