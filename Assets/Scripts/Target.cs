using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Score change when clicked")]
    public int clickScore = 10;

    private void Awake()
    {
        if (Random.Range(0, 3) == 0)
        {
            clickScore = -clickScore;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //elapsedTime = elapsedTime + Time.deltaTime;
        //if (elapsedTime > lifespan)
        //{
        //    Destroy(gameObject);
        //}

        // TODO: change -10 to sth. reliable
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }

    public int Hit()
    {
        Destroy(gameObject);
        return clickScore;
    }
}
