using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Player : MonoBehaviour
    {
        //player characteristics
        public int _lives = 100;
        public int _points;
        public int _interactRange;
        public int _pointsOnHit = 10;

        //references
        private UIManager _uiManager;
        public Camera _camera;
        private Door _door;
        private PointsButton _pointsButton;
        //misc
        public bool canRepairWall;
        float rebuildTimer = 0;
        public float RebuildTime;
        // Start is called before the first frame update
        void Start()
        {
            _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
            if (_uiManager == null)
            {
                Debug.LogError("Did not get ui manager component");
            }

        }

        // Update is called once per frame
        void Update()
        {
            HoverInteract(); // when you hover over items
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "SpawnWall")
            {
                rebuildTimer += Time.deltaTime;
                if(canRepairWall)
                {
                    if(Input.GetKey(KeyCode.E) && rebuildTimer > RebuildTime)
                    {
                        other.SendMessage("AddBoard", SendMessageOptions.RequireReceiver);
                        rebuildTimer = 0;
                    }
                }
            }
        }
        public void AddPoints(int score)
        {
            _points += score;
            _uiManager.UpdatePoints(_points);
        }
        void Interact()//interacts with game world
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _interactRange))
            {
                if (hit.transform.name == "Door")
                {
                    _door.OpenDoor();
                }
                if (hit.transform.name == "PointsButton")
                {
                    _pointsButton.PointsButtonPressed();
                }
            }
        }
        //when you hover over items
        void HoverInteract()
        {
            RaycastHit hoverHit;
            _uiManager._doorText.text = ("");
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hoverHit, _interactRange))
            {
                if (hoverHit.transform.name == "Door")
                {
                    _uiManager.UpdateDoor();
                }
            }
        }
        public void SubtractLives()
        {
            _lives -= 10;
        }
        public void OnEnemyHit()
        {
            _points += _pointsOnHit;
            _uiManager.UpdatePoints(_points);
        }
        public void OnEnemyKill()
        {
            _points += 100;
            _uiManager.UpdatePoints(_points);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "SpawnWall" && other.GetComponent<WallBoardScript>().boards != 0)
            {
                canRepairWall = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "SpawnWall")
            {
                canRepairWall = false;
            }
        }
    }
}
