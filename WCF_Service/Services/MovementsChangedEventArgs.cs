using Models;
using System;
using System.Collections.Generic;

namespace WCF_Service.Services
{
    public class MovementsChangedEventArgs : EventArgs
    {
        public MovementsChangedEventArgs(Queue<Movement> movements)
        {
            Movements = movements;
        }

        public Queue<Movement> Movements { get; }
    }
}
