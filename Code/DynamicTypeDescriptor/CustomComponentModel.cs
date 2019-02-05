﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Design;
using Scm = System.ComponentModel;

// Dynamic Type Description Framework for PropertyGrid
// Mizan Rahman, 25 Feb 2013
// https://www.codeproject.com/Articles/415070/%2FArticles%2F415070%2FDynamic-Type-Description-Framework-for-PropertyGri

namespace DynamicTypeDescriptor
{
  internal class PropertyValueUIService : IPropertyValueUIService
  {
    private PropertyValueUIHandler m_ValueUIHandler;
    private EventHandler m_NotifyHandler;

    public PropertyValueUIService()
    {
    }

    /// <summary>
    /// Adds or removes an event handler that will be invoked
    /// when the global list of PropertyValueUIItems is modified.
    /// </summary>
    event EventHandler IPropertyValueUIService.PropertyUIValueItemsChanged
    {
      add
      {
        lock (this)
          this.m_NotifyHandler += value;
      }
      remove
      {
        lock (this)
          this.m_NotifyHandler -= value;
      }
    }

    /// <summary>
    /// Tell the IPropertyValueUIService implementation that the global list of PropertyValueUIItems has been modified.
    /// </summary>
    void IPropertyValueUIService.NotifyPropertyValueUIItemsChanged()
    {
      if (this.m_NotifyHandler != null)
      {
        this.m_NotifyHandler(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Adds a PropertyValueUIHandler to this service.  When GetPropertyUIValueItems is
    /// called, each handler added to this service will be called and given the opportunity
    /// to add an icon to the specified property.
    /// </summary>
    /// <param name="newHandler"></param>
    void IPropertyValueUIService.AddPropertyValueUIHandler( PropertyValueUIHandler newHandler )
    {
      if (newHandler == null)
      {
        throw new ArgumentNullException("newHandler");
      }
      lock (this)
        this.m_ValueUIHandler = (PropertyValueUIHandler)Delegate.Combine(this.m_ValueUIHandler, newHandler);
    }

    /// <summary>
    /// Removes a PropertyValueUIHandler to this service.  When GetPropertyUIValueItems is
    /// called, each handler added to this service will be called and given the opportunity
    /// to add an icon to the specified property.
    /// </summary>
    /// <param name="newHandler"></param>
    void IPropertyValueUIService.RemovePropertyValueUIHandler( PropertyValueUIHandler newHandler )
    {
      if (newHandler == null)
      {
        throw new ArgumentNullException("newHandler");
      }

      this.m_ValueUIHandler = (PropertyValueUIHandler)Delegate.Remove(this.m_ValueUIHandler, newHandler);
    }

    /// <summary>
    /// Gets all the PropertyValueUIItems that should be displayed on the given property.
    /// For each item returned, a glyph icon will be aded to the property.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="propDesc"></param>
    /// <returns></returns>
    PropertyValueUIItem[] IPropertyValueUIService.GetPropertyUIValueItems( Scm.ITypeDescriptorContext context, Scm.PropertyDescriptor propDesc )
    {

      if (propDesc == null)
      {
        throw new ArgumentNullException("propDesc");
      }

      if (this.m_ValueUIHandler == null)
      {
        return new PropertyValueUIItem[0];
      }


      lock (this)
      {
        ArrayList result = new ArrayList( );

        this.m_ValueUIHandler(context, propDesc, result);

        return (PropertyValueUIItem[])result.ToArray(typeof(PropertyValueUIItem));
      }

    }
  }


  internal sealed class SimpleSite : Scm.ISite, IServiceProvider
  {
    public Scm.IComponent Component
    {
      get;
      set;
    }
    private readonly Scm.IContainer container = new Scm.Container( );
    Scm.IContainer Scm.ISite.Container
    {
      get
      {
        return container;
      }
    }
    public bool DesignMode
    {
      get;
      set;
    }
    public string Name
    {
      get;
      set;
    }
    private Dictionary<Type, object> services;
    public void AddService<T>( T service ) where T : class
    {
      if (services == null)
        services = new Dictionary<Type, object>( );
      services[typeof(T)] = service;
    }
    public void RemoveService<T>() where T : class
    {
      if (services != null)
        services.Remove(typeof(T));
    }
    object IServiceProvider.GetService( Type serviceType )
    {
      object service;
      if (services != null && services.TryGetValue(serviceType, out service))
      {
        return service;
      }
      return null;
    }
  }

 


 
}
