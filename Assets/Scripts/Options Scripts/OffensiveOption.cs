using UnityEngine;

/// <summary>
/// THe offensive option elements
/// </summary>
[CreateAssetMenu(fileName = "New Offensive Option", menuName = "Options/Offensive Option")]
public class OffensiveOption : Option
{
    //Set the damage amount for the option
    [SerializeField] public float damageAmmount;
    [SerializeField] private string _statusName = "Damage";

    /// <summary>
    /// A method that gets the damage ammount outside of class in text related things
    /// </summary>
    /// <returns></returns>
    ///     The damage of the option
    public float GetDamageAmmount()
    {
        //Returns damage ammount
        return damageAmmount;
    }

    /// <summary>
    /// Make the static effect
    /// </summary>
    /// <returns></returns>
    ///     The status version of the option
    public override Status MakeStatusEffect()
    {
        //Instantiate the status effect
        Status attackStatus = new Status();

        //Set the status name
        attackStatus.statusName = _statusName;

        //If the status is instant, set status time to the lowest value
        if (_isInstant == true)
            statusTime = float.MinValue;

        //Does the stat effect the self
        attackStatus.effectsSelf = effectsSelf;

        //Is the effect immediate
        attackStatus.isImmediate = _isInstant;

        //Set the remaining time to status time
        attackStatus.SetTime(statusTime);

        //Set the effects
        attackStatus.healthChange = -damageAmmount * ( 1 + ghostMult);

        //Return the status effect
        return attackStatus;
    }

}
