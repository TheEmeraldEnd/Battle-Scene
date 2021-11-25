using UnityEngine;

/// <summary>
/// Heal variation of the options
/// </summary>
[CreateAssetMenu(fileName = "New Heal Option", menuName = "Options/Heal Option")]
public class HealOption : Option
{
    [Header("HP related")]
    [SerializeField][Range(0f, 1f)] public float ammountHealed;     //The ammount healed
    [SerializeField] private string _statusName = "Heal status";    //The name of the stat

    /// <summary>
    /// Get the ammount healed for text related scripts
    /// </summary>
    /// <returns></returns>
    ///     The ammount healed
    public float GetAmmountHealed()
    {
        return ammountHealed;
    }

    /// <summary>
    /// Get the status version of the option
    /// </summary>
    /// <returns></returns>
    ///     THe heal stat
    public override Status MakeStatusEffect()
    {
        //Instantiate the status effect
        Status healStatus = new Status();

        //Set the status name
        healStatus.statusName = _statusName;

        //If the status is instant, set status time to the lowest value
        if (_isInstant == true)
            statusTime = float.MinValue;

        //Does the heal option effect the self
        healStatus.effectsSelf = effectsSelf;

        //Is the effect immediate
        healStatus.isImmediate = _isInstant;

        //Set the remaining time to status time
        healStatus.SetTime(statusTime);

        //Set the effects
        healStatus.healthChange = ammountHealed;

        //Return the status effect
        return healStatus;
    }
}
