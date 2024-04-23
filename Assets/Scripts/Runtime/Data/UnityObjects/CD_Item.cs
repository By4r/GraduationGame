using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Item", menuName = "GraduationGame/CD_Item", order = 1)]
    public class CD_Item : ScriptableObject
    {
        public ItemData ItemData;
    }
}