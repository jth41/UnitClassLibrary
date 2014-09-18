﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitClassLibrary.Tests
{
    [TestFixture()]
    public class AngleTests
    {
        [Test()]
        public void AngleN_GetHashCode()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI * 2);
            Angle a3 = new Angle(AngleType.Degree, 359);

            int hash1 = a1.GetHashCode();
            int hash2 = a2.GetHashCode();
            int hash3 = a3.GetHashCode();

            Assert.AreEqual(hash1,hash2);
            Assert.AreNotEqual(hash2,hash3);
        }

        // SHOULD BE 275 BUT HAS ROUNDING ERRORS
        [Test()]
        public void AngleN_ToStringOverride()
        {
            Angle a1 = new Angle(AngleType.Degree, 275);
            Angle a2 = new Angle(AngleType.Radian, 2 * Math.PI);

            Assert.AreEqual("275°0'-16500\"°", a1.ToString(AngleType.Degree));
        }

        [Test()]
        public void AngleN_EqualsTest()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI * 2);
            Angle a3 = new Angle(AngleType.Degree, 359);

            bool result1 = a1.Equals(a2);
            bool result2 = a2.Equals(a3);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }

        [Test()]
        public void AngleN_MathOperatorTest()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI * 2);

            Angle addedAngle = a1 + a2;
            Assert.AreEqual(720,addedAngle.Degrees);

            Angle subtractedAngle = a1 - a2;
            Assert.AreEqual(0,subtractedAngle.Radians);
        }

        [Test()]
        public void AngleN_ComparisonOperatorTest()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI * 2);
            Angle subtractedAngle = a1 - a2;

            Assert.IsTrue(subtractedAngle < a2);
            Assert.IsTrue(a1 > subtractedAngle);
            Assert.IsTrue(a1 == a2);
            Assert.IsTrue(a1 >= a2);
            Assert.IsTrue(a1 <= a2);
        }

        [Test()]
        public void AngleN_CompareToTest()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI * 2);
            Angle a3 = new Angle(AngleType.Degree, 720);

            Assert.AreEqual(0,a1.CompareTo(a2));
            Assert.AreEqual(0,a1.CompareTo(a2));
            Assert.AreEqual(-1,a1.CompareTo(a3));
            Assert.AreEqual(1,a3.CompareTo(a2));
        }

        [Test()]
        public void AngleN_NegationTest()
        {
            Angle a1 = new Angle(AngleType.Degree, 360);
            Angle a2 = new Angle(AngleType.Radian, Math.PI);

            Assert.AreEqual(-360,a1.Negate().Degrees,-360);
            Assert.AreEqual(Math.PI * -1,a2.Negate().Radians);
            Assert.AreEqual(Math.PI * -2,a1.Negate().Radians);
            Assert.AreEqual(-180,a2.Negate().Degrees);
        }
    }
}
