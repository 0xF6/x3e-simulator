namespace EtherReactorSimulator.Types
{
    using System;
    using System.Globalization;

    public struct ufloat
    {
        /// <summary>
        /// Equivalent to <see cref="float.Epsilon"/>.
        /// </summary>
        public static ufloat Epsilon = float.Epsilon;

        /// <summary>
        /// Represents the smallest possible value of <see cref="ufloat"/> (0).
        /// </summary>
        public static ufloat MinValue = 0f;

        /// <summary>
        /// Represents the largest possible value of <see cref="ufloat"/> (equivalent to <see cref="float.MaxValue"/>).
        /// </summary>
        public static ufloat MaxValue = float.MaxValue;

        /// <summary>
        /// Equivalent to <see cref="float.NaN"/>.
        /// </summary>
        public static ufloat NaN = float.NaN;

        /// <summary>
        /// Equivalent to <see cref="float.PositiveInfinity"/>.
        /// </summary>
        public static ufloat PositiveInfinity = float.PositiveInfinity;

        private readonly float value;

        public ufloat(float Value)
        {
            if (double.IsNegativeInfinity(Value))
                throw new NotSupportedException();

            value = Value < 0 ? 0 : Value;
        }

        public static implicit operator float(ufloat d) => d.value;

        public static implicit operator ufloat(float d) => new ufloat(d);

        public static bool operator <(ufloat a, ufloat b) => a.value < b.value;

        public static bool operator >(ufloat a, ufloat b) => a.value > b.value;

        public static bool operator ==(ufloat a, ufloat b) => a.value == b.value;

        public static bool operator !=(ufloat a, ufloat b) => a.value != b.value;

        public static bool operator <=(ufloat a, ufloat b) => a.value <= b.value;

        public static bool operator >=(ufloat a, ufloat b) => a.value >= b.value;

        public override bool Equals(object a) => a is ufloat val && this == val;

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => value.ToString(CultureInfo.InvariantCulture);
    }
}