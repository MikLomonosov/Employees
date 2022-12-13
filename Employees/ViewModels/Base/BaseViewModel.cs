using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Employees.ViewModels.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {

        #region INotifyPropertyChanged
        /// <summary>
        /// Уведомления
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName]string property = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Dispose
        /// <summary>
        /// запуск сборщика
        /// </summary>
        private bool disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || disposed) return;
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
