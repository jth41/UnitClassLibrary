﻿using System.Collections.Generic;
using UnitClassLibrary.DistanceUnit;
using UnitClassLibrary.GenericUnit;
using UnitClassLibrary.SpeedUnit.SpeedTypes;
using UnitClassLibrary.TimeUnit;

namespace UnitClassLibrary.SpeedUnit
{
    public class Speed : GenericUnit.GenericUnit
    {
        public Speed(ISpeedType SpeedType, double passedDouble)
            : base(new List<KeyValuePair<double, IUnitType>>() { new KeyValuePair<double, IUnitType>(passedDouble, SpeedType) }, new List<KeyValuePair<double, IUnitType>>())
        {
        }

        public Speed(Distance distance, Time time)
            : base(new List<GenericUnit.GenericUnit>() { distance }, new List<GenericUnit.GenericUnit>(){ time})
        {
        }

        private Speed(GenericUnit.GenericUnit toCopy)
            : base(toCopy)
        {
        }

        new public Speed Negate()
        {
            return new Speed(base.Negate());
        }
    }
}
