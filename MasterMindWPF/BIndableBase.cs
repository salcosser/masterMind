using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class BindableBase : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual bool Set<T>(ref T storage, T value, Expression<Func<T>> propertyExpression) {
        if (Equals(storage, value))
            return false;

        storage = value;
        RaisePropertyChanged(propertyExpression);

        return true;
    }

    protected virtual bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
        if (Equals(storage, value))
            return false;

        storage = value;
        RaisePropertyChanged(propertyName);

        return true;
    }
    protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
        var propertyName = GetPropertyName(propertyExpression);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression) {
        if (propertyExpression == null)
            throw new ArgumentNullException(nameof(propertyExpression));

        if (!(propertyExpression.Body is MemberExpression body))
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property.");

        var property = body.Member as System.Reflection.PropertyInfo;
        if (property == null)
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property.");

        return property.Name;
    }
}
