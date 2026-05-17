using UnityEngine;

namespace Client.View.Misc
{
  public static class Vector3Ext
  {
    public static Vector3 ChangeZ(this Vector3 vector, float z) => 
      new(vector.x, vector.y, z);
  }
}