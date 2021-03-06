﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysInventory.LogMessages.DataAccess.LINQ
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SysInventory")]
	public partial class SysInventoryLinqSqlContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertLog(Log instance);
    partial void UpdateLog(Log instance);
    partial void DeleteLog(Log instance);
    #endregion
		
		public SysInventoryLinqSqlContextDataContext() : 
				base(global::SysInventory.LogMessages.Properties.Settings.Default.SysInventoryConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SysInventoryLinqSqlContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SysInventoryLinqSqlContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SysInventoryLinqSqlContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SysInventoryLinqSqlContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Log> Logs
		{
			get
			{
				return this.GetTable<Log>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.LogClear")]
		public int LogClear([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="UniqueIdentifier")] System.Nullable<System.Guid> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.LogMessageAdd")]
		public int LogMessageAdd([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(255)")] string podName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(255)")] string hostname, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> lvl, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Text")] string msg)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), podName, hostname, lvl, msg);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[Log]")]
	public partial class Log : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _LogId;
		
		private string _Message;
		
		private System.Guid _DeviceFk;
		
		private int _Severity;
		
		private System.DateTime _CreatedAt;
		
		private bool _Confirmed;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnLogIdChanging(System.Guid value);
    partial void OnLogIdChanged();
    partial void OnMessageChanging(string value);
    partial void OnMessageChanged();
    partial void OnDeviceFkChanging(System.Guid value);
    partial void OnDeviceFkChanged();
    partial void OnSeverityChanging(int value);
    partial void OnSeverityChanged();
    partial void OnCreatedAtChanging(System.DateTime value);
    partial void OnCreatedAtChanged();
    partial void OnConfirmedChanging(bool value);
    partial void OnConfirmedChanged();
    #endregion
		
		public Log()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LogId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid LogId
		{
			get
			{
				return this._LogId;
			}
			set
			{
				if ((this._LogId != value))
				{
					this.OnLogIdChanging(value);
					this.SendPropertyChanging();
					this._LogId = value;
					this.SendPropertyChanged("LogId");
					this.OnLogIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this.OnMessageChanging(value);
					this.SendPropertyChanging();
					this._Message = value;
					this.SendPropertyChanged("Message");
					this.OnMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeviceFk", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid DeviceFk
		{
			get
			{
				return this._DeviceFk;
			}
			set
			{
				if ((this._DeviceFk != value))
				{
					this.OnDeviceFkChanging(value);
					this.SendPropertyChanging();
					this._DeviceFk = value;
					this.SendPropertyChanged("DeviceFk");
					this.OnDeviceFkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Severity", DbType="Int NOT NULL")]
		public int Severity
		{
			get
			{
				return this._Severity;
			}
			set
			{
				if ((this._Severity != value))
				{
					this.OnSeverityChanging(value);
					this.SendPropertyChanging();
					this._Severity = value;
					this.SendPropertyChanged("Severity");
					this.OnSeverityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedAt", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedAt
		{
			get
			{
				return this._CreatedAt;
			}
			set
			{
				if ((this._CreatedAt != value))
				{
					this.OnCreatedAtChanging(value);
					this.SendPropertyChanging();
					this._CreatedAt = value;
					this.SendPropertyChanged("CreatedAt");
					this.OnCreatedAtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Confirmed", DbType="Bit NOT NULL")]
		public bool Confirmed
		{
			get
			{
				return this._Confirmed;
			}
			set
			{
				if ((this._Confirmed != value))
				{
					this.OnConfirmedChanging(value);
					this.SendPropertyChanging();
					this._Confirmed = value;
					this.SendPropertyChanged("Confirmed");
					this.OnConfirmedChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
