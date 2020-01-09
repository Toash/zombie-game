using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PointsButton : MonoBehaviour
    {
        [SerializeField] private Player _player;
        public int _pointsToPlayer = 50;
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Did not get player component");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PointsButtonPressed()
        {
            _player.AddPoints(_pointsToPlayer);
        }
    }
}
