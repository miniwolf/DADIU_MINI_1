using UnityEngine;
using System.Collections;
/// <summary>
/// Set Nav Mesh Agent / stoppign distance to 2.4 (depending on a collider type the troll has)
/// In collider, set "Is Trigget to true"
/// </summary>
public class Enemy : MonoBehaviour {
    public float dist4SpeedIncrease;
    public float normalSpeed;
    public float accelerationFactor;

	// Use this for initialization
	public int stunTime = 2;
	[Tooltip("Name of the animation toggle to set to true")]
	public string moveAnimation;
	[Tooltip("Name of the animation toggle to set to true")]
	public string eatingAnimation;
	[Tooltip("Name of the animation toggle to set to true")]
	public string catchAnimation;

	private bool moving = true;

	// External components
	private ScoreInterface score;
	private GameObject following;

    // Personal components
    private SoundController soundCtrl;
    private NavMeshAgent navAgent;
	private Animator animator;
    private float now;

	void Start() {
		animator = GetComponentInChildren<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
        soundCtrl = new SoundController();
        soundCtrl.PlaySound("trollRoar", Constants.GAMESOUNDS);  
		score = GameObject.FindGameObjectWithTag(Constants.SCORE).GetComponent<ScoreInterface>();
		following = GameObject.FindGameObjectWithTag(Constants.PLAYER);
        now = Time.time;
	}

	// Update is called once per frame
	void Update() {
		if ( following != null && moving) {
			navAgent.Resume(); // resume agent 
            UpdateSpeed();
            StartMoving();
            if (Time.time - now > 0.35) {
                soundCtrl.PlaySound("trollFootstep", Constants.GAMESOUNDS);
                now = Time.time;
            }
            navAgent.destination = following.transform.position;
			transform.LookAt(following.transform.position);
		} else {
			navAgent.Stop (); // stop agent from evaluating path
		}
	}

	void OnCollisionEnter(Collision collision) {
		if ( moving ) {
			if ( collision.gameObject.tag.Equals(Constants.CAKEICON) ) {
				StartCoroutine(StopMoving());
                soundCtrl.PlaySound("trollHitByCake", Constants.GAMESOUNDS);                
                score.AddTrollScore();
			}
		}

		if ( collision.gameObject.tag.Equals(Constants.CAKEICON) ) {
			StartCoroutine(StopMoving());
			score.AddTrollScore();
		} else if ( collision.gameObject.tag.Equals(Constants.PLAYER) ) {
			CatchGirl();
            //Change here to add the end screen
            soundCtrl.PlaySound("trollCatchLaughter", Constants.GAMESOUNDS);
			GameObject.FindGameObjectWithTag("ReloadOBJ").GetComponent<Reload>().ReloadLevel();
		}
	}

    private void UpdateSpeed() {
		if ( GetDistanceToGirl() > dist4SpeedIncrease ) {
			navAgent.speed = GetDistanceToGirl() / dist4SpeedIncrease * normalSpeed * (1 + accelerationFactor);
		} else {
			navAgent.speed = normalSpeed;
		}
    }

    public float GetDistanceToGirl() {
        return Vector3.Distance(following.transform.position, transform.position);
    }

	IEnumerator StopMoving() {
		moving = false;
		animator.SetBool(moveAnimation, false);
		StartEating();
		yield return new WaitForSeconds(stunTime);
		StopEating();
		StartMoving();
	}

	private void StartEating() {
		animator.SetBool(eatingAnimation, true);
        soundCtrl.PlaySound("trollEating", Constants.GAMESOUNDS);
    }

	private void StopEating() {
		animator.SetBool(eatingAnimation, false);
        soundCtrl.PlaySound("trollGrowl", Constants.GAMESOUNDS);
    }

	private void StartMoving() {
		moving = true;
		animator.SetBool(moveAnimation, true);
	}

	private void CatchGirl() {
		moving = false;
		following.GetComponent<Player>().GotCaught();
		animator.SetBool(moveAnimation, false);
		animator.SetTrigger(catchAnimation);
    }
}
