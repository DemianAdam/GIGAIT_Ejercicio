﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViewModels.MovementService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SqlUniqueFault", Namespace="http://schemas.datacontract.org/2004/07/WCF_Service.Faults")]
    [System.SerializableAttribute()]
    public partial class SqlUniqueFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Details {
            get {
                return this.DetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.DetailsField, value) != true)) {
                    this.DetailsField = value;
                    this.RaisePropertyChanged("Details");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MovementService.IMovementService", CallbackContract=typeof(ViewModels.MovementService.IMovementServiceCallback))]
    public interface IMovementService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Add", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/AddResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ViewModels.MovementService.SqlUniqueFault), Action="http://tempuri.org/ICRUDServiceOf_Movement/AddSqlUniqueFaultFault", Name="SqlUniqueFault", Namespace="http://schemas.datacontract.org/2004/07/WCF_Service.Faults")]
        int Add(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Add", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Update", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/UpdateResponse")]
        void Update(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Update", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/UpdateResponse")]
        System.Threading.Tasks.Task UpdateAsync(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Delete", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/DeleteResponse")]
        void Delete(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/Delete", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(Models.Movement entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/SelectAll", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/SelectAllResponse")]
        System.Collections.Generic.List<Models.Movement> SelectAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/SelectAll", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/SelectAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Movement>> SelectAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/SelectById", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/SelectByIdResponse")]
        Models.Movement SelectById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICRUDServiceOf_Movement/SelectById", ReplyAction="http://tempuri.org/ICRUDServiceOf_Movement/SelectByIdResponse")]
        System.Threading.Tasks.Task<Models.Movement> SelectByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Subscribe")]
        void Subscribe();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Subscribe")]
        System.Threading.Tasks.Task SubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Unsubscribe")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Unsubscribe")]
        System.Threading.Tasks.Task UnsubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Publish")]
        void Publish(Models.Movement value);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/Publish")]
        System.Threading.Tasks.Task PublishAsync(Models.Movement value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMovementServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPubSubServiceOf_Movement/MovementsChanged")]
        void MovementsChanged(System.Collections.Generic.Queue<Models.Movement> value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMovementServiceChannel : ViewModels.MovementService.IMovementService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MovementServiceClient : System.ServiceModel.DuplexClientBase<ViewModels.MovementService.IMovementService>, ViewModels.MovementService.IMovementService {
        
        public MovementServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MovementServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MovementServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MovementServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MovementServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public int Add(Models.Movement entity) {
            return base.Channel.Add(entity);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(Models.Movement entity) {
            return base.Channel.AddAsync(entity);
        }
        
        public void Update(Models.Movement entity) {
            base.Channel.Update(entity);
        }
        
        public System.Threading.Tasks.Task UpdateAsync(Models.Movement entity) {
            return base.Channel.UpdateAsync(entity);
        }
        
        public void Delete(Models.Movement entity) {
            base.Channel.Delete(entity);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(Models.Movement entity) {
            return base.Channel.DeleteAsync(entity);
        }
        
        public System.Collections.Generic.List<Models.Movement> SelectAll() {
            return base.Channel.SelectAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Movement>> SelectAllAsync() {
            return base.Channel.SelectAllAsync();
        }
        
        public Models.Movement SelectById(int id) {
            return base.Channel.SelectById(id);
        }
        
        public System.Threading.Tasks.Task<Models.Movement> SelectByIdAsync(int id) {
            return base.Channel.SelectByIdAsync(id);
        }
        
        public void Subscribe() {
            base.Channel.Subscribe();
        }
        
        public System.Threading.Tasks.Task SubscribeAsync() {
            return base.Channel.SubscribeAsync();
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public System.Threading.Tasks.Task UnsubscribeAsync() {
            return base.Channel.UnsubscribeAsync();
        }
        
        public void Publish(Models.Movement value) {
            base.Channel.Publish(value);
        }
        
        public System.Threading.Tasks.Task PublishAsync(Models.Movement value) {
            return base.Channel.PublishAsync(value);
        }
    }
}
