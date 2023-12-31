﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// IPubSubCallback interface. This interface represents the callback for the IPubSubService.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceContract(CallbackContract = typeof(IPubSubCallback))]
    public interface IPubSubService<T>
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe();
        [OperationContract(IsOneWay = true)]
        void Unsubscribe();
        [OperationContract(IsOneWay = true)]
        void Publish(T value);
    }
}
