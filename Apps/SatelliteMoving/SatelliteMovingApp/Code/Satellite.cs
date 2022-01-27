using System;
using Newtonsoft.Json;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace SatelliteMovingApp.Code
{
   [JsonObject(MemberSerialization.OptIn)]
   public sealed class Satellite : IEquatable<Satellite>
   {
      private const long DefaultRoundingTime = 0;
      private const float DefaultDistance = .0F;
      private const float DefaultAngle = .0F;

      private Satellite(long currentTime, float currentDistance, float angle)
      {
         RoundingTime = currentTime;
         Distance = currentDistance;
         Angle = angle;
      }

      public Satellite(long currentTime, float currentDistance)
         : this(currentTime, currentDistance, RandomAngle())
      {
      }

      public Satellite()
         : this(DefaultRoundingTime, DefaultDistance, DefaultAngle)
      {
      }

      [JsonProperty] public long RoundingTime { get; set; }

      [JsonProperty] public float Distance { get; set; }

      [JsonProperty] public float Angle { get; set; }

      public bool Equals(Satellite other)
         => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || RoundingTime == other.RoundingTime &&
                                              Distance.Equals(other.Distance) && Angle.Equals(other.Angle));

      private static float RandomAngle() => (float) (2 * Math.PI * new Random().NextDouble());

      public override bool Equals(object obj)
         => !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || obj is Satellite other && Equals(other));

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = RoundingTime.GetHashCode();
            hashCode = (hashCode * 397) ^ Distance.GetHashCode();
            hashCode = (hashCode * 397) ^ Angle.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(Satellite left, Satellite right) => Equals(left, right);

      public static bool operator !=(Satellite left, Satellite right) => !Equals(left, right);
   }
}