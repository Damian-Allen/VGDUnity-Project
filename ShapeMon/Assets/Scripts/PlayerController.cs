using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    enum Direction {
        UP,LEFT,RIGHT,DOWN,NONE
    }
    
    private Direction currentDir;
    private Vector3 targetTransform;
    private bool amMove;
    private bool running = false;

    [Tooltip("Lower Values for faster movement. DO NOT GO TO 0!")]
    public float timeToMove;
    public Tilemap wallTileMap;
    public Tilemap encounterTileMap;
    private TileBase checkWallTile;
    private TileBase checkEncounterTile;

    

    void Start() {

    }

    void FixedUpdate() {

        if(!amMove){
            GetInput();
        }

        GetDirection();

        

        if (checkWallTile == null) {
            if (amMove && !running) {
                running = true;
                StartCoroutine(LerpPosition(targetTransform, timeToMove));
            }
        } else {
            amMove = false;
        }
        
        
        
    }

    private void GetInput() {
        if(Input.GetAxis("Vertical") > 0 ){
                currentDir = Direction.UP;
                amMove = true;
            } else if(Input.GetAxis("Vertical") < 0 ){
                currentDir = Direction.DOWN;
                amMove = true;
            } else if(Input.GetAxis("Horizontal") > 0){
                currentDir = Direction.RIGHT;
                amMove = true;
            } else if(Input.GetAxis("Horizontal") < 0){
                currentDir = Direction.LEFT;
                amMove = true;
            }
    }

    private void GetDirection() {
        switch (currentDir){
            case Direction.UP: 
                targetTransform = transform.position + Vector3.up;
                checkWallTile = wallTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                checkEncounterTile = encounterTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                currentDir = Direction.NONE;
                break;

            case Direction.DOWN:
                targetTransform = transform.position + Vector3.down;
                checkWallTile = wallTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                checkEncounterTile = encounterTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                currentDir = Direction.NONE;
                break;

            case Direction.LEFT:
                targetTransform = transform.position + Vector3.left;
                checkWallTile = wallTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                checkEncounterTile = encounterTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                currentDir = Direction.NONE;
                break;

            case Direction.RIGHT:
                targetTransform = transform.position + Vector3.right;
                checkWallTile = wallTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                checkEncounterTile = encounterTileMap.GetTile(Vector3Int.FloorToInt(targetTransform));
                currentDir = Direction.NONE;
                break;

            default: break;
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration) {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration) {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (checkEncounterTile != null) {
            GameController.Instance.EncounterCheck();
        }
        transform.position = targetPosition;
        amMove = false;
        running = false;
    }



}
