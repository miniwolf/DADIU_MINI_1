using UnityEngine;
using System.Collections;

public class GridPlane : MonoBehaviour, GridInterface {
	public int gridSizeX = 10, gridSizeY = 10;
	public float yPlacementOfGO = 0;
	bool isGridInit = false;

	[HideInInspector]
	public GridTile[][] theGrid;

	void Awake(){
		if ( !isGridInit ) {
			theGrid = new GridTile[gridSizeX][];
			for ( int i = 0; i < theGrid.Length; i++ ) {
				theGrid[i] = new GridTile[gridSizeY];
			}
			for ( int i = 0; i < theGrid.Length; i++ ) {
				for ( int u = 0; u < theGrid[i].Length; u++ ) {
					theGrid[i][u] = new GridTile();
				}
			}
		}

		if ( gameObject.layer != LayerMask.NameToLayer(Constants.GROUNDLAYER) ) {
			Debug.LogError("The playing field is missing a layer called \"GroundLayer\", add it to the level space");
		}
	}

	// Use this for initialization
	void Start () {
		if ( !isGridInit ) {
			InitGrid();
			AkSoundEngine.PostEvent("forestSoundscape", GameObject.FindGameObjectWithTag(Constants.SOUND));
		}
	}

	/// <summary>
	/// Initializes the grid, is to be called once, in awake.
	/// </summary>
	void InitGrid() {
		Vector2 initPos = new Vector2((gameObject.transform.position.x - gameObject.transform.localScale.x / 2), gameObject.transform.position.y - gameObject.transform.localScale.y / 2);
		theGrid[0][0].pos = initPos;
		theGrid[0][0].rect.size = new Vector2(gameObject.transform.localScale.x / gridSizeX, gameObject.transform.localScale.y / gridSizeY);
		for(int i = 0; i < theGrid.Length; i++) {
			theGrid[i][0].pos = new Vector2(initPos.x + (gameObject.transform.localScale.x / gridSizeX) * i, initPos.y);
			for(int u = 0; u < theGrid[i].Length; u++) {
				theGrid[i][u].pos = new Vector2(theGrid[i][0].pos.x, initPos.y + (gameObject.transform.localScale.y / gridSizeY) * u);
				theGrid[i][u].rect.center = theGrid[i][u].pos;
				theGrid[i][u].rect.size = new Vector2(gameObject.transform.localScale.x / gridSizeX, gameObject.transform.localScale.y / gridSizeY);
				theGrid[i][u].CheckIfObstructed();
			}
		}
		theGrid[0][0].pos = new Vector2(initPos.x + gameObject.transform.localScale.x / gridSizeX / 2, initPos.y + gameObject.transform.localScale.y / gridSizeY / 2);
		theGrid[0][0].rect.center = theGrid[0][0].pos;
	}

	/// <summary>
	/// Converts world coordinates to grid coordinates.
	/// </summary>
	/// <returns>The grid position in Vector2.</returns>
	/// <param name="worldPos">World coordinates.</param>
	public Vector2 WorldtoGridPos(Vector3 worldPos) {

		/*float x = (gameObject.transform.localScale.x / 2 - worldPos.x) / gameObject.transform.localScale.x / gridSizeX;
		float y = (gameObject.transform.localScale.y/2 - worldPos.z)/gameObject.transform.localScale.y/gridSizeY;
		*/
		Vector3 locScale = transform.localScale;
		float x = ComputeGridPos(worldPos.x, locScale.x, gridSizeX);
		float y = ComputeGridPos(worldPos.y, locScale.y, gridSizeY);

		return (new Vector2(x, y));
	}

	float ComputeGridPos(float worldPosAxis, float scaleAxis, int gridSize) {
		return (((worldPosAxis - scaleAxis / gridSize * .5f) + scaleAxis * .5f) / (scaleAxis / gridSize));
	}

	///Draws the rects in the scene view while in play mode
	public void OnDrawGizmos() {
		if ( !Application.isPlaying ) {
			return;
		}

		for (int i = 0; i < theGrid.Length; i++ ) {
			for ( int u = 0; u < theGrid[i].Length; u++ ) {
				Gizmos.color = theGrid[i][u].isObstructed ? Color.red : Color.blue;
				DrawCube(i, u);
			}
		}
	}

	private void DrawCube(int i, int u) {
		Gizmos.DrawWireCube(new Vector3(theGrid[i][u].rect.center.x, 0, theGrid[i][u].rect.center.y), new Vector3(theGrid[i][u].rect.size.x, 0, theGrid[i][u].rect.size.y));
	}

	public Vector2 getGridPos() {
		return Vector2.zero;
	}

	/// <summary>
	/// Returns the center, as a vector 3, of the grid at the specified coordinate
	/// </summary>
	/// <returns>The world position in Vector3.</returns>
	/// <param name="gridCoords">Grid coordinates.</param>
	public Vector3 getWorldPos(int x, int y) {
		return new Vector3(theGrid[x][y].rect.center.x, transform.position.y, theGrid[x][y].rect.center.y);
	}

	public Vector3 getSpawnPoint() {
		int iterator = 0;
		Vector2 randPoints;
		int randX, randY;
		do {
			randPoints = RandGridPoints();
			randX = Mathf.RoundToInt(randPoints.x);
			randY = Mathf.RoundToInt(randPoints.y);
			theGrid[randX][randY].CheckIfObstructed();
			iterator++;
		} while ( theGrid[randX][randY].isObstructed && iterator < 1000 );

		if ( iterator >= 1000 ) {
			return Vector3.zero;
		}

		ObstructionPlaced(randX, randY);
		return new Vector3(theGrid[randX][randY].rect.center.x, yPlacementOfGO, theGrid[randX][randY].rect.center.y);
	}

	Vector2 RandGridPoints() {
		int randX, randY;
		randX = Random.Range(0, gridSizeX);
		randY = Random.Range(0, gridSizeY);
		return new Vector2(randX, randY);

	}

	public void ObstructionPlaced(int x, int y) {
		theGrid[x][y].isObstructed = true;
	}
	public void NoLongerObstructed(int x, int y) {
		theGrid[x][y].isObstructed = false;
	}
}
