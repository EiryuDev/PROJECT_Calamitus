using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;

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