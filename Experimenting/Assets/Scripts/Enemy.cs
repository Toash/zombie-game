using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent _nav;
    public int _lives = 100;
    public AudioClip[] _zombieDeathSound;
    public AudioClip[] _zombieGroans;
    private AudioSource _audioSource;
    public bool _zombieIsDead = false;
    public bool _stopZombie = false;
    private Collider _collider;
    private Animator _anim;
    private Collider _playerCollider;
    private Collider[] _ragdollColliders;
    private Rigidbody[] _ragdollRigidbodies;
    bool _behindWall;
    //public Light _aura;
    private float attackTimer;
    public float timeToAttack = 2f;


    // Start is called before the first frame update
    void Start()
    {
        //_aura = GetComponentInChildren<Light>();
        _nav = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
        if(_collider == null)
        {
            Debug.LogError("Did not get collider");
        }
        _anim = GetComponentInChildren<Animator>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Collider col in _ragdollColliders)
        {
            if(!col.CompareTag("Enemy"))
            {
                col.enabled = false;
            }
        }
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        _playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        StartCoroutine(ZombieGroan());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _player = GameObject.Find("Player");
        if (_nav.enabled == true)
        {
            _nav.SetDestination(_player.transform.position);
        }
        if(_lives < 0)
        {
            _zombieIsDead = true;
        }
        float distanceFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceFromPlayer < 3 || _behindWall)
        {
            attackTimer += Time.deltaTime;
        }
        else if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime * 2;
        }
        else
            attackTimer = 0;
    }
    bool Attack()
    {
        if(attackTimer > timeToAttack)
        {
            attackTimer = 0;
            return true;
        }
        return false;
    }
    IEnumerator ZombieGroan()
    {
        while (_zombieIsDead == false)
        {
            yield return new WaitForSeconds(Random.Range(10, 30));
            _audioSource.clip = _zombieGroans[Random.Range(0, 6)];
            _audioSource.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(ZombieAttack());
        }
    }
    IEnumerator ZombieAttack()
    {
        _nav.enabled = false;
        _anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(2.5f);
        if (_zombieIsDead == false)
        {
            _nav.enabled = true;
            _anim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(2);
        }
    }
    public void SubtractLives(int subtractAmount)
    {
        //on enemy hit
        _lives -= subtractAmount;
    }
    public void EnemyDeath()
    {
        Destroy(_collider);
        //_aura.color = Color.cyan;
        _collider.gameObject.layer = 2;
        _nav.enabled = false;
        _anim.enabled = false;
        //enables colliders
        foreach (Collider col in _ragdollColliders)
        {
            col.enabled = true;
            Physics.IgnoreCollision(col, _playerCollider, true);
        }
        //disables kinematics in rigidbody (makes gravity apply on the zombie)
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        _audioSource.clip = _zombieDeathSound[Random.Range(0, 11)]; //plays zombie death sound
        _audioSource.Play(); //plays zombie death sound
        Destroy(this.gameObject, 7);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "SpawnWall")
        {
            if(other.GetComponent<Renderer>().enabled)
            {
                Debug.Log("dfasdf");
                _behindWall = true;
                _nav.enabled = false;
                if(Attack() == true)
                {
                    other.GetComponent<Collider>().SendMessageUpwards("RemoveBoard", SendMessageOptions.RequireReceiver);
                }
            }
            else
            {
                _nav.enabled = true;
                _behindWall = false;
            }
        }
    }
}
