using System;
using System.ComponentModel;

namespace BinaryTreeSearch
{
    public abstract class ChangeListener : INotifyPropertyChanged, IDisposable
    {
        #region *** Members ***
        protected string _propertyName;
        #endregion


        #region *** Abstract Members ***
        protected abstract void Unsubscribe();
        #endregion


        #region *** INotifyPropertyChanged Members and Invoker ***
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region *** Disposable Pattern ***

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unsubscribe();
            }
        }

        ~ChangeListener()
        {
            Dispose(false);
        }

        #endregion


        #region *** Factory ***
        public static ChangeListener Create(INotifyPropertyChanged value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return Create(value, null);
        }

        public static ChangeListener Create(INotifyPropertyChanged value, string propertyName) => new InnerChangeListener(value as INotifyPropertyChanged, propertyName);
            
        #endregion
    }
}
