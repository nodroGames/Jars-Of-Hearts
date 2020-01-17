using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Assets.NewScripts
{
    public class PlayerAttack : MonoBehaviour
    {

        private float timeBtwAttack;
        public float startTimeBtwAttack;

        public Transform attackPos;
        public LayerMask whatIsEnemies;
        public Animator camAnim;
        public Animator playerAnim;
        public float attackRange;
        public int damage;

        private void Start()
        {
            
        }

        private void Update()
        {
            Attack();
        }

        public void Attack()
        {
            if (Input.GetMouseButtonDown(0) && PlayerController.isInKitchen == true)
            {
                if (timeBtwAttack <= 0 && PauseDialogue.GameIsPaused == false)
                {
                    //then you can attack

                    playerAnim.SetTrigger("attack");
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        camAnim.SetTrigger("shake");
                        enemiesToDamage[i].GetComponent<NewCustomer>().TakeDamage(damage);
                    }

                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }            

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}