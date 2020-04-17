using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BinaryTreeSearch
{
    public class InnerChangeListener : ChangeListener
    {
        #region *** Members ***
        protected static readonly Type _inotifyType = typeof(INotifyPropertyChanged);

        private readonly INotifyPropertyChanged _value;
        private readonly Type _type;
        private readonly Dictionary<string, ChangeListener> _innerListeners = new Dictionary<string, ChangeListener>();
        #endregion


        #region *** Constructors ***
        public InnerChangeListener(INotifyPropertyChanged instance)
        {
            _value = instance ?? throw new ArgumentNullException("instance");
            _type = _value.GetType();

            Subscribe();
        }

        public InnerChangeListener(INotifyPropertyChanged instance, string propertyName)
            : this(instance)
        {
            _propertyName = propertyName;
        }
        #endregion


        #region *** Private Methods ***
        private void Subscribe()
        {
            _value.PropertyChanged += new PropertyChangedEventHandler(Value_PropertyChanged);

            var query =
                from property
                in _type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where _inotifyType.IsAssignableFrom(property.PropertyType)
                select property;

            foreach (var property in query)
            {
                // Declare property as known "Inner", then register it
                _innerListeners.Add(property.Name, null);
                ResetInnerListener(property.Name);
            }
        }


        /// <summary>
        /// Resets known (must exist in innerren collection) inner event handlers
        /// </summary>
        /// <param name="propertyName">Name of known inner property</param>
        private void ResetInnerListener(string propertyName)
        {
            if (_innerListeners.ContainsKey(propertyName))
            {
                // Unsubscribe if existing
                if (_innerListeners[propertyName] != null)
                {
                    _innerListeners[propertyName].PropertyChanged -= new PropertyChangedEventHandler(Inner_PropertyChanged);

                    // Should unsubscribe all events
                    _innerListeners[propertyName].Dispose();
                    _innerListeners[propertyName] = null;
                }

                var property = _type.GetProperty(propertyName);
                if (property == null)
                    throw new InvalidOperationException(string.Format("Was unable to get '{0}' property information from Type '{1}'", propertyName, _type.Name));

                object newValue = property.GetValue(_value, null);

                // Only recreate if there is a new value
                if (newValue != null)
                {
                    if (newValue is INotifyPropertyChanged)
                    {
                        _innerListeners[propertyName] =
                            new InnerChangeListener(newValue as INotifyPropertyChanged, propertyName);
                    }

                    if (_innerListeners[propertyName] != null)
                        _innerListeners[propertyName].PropertyChanged += new PropertyChangedEventHandler(Inner_PropertyChanged);
                }
            }
        }
        #endregion


        #region *** Event Handler ***
        void Inner_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // First, reset inner on change, if required...
            ResetInnerListener(e.PropertyName);

            // ...then, notify about it
            OnPropertyChanged(e.PropertyName);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            // Special Formatting
            base.OnPropertyChanged(string.Format("{0}{1}{2}",
                _propertyName, _propertyName != null ? "." : null, propertyName));
        }
        #endregion


        #region *** Overrides ***
        /// <summary>
        /// Release all inner handlers and self handler
        /// </summary>
        protected override void Unsubscribe()
        {
            _value.PropertyChanged -= new PropertyChangedEventHandler(Value_PropertyChanged);

            foreach (var binderKey in _innerListeners.Keys)
            {
                if (_innerListeners[binderKey] != null)
                    _innerListeners[binderKey].Dispose();
            }

            _innerListeners.Clear();

            System.Diagnostics.Debug.WriteLine("InnerChangeListener '{0}' unsubscribed", _propertyName);
        }
        #endregion
    }
}
