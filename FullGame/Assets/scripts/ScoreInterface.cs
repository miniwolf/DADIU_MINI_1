using UnityEngine;
using System.Collections;

/// <summary>
/// Score interface. Manages all the methods for adding score
/// (when taking laundry, cakes or hitting the troll).
/// </summary>
public interface ScoreInterface {
	/// <summary>
	/// Adds the laundry score.
	/// </summary>
	void AddLaundryScore ();

	/// <summary>
	/// Adds score when a cake is taken.
	/// </summary>
	void AddCakesScore ();

	/// <summary>
	/// Adds score when the troll is hit.
	/// </summary>
	void AddTrollScore();
}
