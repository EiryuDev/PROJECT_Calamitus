using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class CharacterCombatManager : MonoBehaviour
    {
        [HideInInspector] public CharacterManager character;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
    }
}