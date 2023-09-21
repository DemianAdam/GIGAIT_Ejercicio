using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.MovementService;

namespace UnitTest
{
    public class MovementCallback : IMovementServiceCallback
    {
        public void MovementsChanged(Queue<Movement> value)
        {
            Assert.IsNotEmpty(value);
        }
    }
}
