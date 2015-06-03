﻿using System;
using UnitClassLibrary;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using UnitClassLibrary.DistanceUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.FootUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.InchUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.MileUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.YardUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Metric.CentimeterUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Metric.KilometerUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Metric.MeterUnit;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Metric.MillimeterUnit;
using UnitClassLibrary.GenericUnit;


namespace UnitLibraryTests
{
    /// <summary>
    /// Test Class for all conversion functions 
    /// </summary>
    [TestFixture()]
    public class DistanceTests
    {
        /// <summary>
        /// Tests the architectural string constructor and the regular Distance constructor
        /// </summary>
        [Test()]
        public void Distance_Constructors()
        {

            // arrange & act

            //numeric value constructor
            Distance inchDistance = new Distance(new Inch(), 14.1875);

            //architectural string constructor
            Distance architecturalDistance = new Distance("1' 2 3/16\"");

            //copy constructor
            Distance copiedDistance = new Distance(architecturalDistance);

            // assert
            inchDistance.AsMillimeters().Should().Be(architecturalDistance.AsMillimeters());
            copiedDistance.ShouldBeEquivalentTo(architecturalDistance);
        }

        /// <summary>
        /// Tests mathmatical operators
        /// </summary>
        [Test()]
        public void Distance_Math_Operators()
        {
            // arrange
            Distance inchDistance = new Distance(new Inch(), 14.1875);
            Distance architecturalDistance = new Distance("1'2 3/16\"");

            // act
            Distance subtractionDistance = inchDistance - architecturalDistance;
            Distance additionDistance = inchDistance + architecturalDistance;

            // assert
            subtractionDistance.Equals(new Distance(new Inch(), 0)).Should().BeTrue();
            additionDistance.Equals(new Distance(new Millimeter(), 720.725)).Should().BeTrue();
            additionDistance.Architectural.Should().Be("2'4 6/16\"");
        }

        [Test]
        public void DistanceConversions()
        {

            Distance kilometerDistance = new Distance(new Kilometer(), 1);

            (kilometerDistance == new Distance(new Millimeter(), 1000000)).Should().BeTrue();
            (kilometerDistance == new Distance(new Centimeter(), 100000)).Should().BeTrue();
            (kilometerDistance == new Distance(new Inch(), 39370.1)).Should().BeTrue();
            (kilometerDistance == new Distance(new Foot(), 3280.84)).Should().BeTrue();
            (kilometerDistance == new Distance(new Yard(), 1093.61)).Should().BeTrue();
            (kilometerDistance == new Distance(new Mile(), 0.621371)).Should().BeTrue();
            (kilometerDistance == new Distance(new Meter(), 1000)).Should().BeTrue();
            kilometerDistance.Architectural.Should().Be("3280'10 1/16\""); //need to recheck


        }

        /// <summary>
        /// Tests Architectural string inputs.
        /// </summary>
        [Test()]
        public void Distance_Architectural_Constructor()
        {
            // arrange
            Distance Distance1 = new Distance("1'2 3/16\"");
            Distance Distance2 = new Distance("1'");
            Distance Distance3 = new Distance("1'2\"");
            Distance Distance4 = new Distance("2 3/16\"");
            Distance Distance5 = new Distance("1'2-3/16\"");
            Distance Distance6 = new Distance("3/16\"");
            Distance Distance7 = new Distance("121103");
            Distance Distance8 = new Distance("-1'2\"");

            // assert
            Distance1.Architectural.ShouldBeEquivalentTo("1'2 3/16\"");
            Distance2.Architectural.ShouldBeEquivalentTo("1'");
            Distance3.Architectural.ShouldBeEquivalentTo("1'2\"");
            Distance4.Architectural.ShouldBeEquivalentTo("2 3/16\"");
            Distance5.Architectural.ShouldBeEquivalentTo("1'2 3/16\"");
            Distance6.Architectural.ShouldBeEquivalentTo("3/16\"");
            Distance7.Architectural.ShouldBeEquivalentTo("12'11 3/16\"");
            Distance8.Architectural.ShouldBeEquivalentTo("-1'2\"");
        }

        /// <summary>
        /// Tests all equality operators
        /// </summary>
        [Test()]
        public void Distance_EqualityTests()
        {
            // arrange
            Distance biggerDistance = new Distance(new Inch(), 14.1875);
            Distance smallerDistance = new Distance("1' 2 1/16\"");
            Distance equivalentbiggerDistance = new Distance(new Millimeter(), 360.3625);

            (equivalentbiggerDistance.Equals(biggerDistance)).Should().Be(true);
            (equivalentbiggerDistance == smallerDistance).Should().Be(false);

            (equivalentbiggerDistance != smallerDistance).Should().Be(true);
            (equivalentbiggerDistance != biggerDistance).Should().Be(false);


            //check ==
            bool nonNullFirst = (biggerDistance == null);
            bool nullFirst = (null == biggerDistance);
            bool bothNull = (null == null);

            nonNullFirst.Should().BeFalse();
            nullFirst.Should().BeFalse();
            bothNull.Should().BeTrue();

            //check != 
            bool nonNullFirstNotEqual = (biggerDistance != null);
            bool nullFirstNotEqual = (null != biggerDistance);
            bool bothNullNotEqual = (null != null);

            nonNullFirstNotEqual.Should().BeTrue();
            nullFirstNotEqual.Should().BeTrue();
            bothNullNotEqual.Should().BeFalse();

            //check equals (other way should throw a nullPointerException)
            bool nullSecond = biggerDistance.Equals(null);

            nullSecond.Should().BeFalse();
        }

        /// <summary>
        /// Tests all equality operators
        /// </summary>
        [Test()]
        public void Dimension_ComparisonOperatorTest()
        {
            // arrange
            Distance biggerDistance = new Distance(new Inch(), 14.1875);
            Distance smallerDistance = new Distance("1' 2 1/16\"");
            Distance equivalentbiggerDistance = new Distance(new Millimeter(), 360.3625);

            // assert
            (smallerDistance < biggerDistance).Should().Be(true);
            (biggerDistance < smallerDistance).Should().Be(false);

            (biggerDistance > smallerDistance).Should().Be(true);
            (smallerDistance > biggerDistance).Should().Be(false);
        }


        [Test()]
        public void Distance_EqualsWithinPassedAcceptedDeviation()
        {
            // arrange
            Distance distance = new Distance(new Inch(), 5);
            Distance equivalentDistance = new Distance(new Inch(), 5.03125);
            Distance notequivalentDistance = new Distance(new Inch(), 5.03126);


            (equivalentDistance.EqualsWithinDeviationConstant(distance, distance.DeviationAsConstant)).Should().Be(true);
            (notequivalentDistance.EqualsWithinDeviationConstant(distance, distance.DeviationAsConstant)).Should().Be(false);
        }



        /// <summary>
        /// Tests GetHashCodeOperation
        /// </summary>
        [Test()]
        public void Distance_GetHashCode()
        {
            // arrange
            Distance Distance = new Distance(new Millimeter(), 14.1875);
            double number = 14.1875;

            // act
            int DistanceHashCode = Distance.GetHashCode();

            int hashCode = number.GetHashCode();

            // assert
            hashCode.ShouldBeEquivalentTo(DistanceHashCode);
        }

        /// <summary>
        /// Tests toString
        /// </summary>
        [Test()]
        public void Distance_ToString()
        {
            // arrange
            Distance Distance = new Distance(new Millimeter(), 14.1875);
            Distance Distance2 = new Distance(new Millimeter(), 0);

            //should come back rounded due to DeviationConstant
            // act            

            // assert
            Distance.ToString().Should().Be("14.188 Millimeters");
            Distance2.ToString().Should().Be("0 Millimeters");
        }

        /// <summary>
        /// Tests CompareTo implementation
        /// </summary>
        [Test()]
        public void Distance_CompareTo()
        {
            // arrange
            Distance smallDistance = new Distance(new Millimeter(), 1);
            Distance mediumDistance = new Distance(new Foot(), 1);
            Distance largeDistance = new Distance(new Kilometer(), 1);

            //Act & Assert
            smallDistance.CompareTo(mediumDistance).Should().Be(-1);
            mediumDistance.CompareTo(smallDistance).Should().Be(1);
            largeDistance.CompareTo(largeDistance).Should().Be(0);
        }

        [Test()]
        public void Distance_Incrementing()
        {

            //Static constants
            Distance oneFoot = 1.FromFeetToDistance();

            //increments
            oneFoot += oneFoot; //should be two feet

            (oneFoot == new Distance(new Foot(), 2)).Should().BeTrue();

            oneFoot -= oneFoot; //should be zero feet

            (oneFoot == new Distance(new Foot(), 0)).Should().BeTrue();

        }

        [Test()]
        public void Distance_SelfConversionTest()
        {
            Distance testInstance = new Distance(new Mile(), 1);
            testInstance.AsMiles().Should().Be(1);
        }


        /// <summary>
        /// Tests intuitiveness. If this compiles then these "pass"
        /// </summary>
        [Test()]
        public void Distance_Intuitiveness()
        {
            //zero constructor
            Distance zero = new Distance();

            //simple constructor
            Distance smallDistance = new Distance(new Millimeter(), 1);
            Distance mediumDistance = new Distance(new Foot(), 1);
            Distance largeDistance = new Distance(new Kilometer(), 1);

            //copy constructor
            Distance copy = new Distance(smallDistance);

            //comparisons
            if (copy > zero)
            {

            }

            if (zero == new Distance())
            {

            }

            if (zero >= new Distance())
            {

            }


            //Math operations
            Distance distance4 = smallDistance + largeDistance;
            Distance doubleDistance = mediumDistance ^ 2;

            //absolute value
            Distance positiveDistance = (new Distance(new Inch(), -1).AbsoluteValue());

            //Static constants
            Distance oneFoot = 1.FromFeetToDistance();

            //increments
            oneFoot += oneFoot; // 2 feet
            oneFoot -= oneFoot; // 0 feet

            //we do not implement ++ and -- because that would break our "all units simultaneously" abstraction.
            // oneFoot++;
            // oneFoot--;

            //User defined equality strategies
            EqualityStrategy<IDistanceType> userStrategy = (d1, d2) => { return true; };

            oneFoot.EqualsWithinEqualityStrategy(positiveDistance, userStrategy);

            // ToString override
            oneFoot.ToString();

            List<Distance> distances = new List<Distance> { oneFoot, positiveDistance, distance4, zero };

            foreach (var distance in distances)
            {
                distance.ToString();
            }


        }

    }
}
