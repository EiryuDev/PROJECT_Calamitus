using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_UTILITY_DISABLE_OBJECT : MonoBehaviour
    {
        public GameObject objectToDisable;
        public void UseDisableObject()
        {
            objectToDisable.SetActive(false);   
        }
    }
}
