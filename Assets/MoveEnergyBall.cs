using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnergyBall : MonoBehaviour {

    public int moveSpeed = 25;

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, 2);
    }
}
