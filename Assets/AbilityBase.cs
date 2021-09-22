using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class AbilityTable
{
    public static Dictionary<AbilityType, AbilityBase> abilityMap = new Dictionary<AbilityType, AbilityBase>()
        {
            {AbilityType.Magnetic, MagneticAbility.Instance}, 
            {AbilityType.Dash, DashAbility.Instance}
        };

    public static AbilityBase GetAblity(this AbilityType abilityType)
        => abilityMap[abilityType];
}

public abstract class AbilityBase : MonoBehaviour
{
    public virtual AbilityType GetAbilityType()
    {
        return AbilityType.None;
    }

    public abstract void Activate();
    public abstract void Deactivate();
}
