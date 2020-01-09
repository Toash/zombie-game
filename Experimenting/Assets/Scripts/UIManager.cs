using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class UIManager : MonoBehaviour
    {
        public Text _pointsText;
        public Text _coeffText;
        public Text _doorText;
        public Text _gunText;
        public Text _reloadText;
        public Text _repairText;
        public Text _roundText;

        private Player _player;
        private Coeff _coeff;
        private Door _door;
        private GunScript _gun;
        private SpawnManager _spawnManager;
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Error");
            }
            _coeff = GameObject.Find("Coeff").GetComponent<Coeff>();
            if (_coeff == null)
            {
                Debug.LogError("Error");
            }
            _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if(_spawnManager == null)
            {
                Debug.LogError("Did not get spawn manager");
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCoeff();
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
        public void UpdateCoeff()
        {
            _coeffText.text = "SpawnRate Coeff: " + _coeff._coefficient.ToString();
        }
        public void UpdateDoor()
        {
            _doorText.text = _door._costToOpen.ToString() + (" points to open door");
        }
        public void UpdateGunText()
        {
            _gunText.text = _gun._gunName.ToString() + ("       ") + _gun._currentAmmo.ToString() + ("/") + _gun._maxAmmo.ToString();
        }
        public void UpdateRoundText()
        {
            _roundText.text = _spawnManager._round.ToString();
        }
    }
}
