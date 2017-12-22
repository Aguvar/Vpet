using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PetBehaviour : MonoBehaviour
{

    public float initialHunger;
    public float initialThirst;
    public float initialFun;
    public float initialEnergy;
    public float initialHealth;

    public Transform corralBoundStart;
    public Transform corralBoundEnd;

    public int minIdleseconds;
    public int maxIdleSeconds;
    

    //{Hunger, Thirst, Fun, Energy, Health}
    private float[] stats;

    private Transform petTransform;
    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer sRenderer;

    private Quaternion faceCameraVector;

    private bool readyForNextAction;


    public float[] Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Awake()
    {
        stats = new float[] { initialHunger, initialThirst, initialFun, initialEnergy, initialHealth };
    }

    // Use this for initialization
    void Start()
    {
        petTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();

        faceCameraVector = Quaternion.LookRotation(Vector3.back, Vector3.up);

        StartCoroutine(HangAround());
        StartCoroutine(animationController());
    }

    // Update is called once per frame
    void Update()
    {
        Simulate(Time.deltaTime);
        petTransform.rotation = faceCameraVector;
    }

    private IEnumerator animationController()
    {
        while (true)
        {
            bool moving = agent.velocity.magnitude > 0.3;
            sRenderer.flipX = agent.velocity.x <= 0;
            
            animator.SetBool("Moving", moving);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private PetBehaviours DecideNextAction()
    {
        /*
        Possible decisions:
            Do nothing
            Walk
            Run
            Check for food
            Check for water
        */

        RandomBag<PetBehaviours> behavioursBag = new RandomBag<PetBehaviours>();

        //Work on making these influenced by pet stats
        behavioursBag.AddElement(PetBehaviours.Idle, 100);
        behavioursBag.AddElement(PetBehaviours.Walk, 50);

        return behavioursBag.ChooseRandom();
    }

    private IEnumerator HangAround()
    {
        while (true)
        {
            switch (DecideNextAction())
            {
                case PetBehaviours.Idle:
                    StartCoroutine(BeIdleForSomeSeconds());
                    break;
                case PetBehaviours.Walk:
                    StartCoroutine(WalkSomewhere());
                    break;
                case PetBehaviours.Run:
                    break;
                case PetBehaviours.Eat:
                    break;
                default:
                    break;
            }
            readyForNextAction = false;
            yield return new WaitUntil(() => isReadyForNextAction());
        }
    }

    private IEnumerator BeIdleForSomeSeconds()
    {
        yield return new WaitForSeconds(Random.Range(3,8));
        readyForNextAction = true;
    }

    private IEnumerator WalkSomewhere()
    {
        Vector3 destination = GenerateDestination();
        agent.SetDestination(destination);
        yield return new WaitUntil(() => agent.velocity.magnitude == 0);
        readyForNextAction = true;
    }

    private bool isReadyForNextAction()
    {
        return readyForNextAction;
    }

    private Vector3 GenerateDestination()
    {
        float X = Random.Range(corralBoundStart.position.x, corralBoundEnd.position.x);
        float Z = Random.Range(corralBoundStart.position.z, corralBoundEnd.position.z);

        return new Vector3(X, petTransform.position.y, Z);
    }

    public void Simulate(float simTime)
    {
        stats[0] = stats[0] - simTime * 0.022f;
        stats[1] = stats[1] - simTime * 0.024f;
        stats[2] = stats[2] - simTime * 0.046f;
        stats[3] = stats[3] - simTime * 0.0115f;
        stats[4] = stats[4] + (stats[0] - 100) * 0.02f + (stats[1] - 200) * 0.02f + stats[3] * 0.02f;
        //Do something about magic numbers afterwards. Abstraction for different pets to have different needs.
    }





}
