using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CharacterComponents finds and stores references to all of the components of the player character.
/// This script must be updated whenever a new component is added to the player character.
/// </summary>
/// <author>
/// David Patch
/// </author>
public class CharacterComponents : MonoBehaviour
{
    #region Fields
    private bool componentsFound = false;
    public static CharacterComponents instance;
    public FPController controller;
    public HeldPhoto heldPhoto;
    public Album album;
    public Inventory inventory;
    #endregion

    private void Awake()
    {
        FindComponents();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Helper Methods
    /// <summary>
    /// Finds all character components and sets the references to them if not already found.
    /// </summary>
    private void FindComponents()
    {
        if(!componentsFound)
        {
            if(controller == null)
            {
                controller = this.GetComponent<FPController>();
            }
            if (heldPhoto == null)
            {
                heldPhoto = this.GetComponent<HeldPhoto>();
            }
            if (album == null)
            {
                album = this.GetComponent<Album>();
            }
            if(inventory == null)
            {
                inventory = this.GetComponent<Inventory>();
            }

            componentsFound = true;
        }
    }
    #endregion
}
