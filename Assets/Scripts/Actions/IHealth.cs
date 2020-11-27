namespace Assets.Scripts.Actions
{
    public interface IHealth
    {
        float TotalHealth { get; }
        float CurrentHealth { get; }
        void Damage(float damage);
        void Heal(float healthPoints);
        void Restore();
        void Die();
        void IncreaseTotal(float total, bool updateHealth);
        float HealthPercentage { get; }
    }
}