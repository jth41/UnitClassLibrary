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
using System.Collections.Generic;
using System.Linq;

namespace UnitClassLibrary
{
    public interface IUnitType
    {
        double ConversionFactor { get; }
        UnitDimensions Dimensions();

        //double InitialErrorMargin(double initialValue);
        double DefaultErrorMargin { get; }

        string AsStringSingular();
        string AsStringPlural();      
    }
   
    public abstract class FundamentalUnitType : IUnitType
    {
        public abstract double DefaultErrorMargin { get; }
        public abstract double ConversionFactor { get; }

        public abstract string Type { get; }
        public abstract string AsStringSingular();
        public virtual string AsStringPlural()
        {
            return AsStringSingular() + "s";
        }

        public UnitDimensions Dimensions()
        {
            return new UnitDimensions(1.0, new List<FundamentalUnitType>() { this });
        }

        //public double InitialErrorMargin(double intrinsicValue)
        //{
        //    return DefaultErrorMargin;
        //}
    }

    public abstract class AbstractDerivedUnitType : IUnitType
    {
        public virtual double DefaultErrorMargin => 0.01*ConversionFactor;


        public abstract UnitDimensions Dimensions();
        public double ConversionFactor => Dimensions().ConversionFactor;

        public abstract string AsStringSingular();
        public virtual string AsStringPlural() { return AsStringSingular() + "s"; }

        //public virtual double InitialErrorMargin(double intrinsicValue)
        //{
        //    return Dimensions().InitialErrorMargin(intrinsicValue);
        //}

        public override string ToString()
        {
            return this.Dimensions().ToString();
        }
    }

    public class DerivedUnitType : AbstractDerivedUnitType
    {
        public override double DefaultErrorMargin { get; }

      
        private readonly UnitDimensions _dimensions;

        public override UnitDimensions Dimensions()
        {
            return _dimensions;
        }

        public DerivedUnitType()
        {
            this._dimensions = new UnitDimensions(1.0);
        }
        public DerivedUnitType(double scale, List<FundamentalUnitType> numerators, List<FundamentalUnitType> denominators = null)
            : this(new UnitDimensions(scale, numerators, denominators)) { }
        public DerivedUnitType(double scale, List<IUnitType> numerators, List<IUnitType> denominators = null) 
        {
            throw new NotImplementedException();
            //this._dimensions = new UnitDimensions(scale, numerators, denominators);
        }
        public DerivedUnitType(UnitDimensions dimensions)
        {
            this._dimensions = dimensions;
        }

        public DerivedUnitType(double scale, IUnitType numerator, IUnitType denominator)
        {
            this._dimensions = new UnitDimensions(scale, numerator, denominator);
        }

        #region Static Methods
        public static DerivedUnitType Multiply(IUnitType type1, IUnitType type2)
        {
            return new DerivedUnitType(type1.Dimensions().Multiply(type2.Dimensions()));    
        }

        public static DerivedUnitType Divide(IUnitType type1, IUnitType type2)
        {
            return new DerivedUnitType(type1.Dimensions().Divide(type2.Dimensions()));
        }

        public static DerivedUnitType Power(IUnitType unitType, int power)
        {
            return new DerivedUnitType(unitType.Dimensions().ToThe(power));
        }

        public override string AsStringSingular()
        { throw new NotImplementedException(); }

        public override string AsStringPlural()
        { throw new NotImplementedException(); }
        #endregion
    }

} 
