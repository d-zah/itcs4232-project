using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{

    public void disableButton(Selectable button){
        button.interactable = false;
    }

}
