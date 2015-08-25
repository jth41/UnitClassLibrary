﻿using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UnitClassLibrary
{
    /// <summary>
    /// Class used for storing Distances that may need to be accessed in a different measurement system
    /// 
    /// For an explanation of why this class is immutable: http://codebetter.com/patricksmacchia/2008/01/13/immutable-types-understand-them-and-use-them/
    /// 
    /// <example>
    /// decimal inches into architectural notation
    /// 
    /// double inches = 14.1875
    /// Distance dm = new Distance(DistanceTypes.Inch, inches);
    /// 
    /// Print(dm.Architectural)
    /// 
    /// ========Output==========
    /// 1'2 3/16"
    /// 
    /// </example>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Distance
    {
        #region _fields and Internal Properties

        /// <summary>
        /// This property must be internal to allow for our Just-In-Time conversions to work with the GetValue() method
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public DistanceType InternalUnitType
        {
            get { return _internalUnitType; }
            private set { _internalUnitType = value; }
        }
        private DistanceType _internalUnitType;

        /// <summary>
        /// The actual value of the stored unit. the 5 in "5 kilometers"
        /// </summary> 
        [JsonProperty(PropertyName = "intrinsicValue")]
        private double _intrinsicValue;
        public double IntrinsicValue 
        { 
            get { return _intrinsicValue; }
            private set { _intrinsicValue = value; }
        }

        /// <summary>
        /// The strategy by which this Distance will be compared to another Distance
        /// </summary>
        [XmlIgnore]
        public DistanceEqualityStrategy EqualityStrategy
        {
            get { return _equalityStrategy; }
            set { _equalityStrategy = value; } 
        }
        private DistanceEqualityStrategy _equalityStrategy;

        #endregion

        #region Constructors

        /// <summary>
        /// Zero Constructor
        /// </summary>
        /// <param name="passedStrategy">Strategy to compare equality by</param>
        public Distance(DistanceEqualityStrategy passedStrategy = null)
        {
            _intrinsicValue = 0;
            _internalUnitType = DistanceType.Inch;
            _equalityStrategy = _chooseDefaultOrPassedStrategy(passedStrategy);
        }

        /// <summary>
        /// Accepts any valid architectural string value for input
        /// </summary>
        /// <param name="passedArchitecturalString"> Architecturally formatted string to create distance from</param>
        /// <param name="passedStrategy">Strategy to compare equality by</param>
        public Distance(string passedArchitecturalString, DistanceEqualityStrategy passedStrategy = null)
        {
            //we will always make the internal unit type of a passed String Inches 
            _internalUnitType = DistanceType.Inch;
            if (passedArchitecturalString == "")
            {
                _intrinsicValue = 0;
            }
            else
            {
                _intrinsicValue = _getArchitecturalStringAsNumberOfInches(passedArchitecturalString);
            }
            _equalityStrategy = _chooseDefaultOrPassedStrategy(passedStrategy);
        }

        /// <summary>
        /// The standard Unit Constructor that takes the value and the unit type that describes it.
        /// </summary>
        /// <param name="passedDistanceType">The unit of distance the input is in</param>
        /// <param name="passedInput">value of the distance</param>
        /// <param name="passedStrategy">Strategy to compare equality by</param>
        [JsonConstructor]
        public Distance(DistanceType internalUnitType, double intrinsicValue, DistanceEqualityStrategy equalityStrategy = null)
        {
            _intrinsicValue = intrinsicValue;
            _internalUnitType = internalUnitType;
            _equalityStrategy = _chooseDefaultOrPassedStrategy(equalityStrategy);
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="passedDistance">Distance objet to copy</param>
        public Distance(Distance passedDistance)
        {
            _intrinsicValue = passedDistance._intrinsicValue;
            _internalUnitType = passedDistance._internalUnitType;
            _equalityStrategy = passedDistance._equalityStrategy;
        }

        #endregion

        #region helper _methods

        private static DistanceEqualityStrategy _chooseDefaultOrPassedStrategy(DistanceEqualityStrategy passedStrategy)
        {
            if (passedStrategy == null)
            {
                return EqualityStrategyImplementations.DefaultConstantEquality;
            }
            else
            {
                return passedStrategy;
            }
        }

        private static double _getArchitecturalStringAsNumberOfInches(string passedArchitecturalString)
        {
            return ConvertArchitectualStringtoUnit(DistanceType.Inch, passedArchitecturalString);
        }

        private double _retrieveIntrinsicValueAsDesiredExternalUnit(DistanceType toDistanceType)
        {
            return ConvertDistance(_internalUnitType, _intrinsicValue, toDistanceType);
        }

        private string _retrieveIntrinsicValueAsArchitecturalString()
        {
            return ConvertDistanceIntoArchitecturalString(this);
        }
        #endregion
    }
}
