using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int floor;
    public int elevatorProgress;
    public float[] position;

    public PlayerData(PlayerController playerController)
    {
        floor = playerController.getPlayerLevel();
        elevatorProgress = playerController.getElevatorProgress();

        position = new float[2];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;

    }
}
