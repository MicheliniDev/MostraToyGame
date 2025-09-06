using UnityEngine;

namespace ToyGame
{
    public interface IFacingFlippable 
    {
        Facings CurrentFacing { get; set; }
        Transform transform { get; }
        bool CanFlip { get; set; }
        void Flip()
        {
            if (!CanFlip) return;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            switch (CurrentFacing)
            {
                case Facings.Right:
                    CurrentFacing = Facings.Left; 
                    break;
                case Facings.Left:
                    CurrentFacing = Facings.Right;
                    break;
            }
        }
    }

    public enum Facings
    {
        None = 0,
        Right = 1,
        Left = -1,
    }
}
