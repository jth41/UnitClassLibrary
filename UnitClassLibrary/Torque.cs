﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace UnitClassLibrary
{
    public class Torque : Moment
    {
        public Torque(ForceUnit passeForce, Distance passedDistance)
            : base(passeForce, passedDistance)
        {

        }
    }
}
