using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 savedValue;
    public int elevatorProgress;
    public int savedElevatorProgress;

    public void OnAfterDeserialize(){ 
        initialValue = savedValue; 
        elevatorProgress = savedElevatorProgress;
        }
    
    public void OnBeforeSerialize(){}

}
