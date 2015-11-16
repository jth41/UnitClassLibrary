﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitClassLibrary.AngleUnit.AngleTypes
{
    class Degree : IAngleUnit
    {
        public override string AsStringPlural
        {
            get
            {
                return "Degrees";
            }
        }

        public override string AsStringSingular
        {
            get
            {
                return "Degree";
            }
        }

        public override double ConversionFactor
        {
            get
            {
                return 1.0;
            }
        }

        public override double DefaultErrorMargin_
        {
            get
            {
                return 1.0;
            }
        }
    }
}
