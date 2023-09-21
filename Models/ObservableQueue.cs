using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableQueue(IEnumerable<T> values) : base(values)
        {

        }

        public ObservableQueue() : base()
        {

        }

        public new T Dequeue()
        {
            var item = base.Dequeue();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            return item;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        public new void Clear()
        {
            base.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }
    }   
    
}
