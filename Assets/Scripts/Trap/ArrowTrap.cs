using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;
    private float changeAttackCooldownTimer;
    public float changeAttackCooldown;
    private Animator anim;
    
    public bool isBoss;

    [Header("SFX")]
    [SerializeField] private AudioSource arrowSound;
    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    private void Attack()
    {
        cooldownTimer = 0;

        arrowSound.Play();
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(isBoss)
        {
            AttackIndicator();
            changeAttackCooldownTimer += Time.deltaTime;

            AbilityActivateIndicator();
            if(changeAttackCooldownTimer>=changeAttackCooldown)
            {
                changeAttackCooldownTimer=0;
                anim.SetTrigger("changeAttackCooldown");
            }
        }

        if (cooldownTimer >= attackCooldown)
            Attack();

    }

    void AttackIndicator()
    {
        if(cooldownTimer >= attackCooldown-1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

    void AbilityActivateIndicator()
    {
        if(changeAttackCooldownTimer >= changeAttackCooldown-1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }
}