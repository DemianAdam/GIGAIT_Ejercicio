using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableStack(IEnumerable<T> values) : base(values)
        {

        }

        public ObservableStack() : base()
        {

        }

        public new T Pop()
        {
            var item = base.Pop();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            return item;
        }

        public new void Push(T item)
        {
            base.Push(item);
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
