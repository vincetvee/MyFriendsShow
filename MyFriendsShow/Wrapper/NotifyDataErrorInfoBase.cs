using MyFriendsShow.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyFriendsShow.Wrapper
{
    public class NotifyDataErrorInfoBase :ViewModelBase, INotifyDataErrorInfo
    {


        private Dictionary<string, List<string>> _errorByPropertyName
            = new Dictionary<string, List<string>>();
        public bool HasErrors => _errorByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorByPropertyName.ContainsKey(propertyName)
                ? _errorByPropertyName[propertyName]
                  : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected  virtual void AddError(string propertyName, string error)
        {
            if (!_errorByPropertyName.ContainsKey(propertyName))
            {
                _errorByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorByPropertyName[propertyName].Contains(error))
            {
                _errorByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorByPropertyName.ContainsKey(propertyName))
            {
                _errorByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
