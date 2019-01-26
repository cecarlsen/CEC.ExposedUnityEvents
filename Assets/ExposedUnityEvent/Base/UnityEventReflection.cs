/*
	Copyright (c) 2019 Carl Emil Carlsen
	https://github.com/cecarlsen/ExposedUnityEvent
	
	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.
*/

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace ExposedUnityEventTools
{
	public static class UnityEventReflection
	{
		// UnityEvent<T> source: https://github.com/Unity-Technologies/UnityCsReference/blob/54dd67f09e090b1ad5ba1f55886f327106406b0c/Runtime/Export/UnityEvent_1.cs

		static FieldInfo _persistentCallsField;
		static FieldInfo _callsField;
		static MethodInfo _addListenerMethod;
		static MethodInfo _removeListenersMethod;
		static MethodInfo _registerEventPersistentListenerMethod;
		static PropertyInfo _countProperty;

		static readonly object[] _noArgs = new object[] { };
		static readonly object[] _twoArgs = new object[2];
		static readonly object[] _threeArgs = new object[3];

		// Private field of type PersistentCallGroup inside UnityEventBase http://www.codeforge.com/read/467440/UnityEventBase.cs__html
		const string _persistentCallsFieldName = "m_PersistentCalls";

		// Private field of type InvocableCallList inside UnityEventBase http://www.codeforge.com/read/467440/UnityEventBase.cs__html
		const string _callsFieldName = "m_Calls";

		// Private field of type List<PersistentCall> inside PersistentCallGroup https://github.com/jamesjlinden/unity-decompiled/blob/96fb16e2eb6fff1acf3d4e25fa713defb3d17999/UnityEngine/UnityEngine/Events/PersistentCallGroup.cs
		const string _persistentCallGroupCallsFieldName = "m_Calls";

		// Public methods inside PersistentCallGroup https://github.com/jamesjlinden/unity-decompiled/blob/96fb16e2eb6fff1acf3d4e25fa713defb3d17999/UnityEngine/UnityEngine/Events/PersistentCallGroup.cs
		const string _registerEventPersistentListenerName = "RegisterEventPersistentListener";
		const string _addListenerMethodName = "AddListener";
		const string _removeListenesMethodName = "RemoveListeners";

		// Public property inside InvocableCallList.
		const string _countPropertyName = "Count";


		public static void AddPersistentListner( object persistentCallsObject, int newIndex, UnityEngine.Object targetObject, MethodInfo targetMethod )
		{
			// Add a new listerner field.
			_addListenerMethod.Invoke( persistentCallsObject, _noArgs );

			// Register it.
			_threeArgs[0] = newIndex;
			_threeArgs[1] = targetObject;
			_threeArgs[2] = targetMethod.Name;
			_registerEventPersistentListenerMethod.Invoke( persistentCallsObject, _threeArgs );
		}


		public static void RemovePersistentListener( object persistentCallsObject, UnityEngine.Object targetObject, MethodInfo targetMethod )
		{
			// Remove listner.
			_twoArgs[0] = targetObject;
			_twoArgs[1] = targetMethod.Name;
			_removeListenersMethod.Invoke( persistentCallsObject, _twoArgs );
		}


		public static int GetEventCount( object callsObject )
		{
			return (int) _countProperty.GetValue( callsObject, _noArgs );
		}


		public static bool TryAccessPersistentCalls( UnityEventBase e, ref object persistentCallsObject )
		{
			if( _persistentCallsField == null ) _persistentCallsField = typeof( UnityEventBase ).GetField( _persistentCallsFieldName, BindingFlags.NonPublic | BindingFlags.Instance );
			if( _persistentCallsField == null ) return LogNoLongerWorking( _persistentCallsFieldName );

			if( persistentCallsObject == null ) persistentCallsObject = _persistentCallsField.GetValue( e );
			if( persistentCallsObject == null ) return LogNoLongerWorking( "persistentCallsObject" );

			if( _addListenerMethod == null || _removeListenersMethod == null || _registerEventPersistentListenerMethod == null ) {
				MethodInfo[] persistentCallsPublicMethods = persistentCallsObject.GetType().GetMethods( BindingFlags.Public | BindingFlags.Instance );

				if( _addListenerMethod == null ) _addListenerMethod = Array.Find( persistentCallsPublicMethods, a => a.Name.Contains( _addListenerMethodName ) );
				if( _addListenerMethod == null ) return LogNoLongerWorking( _addListenerMethodName );

				if( _removeListenersMethod == null ) _removeListenersMethod = Array.Find( persistentCallsPublicMethods, a => a.Name.Contains( _removeListenesMethodName ) );
				if( _removeListenersMethod == null ) return LogNoLongerWorking( _removeListenesMethodName );

				if( _registerEventPersistentListenerMethod == null ) _registerEventPersistentListenerMethod = Array.Find( persistentCallsPublicMethods, a => a.Name.Contains( _registerEventPersistentListenerName ) );
				if( _registerEventPersistentListenerMethod == null ) return LogNoLongerWorking( _registerEventPersistentListenerName );
			}

			return true;
		}


		public static bool TryAccessCalls( UnityEventBase e, ref object callsObject )
		{
			if( _callsField == null ) _callsField = typeof( UnityEventBase ).GetField( _callsFieldName, BindingFlags.NonPublic | BindingFlags.Instance );
			if( _callsField == null ) return LogNoLongerWorking( _callsFieldName );

			if( callsObject == null ) callsObject = _callsField.GetValue( e );
			if( callsObject == null ) return LogNoLongerWorking( "callsObject" );

			if( _countProperty == null ) {
				PropertyInfo[] callsPublicProperties = callsObject.GetType().GetProperties( BindingFlags.Public | BindingFlags.Instance );

				if( _countProperty == null ) _countProperty = Array.Find( callsPublicProperties, a => a.Name.Contains( _countPropertyName ) );
				if( _countProperty == null ) return LogNoLongerWorking( _countPropertyName );
			}

			return true;
		}


		static bool LogNoLongerWorking( string missingThing )
		{
			Debug.LogError( "<b>[ExposedUnityEvent]</b> Hack no longer working, most likely because of a update to Unity. Not found: \n" + missingThing );
			return false;
		}
	}
}