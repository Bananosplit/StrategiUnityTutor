using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameTile : MonoBehaviour {
    [SerializeField]
    GameObject arrow;

    GameTile north;
    GameTile east;
    GameTile west;
    GameTile south;
    GameTile nextSomePath;

    Quaternion northRotation = Quaternion.Euler(0, 180, 0);
    Quaternion eastRotation = Quaternion.Euler(0, 90, 0);
    Quaternion southRotation = Quaternion.Euler(0, 0, 0);
    Quaternion westRotation = Quaternion.Euler(0, 270, 0);


    int distance;

    public bool HasPath() {
        return distance != int.MaxValue;
    }

    public void ClearPath() {
        distance = int.MaxValue;
        nextSomePath = null;
    }

    public void BecomeDestination() {
        distance = 0;
        nextSomePath = null;
    }

    private GameTile GrowPathTo(GameTile neighbor) {
        if (!HasPath() || neighbor == null || neighbor.HasPath())
            return null;

        neighbor.distance = distance + 1;
        neighbor.nextSomePath = this; 
        return neighbor;
    }

    public GameTile GrowToNorth() {
        return GrowPathTo(north);
    }
    public GameTile GrowToSouth() {
        return GrowPathTo(south);
    }
    public GameTile GrowToEast() {
        return GrowPathTo(east);
    }
    public GameTile GrowToWest() {
        return GrowPathTo(west);
    }

    public static void MakeEastWestNeihbor(GameTile east, GameTile west) {
        east.west = west;
        west.east = east;
    }
    public static void MakeNorthSouthNeihbor(GameTile south, GameTile north) {
        north.south = south;
        south.north = north;
    }

    public void ShowPath() {
        if (distance == 0) {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);
        arrow.transform.localRotation =
            nextSomePath == north ? northRotation :
            nextSomePath == east ? eastRotation :
            nextSomePath == south ? southRotation :
            westRotation;
    }


}
