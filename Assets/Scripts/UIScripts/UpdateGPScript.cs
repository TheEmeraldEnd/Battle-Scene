using UnityEngine;

/// <summary>
/// Update the GP of the entity's bar
/// </summary>
public class UpdateGPScript : MonoBehaviour
{
    //Instantiated variables
    [SerializeField] private EntityData _entityData;        //The entity
    [SerializeField] private string _entityTag;             //The entity's tag

    [SerializeField] private Bar _gpBar;                    //The gp bar game object

    /// <summary>
    /// The update before the first update
    /// </summary>
    private void Awake()
    {
        //Find the entity data component
        _entityData = GameObject.FindGameObjectWithTag(_entityTag).GetComponent<EntityData>();

        //Find the bar component
        _gpBar = this.gameObject.GetComponent<Bar>();
    }

    /// <summary>
    /// Update every update
    /// </summary>
    private void Update()
    {
        //The gp bar percentage update
        _gpBar.persent = _entityData.Gp * 100f;
    }
}
