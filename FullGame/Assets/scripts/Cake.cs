using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface Cake  {
	/// <summary>
	/// Tells the player if she may throw.
	/// </summary>
	/// <returns><c>true</c>, if allowed to throw, <c>false</c> otherwise.</returns>
	bool MayThrow();

	/// <summary>
	/// Sets the cakethrowing active.
	/// </summary>
	void SetActive();
}
