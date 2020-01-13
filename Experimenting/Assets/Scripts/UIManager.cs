using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class UIManager : MonoBehaviour
    {
        private Text _pointsText;
        private Text _doorText;
        private Text _gunText;
        private Text _reloadText;
        private Text _repairText;
        private Text _roundText;
        private Text _zombieCountText;

        private Player _player;
        private Door _door;
        private GunScript _gun;
        private SpawnManager _spawnManager;

        //getting references
        void Awake()
        {
            _pointsText = GameObject.Find("PointsText").GetComponent<Text>();
            _doorText = GameObject.Find("DoorText").GetComponent<Text>();
            _gunText = GameObject.Find("GunText").GetComponent<Text>();
            _reloadText = GameObject.Find("ReloadingText").GetComponent<Text>();
            _repairText = GameObject.Find("RepairText").GetComponent<Text>();
            _roundText = GameObject.Find("RoundText").GetComponent<Text>();
            _zombieCountText = GameObject.Find("ZombieCount").GetComponent<Text>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            _doorText.text = ("");
        }
        // Update is called once per frame
        void LateUpdate()
        {
            UpdateRoundText();
            UpdateGunText();
            if(_gun._isReloading == true)
            {
                _reloadText.text = "Reloading...";
            }
            else
            {
                _reloadText.text = " ";
            }
            if(_player.canRepairWall)
            {
                _repairText.text = "press E to repair window";
            }
            else
            {
                _repairText.text = " ";
            }
        }
        public void FindGunReference()
        {
            _gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunScript>();
        }
        //updates ui
        public void UpdatePoints(int playerPoints)
        {
            _pointsText.text = "Points: " + playerPoints.ToString();
        }
        public void UpdateDoor()
        {
            _doorText.text = _door._costToOpen.ToString() + (" points to open door");
        }
        public void RemoveText()
        {
            _doorText.text = ("");
        }
        public void UpdateGunText()
        {
            _gunText.text = _gun._gunName.ToString() + ("       ") + _gun._currentAmmo.ToString() + ("/") + _gun._maxAmmo.ToString();
        }
        public void UpdateRoundText()
        {
            _roundText.text = "Round " + _spawnManager._round.ToString();
        }
        public void UpdateEnemyCount()
        {
            _zombieCountText.text = "Zombies: " + _spawnManager._enemyCount.ToString();
        }
    }
}
