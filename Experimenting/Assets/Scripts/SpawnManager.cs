using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{

    public class SpawnManager : MonoBehaviour
    {
        public GameObject _enemy;
        private GameObject _player;
        public GameObject[] enemySpawn;
        private UIManager _uiManager;
        public float _spawnRate;
        public int _round;
        public int _enemyCount;
        public int _enemiesSpawned;
        public int _maxEnemies;
        public int _enemiesKilled;
        private AudioSource _roundSound;
        void Start()
        {
            _roundSound = GetComponent<AudioSource>();
            _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
            _player = GameObject.Find("Player");
            _maxEnemies = 3;
            _round = 1;

            StartCoroutine(EnemySpawnRoutine());
        }

        // Update is called once per frame
        void Update()
        {
        }
        void SpawnEnemy()
        {
            GameObject enemyPrefab = Instantiate(_enemy, enemySpawn[Random.Range(0, 3)].transform.position, Quaternion.identity);
            _enemyCount++;
            _enemiesSpawned++;
            _uiManager.UpdateEnemyCount();
        }
        IEnumerator EnemySpawnRoutine()
        {
            while (true)
            {
                if (_enemiesSpawned < _maxEnemies)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(3f);
                }
                else
                {
                    Debug.Log("waiting");
                    yield return new WaitForSeconds(1);
                }
                if (_enemiesKilled == _maxEnemies)
                {
                    NextRound();
                    yield return new WaitForSeconds(15);
                }
            }
        }
        void NextRound()
        {
            _roundSound.Play();
            _enemyCount = 0;
            _enemiesKilled = 0;
            _enemiesSpawned = 0;
            _round++;
            _uiManager.UpdateRoundText();
        }

    }
}
