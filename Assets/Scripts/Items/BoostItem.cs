using UnityEngine;

public class BoostItem : Item 
{
    public const float MIN_BOOST = 0;
    public const float MAX_BOOST = 100;

    [Range(MIN_BOOST, MAX_BOOST)]
    public float BoostAmount;
}
