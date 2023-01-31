using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;

    public GhostMovement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightend frightened { get; private set; }
    public Transform target;
    public GhostBehavior initialBehavior;


    private void Awake() 
    {
        this.movement = GetComponent<GhostMovement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightend>();
    }

    private void Start() {
        ResetState();
    }

    public void ResetState() 
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if(this.home != this.initialBehavior) {
            this.home.Disable();
        }
        if(this.initialBehavior != null) {
            this.initialBehavior.Enable();
        }
     }

     private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled) 
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else 
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
     }
}
