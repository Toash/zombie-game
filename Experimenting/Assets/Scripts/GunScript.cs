using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GunScript : MonoBehaviour
    {
        //gun parameters (description)
        public string _gunName;
        [SerializeField] private int _gunDamage = 50;
        [SerializeField] private int _headshotDamage = 100;
        public int _currentAmmo;
        public int _maxAmmo;
        [SerializeField] private float _reloadTime;
        public bool _isReloading = false;

        //gun move
        [SerializeField] private float _moveamount = 0.02f;
        [SerializeField] private float _movemaxAmount = 0.06f;
        [SerializeField] private float _movesmoothAmount = 6f;

        //gun move parameters
        private Vector3 initialPosition;
        private Vector3 finalMousePosition;
        private Vector3 recoilInput;

        //firerate
        [SerializeField] private float _fireRate = .15f;
        private float _canFire = -1f;

        //misc
        [SerializeField] private float _weaponRecoil = -1.25f;
        [SerializeField] GameObject _muzzleFlash;
        private CharacterController _controller;
        [SerializeField] private GameObject _casingEjectpoint;
        public Camera fpsCam;
        private Player _player;
        [SerializeField] GameObject _enemyHitEffect;
        [SerializeField] GameObject _groundHitEffect;
        FirstPersonController _fpsController;
        [SerializeField] private bool _isPistol;
        public AudioSource _gunShotSound;
        void Start()
        {
            initialPosition = transform.localPosition;
            _currentAmmo = _maxAmmo;
            _muzzleFlash.SetActive(false);
            _controller = GameObject.Find("Player").GetComponent<CharacterController>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            _fpsController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        }
        private void OnEnable()
        {
            _gunName = transform.name;
            _isReloading = false;
        }

        // Update is called once per frame
        void Update()
        {
            WeaponSway();
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
            if (_isReloading == true)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + new Vector3(0, -1, 0), 4 * Time.deltaTime);
            }
            if (_isReloading)
                return;
            if (_currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (_isPistol == false)
            {
                if (Input.GetButton("Fire1") && Time.time > _canFire)
                {
                    StartCoroutine(MuzzleFlash());
                    Fire();
                }
                if (_fpsController.m_IsWalking == false)
                {
                    WeaponSprint();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && Time.time > _canFire)
                {
                    StartCoroutine(MuzzleFlash());
                    Fire();
                }
                if (_fpsController.m_IsWalking == false)
                {
                    WeaponSprint();
                }
                //else
                {
                    //Vector3 euler = transform.localEulerAngles;
                    //euler.x = Mathf.Lerp(euler.x, 0, 4 * Time.deltaTime);
                    //transform.localEulerAngles = euler;
                }
            }
            void WeaponSprint()
            {
                //rotations not working
                //Vector3 euler = transform.localEulerAngles;
                //euler.x = Mathf.Lerp(euler.x, 30, 4 * Time.deltaTime);
                //transform.localEulerAngles = euler;
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + new Vector3(0, .25f, -1), 4 * Time.deltaTime);

            }
            void WeaponSway()
            {
                float mouseX = -Input.GetAxis("Mouse X") * _moveamount;
                float mouseY = -Input.GetAxis("Mouse Y") * _moveamount;
                mouseX = Mathf.Clamp(mouseX, -_movemaxAmount, _movemaxAmount);
                mouseY = Mathf.Clamp(mouseY, -_movemaxAmount, _movemaxAmount);

                finalMousePosition = new Vector3(mouseX, mouseY, 0);
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + finalMousePosition, Time.deltaTime * _movesmoothAmount);
                Vector3 euler = transform.localEulerAngles;
                euler.x = Mathf.Lerp(euler.x, 0, 4 * Time.deltaTime);
                transform.localEulerAngles = euler;
            }
            void WeaponRecoil()
            {
                recoilInput = new Vector3(0, 0, _weaponRecoil);
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + recoilInput, 6);
            }
            void Fire()
            {
                _canFire = Time.time + _fireRate;
                _gunShotSound.Play();
                Shoot();
                WeaponRecoil();
                _currentAmmo--;
            }
            IEnumerator Reload()
            {
                _isReloading = true;
                Debug.Log("Reloading");

                yield return new WaitForSeconds(_reloadTime);

                _currentAmmo = _maxAmmo;
                _isReloading = false;
            }
            IEnumerator MuzzleFlash()
            {
                _muzzleFlash.SetActive(true);
                yield return new WaitForSeconds(.05f);
                _muzzleFlash.SetActive(false);
                yield return new WaitForSeconds(.05f);
            }
            void Shoot()
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
                {
                    Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                    //if hit ground
                    if (hit.transform.tag != "Enemy")
                    {
                        GameObject _groundEffect = Instantiate(_groundHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(_groundEffect, 2f);
                    }
                    //if enemy is hit
                    if (hit.collider.tag == "Enemy")
                    {
                        //spawns the blood effect then destroys it after 2 seconds
                        GameObject _bloodEffect = Instantiate(_enemyHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(_bloodEffect, 2f);
                        if(hit.rigidbody != null)
                        {
                        hit.rigidbody.AddForce(-hit.normal * 500);
                        }

                        if (enemy._zombieIsDead == false)
                        {
                            Debug.Log("enemy hit");
                            enemy.SubtractLives(_gunDamage);
                            //add points to the player
                            _player.OnEnemyHit();
                        }
                        if (enemy._zombieIsDead == true && enemy._stopZombie == false)
                        {
                            _player.OnEnemyKill();
                            enemy.EnemyDeath();
                            enemy._stopZombie = true;
                        }
                    }
                }
            }
        }
    }
}
