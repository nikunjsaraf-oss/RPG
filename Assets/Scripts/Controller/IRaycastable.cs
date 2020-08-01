using System;
using System.Collections;
using UnityEngine;

namespace RPG.Controller
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRayCast(PlayerController callingController);
    }
}