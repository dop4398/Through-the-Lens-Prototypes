using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Globals is a singleton for keeping references of global objects.
/// </summary>
/// <author>
/// Alfie Luo and David Patch
/// </author>
public class Globals : MonoBehaviour
{
    #region Fields
    private static Globals instance = null;
	public static Globals Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject instanceObject = GameObject.FindGameObjectWithTag("Globals");
				if (instanceObject != null)
				{
					instance = instanceObject.GetComponent<Globals>();
				}
			}
			return instance;
		}
	}
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Helper Methods
    #endregion
}
