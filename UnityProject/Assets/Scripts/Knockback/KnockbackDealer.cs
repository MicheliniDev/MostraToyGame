using UnityEngine;

namespace ToyGame
{
    public class KnockbackDealer : MonoBehaviour
    {
        public Facings ownerFacing;
        public float KnockbackAmount;
        public virtual void OnEnable()
        {
            ownerFacing = GetComponentInParent<IFacingFlippable>().CurrentFacing;
        }
    }
}
