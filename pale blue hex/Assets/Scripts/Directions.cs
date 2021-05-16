using UnityEngine;

public class Directions
{
      public static readonly Vector3 U = new Vector3(0, 1, 0);
      public static readonly Vector3 UR = new Vector3(Mathf.Cos(Mathf.PI / 6), Mathf.Sin(Mathf.PI / 6), 0);

      public static readonly Vector3 UL = new Vector3(-Mathf.Cos(Mathf.PI / 6), Mathf.Sin(Mathf.PI / 6), 0);

      public static readonly Vector3 D = new Vector3(0, -1, 0);
      public static readonly Vector3 DR = new Vector3(Mathf.Cos(Mathf.PI / 6), -Mathf.Sin(Mathf.PI / 6), 0);

      public static readonly Vector3 DL = new Vector3(-Mathf.Cos(Mathf.PI / 6), -Mathf.Sin(Mathf.PI / 6), 0);
}
