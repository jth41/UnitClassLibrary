﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitClassLibrary;
using UnitClassLibrary.DerivedUnits.StressUnit;
using UnitClassLibrary.DistanceUnit;
using UnitClassLibrary.DistributedForceUnit;
using static UnitClassLibrary.DistributedForceUnit.PoundPerInch;
namespace UnitLibraryTests
{
    [TestFixture]
    class DerivedUnitTests
    {
        

        [Test]
        public void DerivedUnits_SpeedTest()
        {
            var total = Stress.ZeroStress;
            for (int i = 0; i < 1E7; i++)
            {
                var stress = new Stress(5, Stress.PSI);
                total += stress;
            }
         
            Assert.Pass();
        }

        [Test]
        public void FundamentalUnit_SpeedTest()
        {
            var total = Distance.ZeroDistance;
            for (int i = 0; i < 1E7; i++)
            {
                var distance = new Distance(5, Distance.Inches);
                total += distance;
            }

            Assert.Pass();
        }

        [Test]
        public void Measurement_SpeedTest()
        {
            var total = new Measurement();
            for (int i = 0; i < 1E7; i++)
            {
                var measurement = new Measurement(5);
                total += measurement;
            }

            Assert.Pass();
        }

        [Test]
        public void Double_SpeedTest()
        {
            var total = 0;
            for (int i = 0; i < 1E7; i++)
            {
                var five = 5;
                total += five;
            }

            Assert.Pass();
        }
    }
}
