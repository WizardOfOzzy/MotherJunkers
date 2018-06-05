public abstract class PublisherEvent
{

}

public abstract class WeaponEvent : PublisherEvent
{
    public EController PlayerIndex;
    public Weapon Weapon;

    public WeaponEvent(EController playerIndex, Weapon weapon)
    {
        PlayerIndex = playerIndex;
        Weapon = weapon;
    }
}

public class WeaponChangedEvent : WeaponEvent
{
    public WeaponChangedEvent(EController playerIndex, Weapon weapon)
        : base(playerIndex, weapon)
    {
        
    }
}

public class WeaponFiredEvent : WeaponEvent
{
    public WeaponFiredEvent(EController playerIndex, Weapon weapon)
        : base(playerIndex, weapon)
    {
        
    }
}

public class DamageTakenEvent : PublisherEvent
{
    public EController PlayerIndex;
    public float CurrentHealth;
    public DamageTakenEvent(EController playerIndex, float currentHealth)
    {
        PlayerIndex = playerIndex;
        CurrentHealth = currentHealth;
    }
}

public class KillVolumeHitEvent : PublisherEvent
{
    public EController PlayerIndex;
    public Vehicle Vehicle;

    public KillVolumeHitEvent(EController playerIndex, Vehicle vehicle)
    {
        PlayerIndex = playerIndex;
        Vehicle = vehicle;
    }
}