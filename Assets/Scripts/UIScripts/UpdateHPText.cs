using UnityEngine;

/// <summary>
/// Set the hp bar to match the number
/// </summary>
public class UpdateHPText : MonoBehaviour
{
    //Instantiated variables
    [SerializeField] private EntityData _entityData;    //The entity data to get hp
    [SerializeField] private string _entityTag;         //The entity tag

    [SerializeField] private Bar _hpBar;                //The hp game object

    /// <summary>
    /// The update before the first update
    /// </summary>
    private void Awake()
    {
        //Find the enitity
        _entityData = GameObject.FindGameObjectWithTag(_entityTag).GetComponent<EntityData>();

        //Find and set the hp bar
        _hpBar = this.gameObject.GetComponent<Bar>();
    }

    /// <summary>
    /// Update the hp bar
    /// </summary>
    private void Update()
    {
        //Update the hp bar
        _hpBar.persent = _entityData.Hp * 100f;
    }
}
