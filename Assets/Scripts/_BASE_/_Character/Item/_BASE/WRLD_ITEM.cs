using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_ITEM : ScriptableObject
    {
        [Header("INFO DATA")]
        [Tooltip("Name of the item")]
        public string name;
        [Tooltip("Icon sprite of the item")]
        public Sprite icon;
        [Tooltip("Icon lore of the item")]
        [TextArea]public string lore;
    }
}
