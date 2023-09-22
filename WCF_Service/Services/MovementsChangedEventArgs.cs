using Models;
using System;
using System.Collections.Generic;

namespace WCF_Service.Services
{
    /// <summary>
    /// MovementsChangedEventArgs class. This class represents the event arguments for the MovementsChanged event.
    /// </summary>
    public class MovementsChangedEventArgs : EventArgs
    {
        public MovementsChangedEventArgs(Queue<Movement> movements)
        {
            Movements = movements;
        }

        public Queue<Movement> Movements { get; }
    }
}
