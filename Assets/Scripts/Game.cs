using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameBoard ground;
    [SerializeField]
    private Vector2Int groundSize;

    private void Start() {
        ground.Initialization(groundSize);
    }
}
