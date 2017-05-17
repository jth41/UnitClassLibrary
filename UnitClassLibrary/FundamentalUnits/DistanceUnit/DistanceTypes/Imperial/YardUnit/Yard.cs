﻿/*
    This file is part of Unit Class Library.
    Copyright (C) 2016 Paragon Component Systems, LLC.

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.InchUnit;

namespace UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.YardUnit
{
    public class Yard : DistanceType
    {
        public override string AsStringSingular()
        {
            return "yd";
        }

        public override double ConversionFactor => 36;

        public override double DefaultErrorMargin => new Inch().DefaultErrorMargin / ConversionFactor;
    }

    public static class YardExtensions
    {
        public static Distance FromYardsToDistance(this double passedDouble)
        {
            return new Distance(new Yard(), passedDouble);
        }

        public static Distance FromYardsToDistance(this int passedint)
        {
            return new Distance(new Yard(), passedint);
        }

        public static double AsYards(this Distance passedDistance)
        {
            return passedDistance.ConversionFromThisTo(new Yard());
        }
    }
}
