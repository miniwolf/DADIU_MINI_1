using UnityEngine;
using System.Collections;

/// <summary>
/// Cakes-text interface. Manages methods for adding and 
/// loosing cakes.
/// </summary>
public interface CakesTextInterface {

	/// <summary>
	/// Adds 1 when a cake is taken.
	/// </summary>
	void addCake ();

	/// <summary>
	/// Removes 1 when a cake is thrown.
	/// </summary>
	void removeCake ();
}
