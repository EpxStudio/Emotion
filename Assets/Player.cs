using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats { // like a class declaration
        public int Health = 100;
    }
    public PlayerStats playerStats = new PlayerStats(); // like a new instance

    public int fallBoundary = -20;

    void Update() {
        if (transform.position.y <= fallBoundary) {
            DamagePlayer (999999);
        }
    }

    public void DamagePlayer(int damage){
        playerStats.Health -= damage;
        if (playerStats.Health <= 0){
            GameMaster.KillPlayer(this);
        }
    }
}
