using UnityEngine;
using System.Collections;

public abstract class PublisherEvent
{

}

public abstract class WeaponEvent : PublisherEvent
{
    public int PlayerIndex;
    public Weapon Weapon;

    public WeaponEvent(int playerIndex, Weapon weapon)
    {
        PlayerIndex = playerIndex;
        Weapon = weapon;
    }
}

public class WeaponChangedEvent : WeaponEvent
{
    public WeaponChangedEvent(int playerIndex, Weapon weapon)
        : base(playerIndex, weapon)
    {
        
    }
}

public class WeaponFiredEvent : WeaponEvent
{
    public WeaponFiredEvent(int playerIndex, Weapon weapon)
        : base(playerIndex, weapon)
    {
        
    }
}