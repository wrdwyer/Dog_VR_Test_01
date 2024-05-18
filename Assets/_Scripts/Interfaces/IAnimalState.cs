using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimalState
{
    enum AnimalState
        {
        Idle,
        Anxious,
        Happy,
        Sad,
        Threatened
        }
    void SendState(AnimalState state);

}
