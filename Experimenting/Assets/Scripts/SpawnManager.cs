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
        void Start()
        {
            _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
            _player = GameObject.Find("Player");

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

        }
        IEnumerator EnemySpawnRoutine()
        {
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
