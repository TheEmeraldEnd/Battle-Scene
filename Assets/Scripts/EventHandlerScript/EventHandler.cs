using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The main event handler
/// </summary>
public class EventHandler : MonoBehaviour
{
    #region entity related variables
    [Header("Entity data")]
    //Variables relating to entities
    [SerializeField] private GameObject _player;        //The player game object
    [SerializeField] private GameObject _enemy;         //The enemy game object

    [SerializeField] private EntityData _playerData;    //The player data
    [SerializeField] private EntityData _enemyData;     //The enemy data

    [SerializeField] private PlayerOptionInput _playerInput;    //The player option picker
    [SerializeField] private EnemyOptionInput _enemyInput;      //The enemy option picker

    //Tags for the player and enemy
    [SerializeField] [Tooltip("Tag of the player")] 
    private string _playerTag;                          //Tag of the player game object

    [SerializeField] [Tooltip("Tag of the enemy")] 
    private string _enemyTag;                           //Tag of the enemy game object

    protected Status newStatus = new Status();          //Variable containing the new stat changes

    [Header("Numbers Related")]
    //Update gp per second
    [Tooltip("GP regen per second")]
    [Range(0f, 1f)]
    [SerializeField] private float _gpRegen;            //The gp regen per round

    [Header("Win conditions")]
    [Tooltip("The tolerance range for a win")]
    [SerializeField][Range(0f, 0.5f)] private float _toleranceRange;    //The tolerance range for the win

    [Tooltip("Scene loaded if player wins")]
    [SerializeField] private string _winScreen;         //String to go to the win screen

    [Tooltip("Scene loaded if the player looses")]
    [SerializeField] private string _looseScreen;       //The loose screen string
    #endregion

    #region textbox
    [Header("Textbox options")]
    //Disblay options
    [SerializeField] private OptionText _optionText;        //The option text game object
    [SerializeField] private string _optionTag;             //The option game object tag

    #endregion

    #region time score variable
    [Header("Time Score")]
    [SerializeField] private TimeHold _time;
    #endregion

    /// <summary>
    /// Before the first update
    /// </summary>
    void Awake()
    {
        #region entity setup
        //Set game objects to variables
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _enemy = GameObject.FindGameObjectWithTag(_enemyTag);

        //Get data into variables
        _playerData = _player.GetComponent<EntityData>();
        _enemyData = _enemy.GetComponent<EntityData>();

        //Get data for the ability for the player to make a choice
        _playerInput = _player.GetComponent<PlayerOptionInput>();
        _enemyInput = _enemy.GetComponent<EnemyOptionInput>();

        //Set the hp and gp to max
        _playerData.ChangeHp(1f);
        _enemyData.ChangeHp(1f);

        _playerData.ChangeGp(1f);
        _enemyData.ChangeGp(1f);
        #endregion

        #region text Setups
        //Instantiate option choice
        _optionText = GameObject.FindGameObjectWithTag(_optionTag)
            .GetComponent<OptionText>();


        #endregion

        #region time score setup
        _time.timeScore = 0f;
        #endregion
    }

    /// <summary>
    /// What should happen every update
    /// </summary>
    private void Update()
    {
        #region status
        ///User things
        //Find input
        newStatus = _playerInput.FindOptionChoice(Time.deltaTime);

        //Set status effect to the status effect lists
        if (newStatus.statusName != "")
            SetNewStatus(newStatus, _playerData, _enemyData);

        //Use status effects in user list
        StatusUpdate(_playerData);

        ///Enemy things
        //Find enemy option
        newStatus = _enemyInput.FindOptionChoice(Time.deltaTime);

        //Set the status into appropriate status list
        if (newStatus.statusName != "")
            SetNewStatus(newStatus, _enemyData, _playerData);

        //Use status effect in enemy list
        StatusUpdate(_enemyData);
        #endregion

        #region update gp
        //Update entities gp
        _playerData.ChangeGp(_gpRegen * Time.deltaTime);
        _enemyData.ChangeGp(_gpRegen * Time.deltaTime);
        #endregion

        #region check win
        //Check who wins
        CheckPlayerWin(_playerData, _enemyData);
        #endregion

        #region Update texts
        //Cooldown update for text
        _optionText.UpdateCooldown(_playerInput.inputCooldown);

        //Update the text directly
        _optionText.UpdateText(0.01f);
        #endregion

        #region update time score
        //Update time score
        _time.timeScore += Time.deltaTime;
        #endregion
    }

    #region stats methods
    /// <summary>
    /// Update all active stats
    /// </summary>
    /// <param name="entity"></param>
    ///     The entity that the active effects are being updated
    private void StatusUpdate(EntityData entity)
    {
        //Initialize 
        float hpDamage = 0f;
        float shield = 0f;
        float hpHeal = 0f;

        //Set up the effects
        foreach(Status stat in entity.activeEffects)
        {
            //Change Health
            if (stat.healthChange < 0f)
            {
                hpDamage += CheckIfImmediate(stat, stat.healthChange);
            }
            else if( stat.healthChange > 0f)
            {
                hpHeal += CheckIfImmediate(stat, stat.healthChange);
            }

            
            shield += stat.blockPercentageChange;

            stat.SubtractTime(Time.deltaTime);
        }

        entity.activeEffects = RemoveInactiveStats(entity.activeEffects);
        entity.activeEffects.TrimExcess();

        //Set up shield
        shield = SetToRange(shield, 0f, 1f);

        //Heal
        entity.ChangeHp(hpHeal);

        //Damage health
        entity.ChangeHp(hpDamage * (1f - shield));
    }

    /// <summary>
    /// Checks if the stat is immediate (if it lasts for one update)
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="statEffect"></param>
    /// <returns></returns>
    private float CheckIfImmediate(Status stat, float statEffect)
    {
        //Spreads out the stat effect if not immediate
        if (stat.isImmediate == false)
        {
            return (statEffect / stat.maxTime) * Time.deltaTime;
        }
        //return raw effect
        else
        {
            return statEffect;
        }
    }

    /// <summary>
    /// Sets number inbetween 2 numbers
    /// </summary>
    /// <param name="attribute"></param>
    ///     The number between the range
    /// <param name="min"></param>
    ///     The min the number can be
    /// <param name="max"></param>
    ///     The max the number can be
    /// <returns></returns>
    ///     The number within the range.
    private float SetToRange(float attribute, float min, float max)
    {
        //Set value to min if less than min
        if (attribute < min)
        {
            return min;
        }

        //Set value to max if above max
        else if (attribute > max)
        {
            return max;
        }

        //Return raw value
        else
        {
            return attribute;
        }
    }

    /// <summary>
    /// Sets a stat to either opposing entity active effect list or self list
    /// </summary>
    /// <param name="optionStat"></param>
    ///     The stat
    /// <param name="selfEntity"></param>
    ///     Entity casting it
    /// <param name="oppEntity"></param>
    ///     Not entity casting it
    private void SetNewStatus(Status optionStat, EntityData selfEntity, EntityData oppEntity)
    {
        //Apply stat to self
        if(optionStat.effectsSelf == true)
        {
            selfEntity.activeEffects.Add(optionStat);
        }
        
        //Apply stat to enemy
        else
        {
            oppEntity.activeEffects.Add(optionStat);
        }
    }

    /// <summary>
    /// Removes all inactive effects
    /// </summary>
    /// <param name="oldStats"></param>
    ///     Old stats
    /// <returns></returns>
    ///     The new active stats
    private List<Status> RemoveInactiveStats(List<Status> oldStats)
    {
        //Where the new active effects go
        List<Status> newStats = new List<Status>();

        //Filter out the inactive effects
        foreach (Status stat in oldStats)
        {

            //add active effect to active stat list
            if (stat.isNull == false)
            {
                newStats.Add(stat);
            }
        }

        //Return the new active effects
        return newStats;
    }

    #endregion

    #region win methods
    /// <summary>
    /// Checks who wins
    /// </summary>
    /// <param name="player"></param>
    ///     The player entity
    /// <param name="enemy"></param>
    ///     The enemy entity
    private void CheckPlayerWin(EntityData player, EntityData enemy)
    {
        //Check if player wins
        if (enemy.Hp < _toleranceRange && enemy.Gp < _toleranceRange)
        {
            SceneManager.LoadScene(_winScreen);
        }
        //Check if enemy wins
        else if (player.Hp < _toleranceRange && player.Gp < _toleranceRange)
        {
            SceneManager.LoadScene(_looseScreen);
        }
    }

    #endregion
}
