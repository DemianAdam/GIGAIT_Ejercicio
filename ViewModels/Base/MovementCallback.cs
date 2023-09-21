using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MovementService;

namespace ViewModels.Base
{
    public class MovementCallback : IMovementServiceCallback
    {
        public InstanceContext InstanceContext { get; }
        private readonly Action<Queue<Movement>> action;
        public MovementCallback(Action<Queue<Movement>> action)
        {
            InstanceContext = new InstanceContext(this);
            this.action = action;
        }

        public void MovementsChanged(Queue<Movement> value)
        {
            action.Invoke(value);
        }
    }
}
