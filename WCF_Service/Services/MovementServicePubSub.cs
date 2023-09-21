using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.Contracts;

namespace WCF_Service.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public partial class MovementService
    {
        public delegate void MovementChangeEventHandler(object sender, MovementsChangedEventArgs e);
        public static event MovementChangeEventHandler MovementsChanged;
        private static Queue<Movement> movements = new Queue<Movement>();

        IPubSubCallback callback = null;
        MovementChangeEventHandler movementChangeEventHandler = null;

        public void Publish(Movement value)
        {
            movements.Enqueue(value);
            MovementsChanged(this, new MovementsChangedEventArgs(movements));
        }

        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<IPubSubCallback>();
            movementChangeEventHandler = new MovementChangeEventHandler(OnMovementsChanged);
            MovementsChanged += movementChangeEventHandler;
        }

        private void OnMovementsChanged(object sender, MovementsChangedEventArgs e)
        {
            callback.MovementsChanged(e.Movements);
        }

        public void Unsubscribe()
        {
            MovementsChanged -= movementChangeEventHandler;
        }
    }
}
