using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ViewModels.CustomerViewModel;
using ViewModels.MovementService;

namespace ViewModels.Base
{
    /// <summary>
    /// MovementCallback class. This class represents the callback for the MovementService.
    /// </summary>
    public class MovementCallback : IMovementServiceCallback
    {
        public InstanceContext InstanceContext { get; }
        public event Action<IEnumerable<Movement>> DataRecived;
        public MovementCallback()
        {
            InstanceContext = new InstanceContext(this);
        }


        public void MovementsChanged(Queue<Movement> value)
        {
           // viewModelBase.UpdateMovements(value);
            DataRecived?.Invoke(value);
        }
    }
}
