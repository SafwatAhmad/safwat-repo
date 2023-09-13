namespace BattleShip.WeaponSystem
{
    internal interface IInteract
    {
        internal void Interact();
    }

    internal interface ITakeHit
    {
        internal void TakeHit(int damage);
    }

    internal interface ITrigger
    {
        internal void Trigger();
    }
}