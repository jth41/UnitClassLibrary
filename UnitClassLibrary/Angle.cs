﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitClassLibrary
{


    /// <summary>
    /// Class used for storing Angles that may need to be accessed in a different measurement system
    /// Accepts anything as input
    /// 
    /// For an explanation of why this class is immutatble: http://codebetter.com/patricksmacchia/2008/01/13/immutable-types-understand-them-and-use-them/
    /// 
    /// <example>
    /// radians into degrees then returned as string
    /// 
    /// double radians = 10.5;
    /// Angle a = new Angle(AngleType.Radian, radians);
    /// 
    /// a.Degrees.ToString()         //for decimal degrees
    /// a.ToString(AngleType.Degree) //for formated string
    /// </example>
    /// 
    /// </summary>
    public class Angle : AngularDistance, IComparable<Angle>
    {
        #region Constructors

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Angle(): base() { }

        /// <summary>
        /// Create an angle object from an angle value.
        /// </summary>
        /// <param name="angleType">angle unit type</param>
        /// <param name="passedValue">angle value</param>
        public Angle(AngleType angleType, double passedValue)
        {
            switch (angleType)
            {
                case AngleType.Radian:
                    _intrinsicValue = passedValue;
                    _internalUnitType = AngleType.Radian;
                    break;
                case AngleType.Degree:
                    while (passedValue > 360)
                    {
                        passedValue = passedValue - 360;
                    }
                    _intrinsicValue = passedValue;
                    _internalUnitType = AngleType.Degree; 
                    break;
            }

        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="passedAngle">angle to copy</param>
        public Angle(Angle passedAngle)
        {
            this._intrinsicValue = passedAngle._intrinsicValue;
            this._internalUnitType = passedAngle._internalUnitType;
        }

        /// <summary>
        /// castes an angle string to angle degrees
        /// </summary>
        /// <param name="passedAngleString">angle string to parse</param>
        public Angle(string passedAngleString)
        {
            string[] seperatedStrings = passedAngleString.Split(new char[] { '°', '\'' });
            int degrees;
            int minutes;
            int seconds;

            try
            {
                degrees = int.Parse(seperatedStrings[0]);
                minutes = int.Parse(seperatedStrings[1]);
                seconds = int.Parse(seperatedStrings[2]);
            }
            catch (Exception)
            {
                throw new Exception();
            }

            _intrinsicValue = degrees + (minutes / 60f) + (seconds / 3600f);
            _internalUnitType = AngleType.Degree;
        }

        #endregion

        #region Overloaded Operators

        /* You may notice that we do not overload the increment and decrement operators nor do we overload multiplication and division.
         * This is because the user of this library does not know what is being internally stored and those operations will not return useful information. 
         */

        /// <summary>
        /// adds the two angles together
        /// </summary>
        /// <param name="a1">first angle</param>
        /// <param name="a2">second angle</param>
        /// <returns>the sum of the two angles</returns>
        public static Angle operator +(Angle a1, Angle a2)
        {
            //add the two Angles together
            //return a new Angle with the new value
            return new Angle(AngleType.Radian, (a1.Radians + a2.Radians));
        }

        /// <summary>
        /// subtracts one angle from the other
        /// </summary>
        /// <param name="a1">the angle to be subtracted from</param>
        /// <param name="a2">the angle to subtract</param>
        /// <returns>the result of the first angle minus the second</returns>
        public static Angle operator -(Angle a1, Angle a2)
        {
            //subtract the two Angles
            //return a new Angle with the new value
            return new Angle(AngleType.Radian, (a1.Radians - a2.Radians));
        }

        /// <summary>
        /// value equals to check if the two angles are equal
        /// </summary>
        /// <param name="a1">first angle to check for equality</param>
        /// <param name="a2">second angle to check for equality</param>
        /// <returns>whether the two angles are equal</returns>
        public static bool operator ==(Angle a1, Angle a2)
        {
            if ((object)a1 == null)
            {
                if ((object)a2 == null)
                {
                    return true;
                }
                return false;
            }
            return a1.Equals(a2);
        }

        /// <summary>
        /// value equals to check if the two angles are not equal
        /// </summary>
        /// <param name="a1">first angle to check for inequality</param>
        /// <param name="a2">second angle to check for inequality</param>
        /// <returns>whether the two angles aren't equal</returns>
        public static bool operator !=(Angle a1, Angle a2)
        {
            if ((object)a1 == null)
            {
                if ((object)a2 == null)
                {
                    return false;
                }
                return true;
            }
            return !a1.Equals(a2);
        }

        /// <summary>
        /// checks whether one angle is larger than the other
        /// </summary>
        /// <param name="a1">angle that is supposed to be larger</param>
        /// <param name="a2">angle that is supposed to be smaller</param>
        /// <returns>whether the first angle is greater than the second</returns>
        public static bool operator >(Angle a1, Angle a2)
        {
            return a1._intrinsicValue > a2._intrinsicValue;
        }

        /// <summary>
        /// checks whether one angle is smaller than the other
        /// </summary>
        /// <param name="a1">angle that is supposed to be smaller</param>
        /// <param name="a2">angle that is supposed to be larger</param>
        /// <returns>whether the first angle is less than the second</returns>
        public static bool operator <(Angle a1, Angle a2)
        {
            return a1._intrinsicValue < a2._intrinsicValue;
        }

        /// <summary>
        /// checks whether an angle is larger than or equal to the other
        /// </summary>
        /// <param name="a1">supposed to be larger or equal angle</param>
        /// <param name="a2">supposed to be smaller or equal angle</param>
        /// <returns>whether the angle on the left is greater than or equal to the angle on the right</returns>
        public static bool operator >=(Angle a1, Angle a2)
        {
            return a1.Equals(a2) || a1._intrinsicValue > a2._intrinsicValue;
        }

        /// <summary>
        /// checks whether an angle is less than or equal to the other
        /// </summary>
        /// <param name="a1">supposed to be smaller or equal angle</param>
        /// <param name="a2">supposed to be larger or equal angle</param>
        /// <returns>whether the angle on the left is less than or equal to the angle on the right</returns>
        public static bool operator <=(Angle a1, Angle a2)
        {
            return a1.Equals(a2) || a1._intrinsicValue < a2._intrinsicValue;
        }

        /// <summary>
        /// This override determines how this object is inserted into hashtables.
        /// </summary>
        /// <returns>same hashcode as any double would</returns>
        public override int GetHashCode()
        {
            //cannot use InternalUnitType because this would give a different hash depending on how the object was crr
            return this.GetValue(AngleType.Degree).GetHashCode();
        }

        /// <summary>
        /// Makes sure to throw an error telling the user that this is a bad idea
        /// The Angle class does not know what type of unit it contains, 
        /// (Because it should be thought of containing all unit types) 
        /// Call Angle.Tostring(AngleType) instead
        /// </summary>
        /// <returns>Should never return anything</returns>
        public override string ToString()
        {
            throw new NotImplementedException("The Angle class does not know what type of unit it contains, (Because it should be thought of as containing all unit types) Call Angle.[unit].ToString() instead");
        }

        /// <summary>
        /// value equality override
        /// </summary>
        /// <param name="obj">object to check equality against</param>
        /// <returns>a boolean representing the equality of the two angles</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            try
            {
                //check 

                Angle other = (Angle)obj;
                return Math.Abs(this._intrinsicValue - other.GetValue(this._internalUnitType)) <= DeviationDefaults.AcceptedEqualityDeviationAngle.GetValue(this._internalUnitType);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the a string converted to a desired unitType
        /// </summary>
        /// <param name="angleType">unit that you want the desired output in</param>
        /// <returns>string representation of angle in unit of choice</returns>
        public string ToString(AngleType angleType)
        {
            switch (angleType)
            {

                case AngleType.Radian:
                    return this.GetValue(AngleType.Radian).ToString() + " rad";
                case AngleType.Degree:
                    return this.GetValue(AngleType.Degree) + "°";
                default:
                    //code should never be run
                    return "We were unable to identify your desired Unit Type";
            }
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// This implements the IComparable interface and allows Angles to be sorted and such
        /// </summary>
        /// <param name="other">angle to compare against</param>
        /// <returns>1 if this > other; 0 if this == other; -1 if this < other</returns>
        public int CompareTo(Angle other)
        {
            if (this.Equals(other))
                return 0;
            else
                return (this.GetValue(_internalUnitType).CompareTo(other.GetValue(_internalUnitType)));
        }
        #endregion
    }
}
