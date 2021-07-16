using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : Enemy
{
    public float speed;
    public List<Transform> points;
    public int nextId = 0;
    protected int idChangeValue = 1;
    protected float val;
    protected float respawnTimeFire;
    public float delayTimeFire;
    public GameObject fireBall;
    public GameObject crow;
    public GameObject curCrow;


    // protected float currentHealth = 0.5*;

    protected Animator bossAnimator;
    Transform main;
    //40-75% health
    private bool isAngry = false;
    GameObject subBoss1;
    GameObject subBoss2;
    [SerializeField] private GameObject cloneBoss;
    //private List<Transform> pointsAngry;
    //<50% health
    [SerializeField] private GameObject boneMonsters;
    int next = 0;
    float prevBossHealth;
    float respawnBone = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        // currentHealth = 0.5f * maxHealth;
        val = transform.localScale.x;
        main = GameObject.FindGameObjectWithTag("Player").transform;
        bossAnimator = GetComponent<Animator>();

        anim = GetComponent<Animator>();
        deadParameterID = Animator.StringToHash("dead");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= 0.75f * maxHealth)
            MoveToNext();
        else if (currentHealth >= 0.40f * maxHealth && currentHealth < 0.75f * maxHealth)
        {
            normalAngry();
        }
        else if (currentHealth > 0f * maxHealth && currentHealth < 0.4f * maxHealth)
        {
            crazyMode();
        }
        // else GameManager.PlayerWon();
    }

    void MoveToNext()
    {
        Transform nextPoint = points[nextId];

        if (nextPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-val * 1, val, val);
        }
        else
            transform.localScale = new Vector3(val, val, val);

        transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPoint.position) < 0.2f)
        {
            if (nextId == points.Count - 1)
            {
                idChangeValue = -1;
            }
            if (nextId == 0)
                idChangeValue = 1;

            if (transform.position.x < main.position.x)
                transform.localScale = new Vector3(-val * 1, val, val);
            else
                transform.localScale = new Vector3(val * 1, val, val);
            if (respawnTimeFire <= 0)
            {
                nextId += idChangeValue;
                //boss.Play("wizard fly forward (1)_0");

                Instantiate(fireBall, transform.position, Quaternion.identity);

                respawnTimeFire = delayTimeFire;
            }
            else
            {
                respawnTimeFire -= Time.deltaTime;
                //boss.Play("Wizard");
            }
        }

    }
    void normalAngry()
    {

        if (!isAngry)
        {
            Transform nextPoint = points[points.Count - 1];

            if (nextPoint.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-val * 1, val, val);
            }
            else
                transform.localScale = new Vector3(val, val, val);

            transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);
            if (transform.position.x == nextPoint.transform.position.x &&
            transform.position.y == nextPoint.transform.position.y)
            {

                isAngry = true;
                subBoss1 = Instantiate(cloneBoss, points[2].transform.position, Quaternion.identity);
                subBoss2 = Instantiate(cloneBoss, points[3].transform.position, Quaternion.identity);
                next = 0;
            }
        }
        else if (isAngry)
        {
            //bossAnimator.Play("bossIdle");
            Transform goalPoint = points[next];

            if (goalPoint.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-val * 1, val, val);
            }
            else
                transform.localScale = new Vector3(val, val, val);


            transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, (speed * 1.5f) * Time.deltaTime);
            if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
            {
                if (next == points.Count - 1)
                {
                    next = 0;
                }
                else if (next == 0)
                    next++;
                else
                    next = points.Count - 1;

            }
        }
    }

    void crazyMode()
    {
        if (isAngry)
        {

            subBoss1.transform.position = Vector2.MoveTowards(subBoss1.transform.position, transform.position, 8 * Time.deltaTime);
            subBoss2.transform.position = Vector2.MoveTowards(subBoss2.transform.position, transform.position, 8 * Time.deltaTime);
            if ((subBoss1.transform.position.x == transform.position.x && subBoss1.transform.position.y == transform.position.y) ||
                (subBoss2.transform.position.x == transform.position.x && subBoss2.transform.position.y == transform.position.y)
                )
            {
                Destroy(subBoss1);
                Destroy(subBoss2);
                isAngry = false;
                prevBossHealth = currentHealth;
            }

        }
        else
        {
            Transform point = points[points.Count - 1];
            if (transform.position.x == point.position.x && transform.position.y == point.position.y)
            {
                // Debug.Log("SKELETON");
                if (prevBossHealth - currentHealth >= 0.05f * maxHealth)
                {
                    Debug.Log("SKELETON");
                    prevBossHealth = currentHealth;
                    GameObject ske1 = Instantiate(boneMonsters, points[0].position, Quaternion.identity);
                    GameObject ske2 = Instantiate(boneMonsters, points[1].position, Quaternion.identity);
                    if (ske1.GetComponent<SkeletonOfBoss>() != null)
                    {
                        List<Transform> pointList = new List<Transform>(2);
                        pointList.Add(points[0]);
                        pointList.Add(points[1]);
                        ske1.GetComponent<SkeletonOfBoss>().SetPoints(pointList);
                    }
                    if (ske2.GetComponent<SkeletonOfBoss>() != null)
                    {
                        List<Transform> pointList = new List<Transform>(2);
                        pointList.Add(points[1]);
                        pointList.Add(points[0]);
                        ske2.GetComponent<SkeletonOfBoss>().SetPoints(pointList);
                    }
                    // ske2.GetComponent<Skeleton>().Rise();
                }
                //if (respawnBone <= 0)
                //{
                //    Instantiate(boneMonsters, points[0].position, Quaternion.identity);

                //    Instantiate(boneMonsters, points[1].position, Quaternion.identity);
                //    respawnBone = 2;currentHealth >= 0.10*maxHealth

                //}
                if ((currentHealth <= 0.20f * maxHealth) && curCrow == null)
                {
                    curCrow = Instantiate(crow, points[4].position, Quaternion.identity);
                    // respawnTimeFire = 3.0f;
                }
                else
                {
                    bossAnimator.Play("idle");
                    respawnTimeFire -= Time.deltaTime;
                }
            }
            else
            {

                transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            }


        }

    }
    public float getBossHealth()
    {
        return currentHealth;
    }
    public void reduceBar()
    {
        UIManager.UpdateHealthBar((currentHealth / maxHealth));
    }
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        reduceBar();
        if (currentHealth <= 0)
        {
            base.Die();
            GameManager.PlayerWon();
        }
    }

}
