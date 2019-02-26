namespace Assets.Scripts.Actions
{
    public interface IHealth
    {
        int TotalHealth { get; }
        int CurrentHealth { get; }
        void Damage(int damage);
        void Heal(int healthPoints);
        void Restore();
        void Die();
        void IncreaseTotal(int total, bool updateHealth);
    }
}