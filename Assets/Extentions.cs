using UnityEngine;

public static class Extentions
{
    public static Vector3 Abs(this Vector3 vector) => new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z)); 
    public static Vector2 Abs(this Vector2 vector) => new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
}
