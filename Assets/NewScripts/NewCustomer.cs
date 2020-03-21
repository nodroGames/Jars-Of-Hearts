using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NewScripts
{
    public class NewCustomer : MonoBehaviour
    {
        private NewPoint target;
        public float speed;
        public int startingHealth;
        private float dazedTime;
        private float startDazedTime = 1f;
        public int health;

        public bool isDead;
        public bool wasDead = false;
        public bool IsMoving { get; set; }
        
        public NewPoint Target { get => target; set => target = value; }

        public GameObject bloodEffect;

        public float beforeDestroyed;
        private GameObject BloodParticleSystem;
        public GameObject heart;

        public void Start()
        {
            isDead = false;
            //wasDead = false;
        }

        private void Update()
        {
        }

        public void PlaceAtPoint(NewPoint point)
        {
            this.gameObject.SetActive(true);
            this.transform.position = point.Position;
        }

        public void MoveTo(NewPoint point)
        {
            target = point;
            IsMoving = true;            
        }

        public void Move()
        {

            if (IsMoving && Vector2.Distance(this.transform.position, target.Position) != 0)
            {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, target.Position, speed * Time.deltaTime);
            }
            else
            {
                IsMoving = false;
            }
        }

        public void TakeDamage(int damage)
        {
            dazedTime = startDazedTime;
            // play a hurt sound
            BloodParticleSystem = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(BloodParticleSystem, beforeDestroyed);
            health -= damage;

            if (health <= 0)
            {
                //Let all out listeners know that we have died
                isDead = true;

                CustomerDeathEvent cde = new CustomerDeathEvent();
                cde.Description = "Unit "+ gameObject.name +" has died.";
                cde.DeadCustomerGO = gameObject;
                cde.FireEvent();

                this.gameObject.SetActive(false);
                Instantiate(heart, transform.position, heart.transform.rotation);
            }
            SuspicionBarScript.suspicion += 10f;
        }

        void OnDisable()
        {
            if (isDead)
            {
                Kitchen.customerIsOnTheTour = false;
                wasDead = true;
                Invoke("OnEnable", 2.0f);
                isDead = false;
            }
            else
            {
                return;
            }
            // TODO Unregister NewCustomer from Listener?
        }

        void OnEnable()
        {
            if (wasDead)
            {
                wasDead = false;
                health = startingHealth;
            }
            else
            {
                health = startingHealth;
            }            
        }
    }
}
