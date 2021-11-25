using UnityEngine;

/// <summary>
/// The base class for all options
/// </summary>
public abstract class Option : ScriptableObject
{
    #region instantiated variables
    //Option type
    [SerializeField] public bool isOffensive;       //If derived class is offensive
    [SerializeField] public bool isDefensive;       //If derived class is defensive
    [SerializeField] public bool isHeal;            //If derived class heals

    //Ghost stuff
    [SerializeField] public float ghostMult;        //Ghost multiplier to add to stat

    //Effects self
    [SerializeField] public bool effectsSelf;       //If thing effects self

    //GP related
    [SerializeField] [Range(0f, 1f)]
    protected float _gpCost;                        //The cost of the option

    //Time related
    [SerializeField] [Tooltip("Used to find the cooldown time")]
    protected float _coolDownTime;                  //The cooldown time for casting the option

    [SerializeField] [Tooltip("How long the status effect happens")] 
    public float statusTime;                        //How long the option lasts

    [SerializeField] public bool _isInstant;        //If the option happens in one update
    #endregion

    /// <summary>
    /// Get the gp cost for the action
    /// </summary>
    public float GPCost
    {
        get
        {
            return _gpCost;
        }
    }

    /// <summary>
    /// Get the cooldown time
    /// </summary>
    public float CoolDownTime
    {
        get
        {
            return _coolDownTime;
        }
        set
        {
            _coolDownTime = value;
        }
    }

    /// <summary>
    /// Allows the use of enumerators to make status effects
    /// </summary>
    /// <returns></returns>
    ///     The status effects with all of the effects
    public abstract Status MakeStatusEffect();
}
