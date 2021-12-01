using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeContent : MonoBehaviour {
    private GameTileContentType type;
    public GameTileContentType Type => type;

    public GameTileContentFactory OriginFactory { get; set; }

    public void Recycle() {
        OriginFactory.Reclaim(this);
    }

}

public enum GameTileContentType {
    Empty,
    Destination
}
