using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        [HideInInspector] public CharacterManager character; // Reference to the Character Manager script

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
    }
}