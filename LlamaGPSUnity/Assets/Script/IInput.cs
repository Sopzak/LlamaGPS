using System;
using UnityEngine;

public interface IInput
{
    Action<Vector2> OnMovementInput { get; set;}
    Action<Vector3> OnMovementDirectionInput { get; set;}

}
