﻿/*
    This file is part of Unit Class Library.
    Copyright (C) 2017 Paragon Component Systems, LLC.

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

using UnitClassLibrary.DistanceUnit.DistanceTypes;

using UnitClassLibrary.TimeUnit.TimeTypes;

namespace UnitClassLibrary.SpeedUnit.SpeedTypes
{
    public abstract class SpeedType : AbstractDerivedUnitType
    {
        public static Speed operator *(double d, SpeedType type)
        {
            return new Speed(d, type);
        }

        public static Speed operator *(int m, SpeedType type)
        {
            return new Speed(m, type);
        }
    }
}