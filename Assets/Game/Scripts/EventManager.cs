using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void D_Void();
    public delegate void D_Int(int value);
    public delegate void D_String(string value);
    public delegate void D_Float(float value);
    public delegate void D_Bool(bool value);

    public static event D_Void onGameReset;
    public static event D_Void onBrickHit;
    public static event D_Void onBrickDestroyed;
    public static event D_Void onWinLevel;
    public static event D_Void onLoseLevel;
    public static event D_Int onScoreChanged;
    public static event D_Int onLivesChanged;

    public static void GameReset() { onGameReset?.Invoke(); }
    public static void BrickHit() { onBrickHit?.Invoke(); }
    public static void BrickDestroyed() { onBrickDestroyed?.Invoke(); }
    public static void LevelComplete() { onWinLevel?.Invoke(); }
    public static void LevelFail() { onLoseLevel?.Invoke(); }
    public static void ScoreChanged(int score) { onScoreChanged?.Invoke(score); }
    public static void LivesChanged(int lives) { onLivesChanged?.Invoke(lives); }

}
