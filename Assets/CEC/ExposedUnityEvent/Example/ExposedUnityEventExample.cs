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
using UnityEngine;
using CEC.ExposedUnityEvents;

namespace ExposedUnityEventTools
{
	public class ExposedUnityEventExample : MonoBehaviour
	{
		// Four events with different number of arguments.
		[SerializeField] ExposedEvent _event = new ExposedEvent();
		[SerializeField] ExposedFloatEvent _floatEvent = new ExposedFloatEvent();
		[SerializeField] ExposedFloatIntEvent _floatIntEvent = new ExposedFloatIntEvent();
		[SerializeField] ExposedFloatIntStringEvent _floatIntStringEvent = new ExposedFloatIntStringEvent();
		[SerializeField] ExposedFloatIntStringBoolEvent _floatIntStringBoolEvent = new ExposedFloatIntStringBoolEvent();

		// Just like with noemal UnityEvents, you have to extend them with your own type to have them exposed in the inspector.
		[Serializable] public class ExposedEvent : ExposedUnityEvent { }
		[Serializable] public class ExposedFloatEvent : ExposedUnityEvent<float> { }
		[Serializable] public class ExposedFloatIntEvent : ExposedUnityEvent<float, int> { }
		[Serializable] public class ExposedFloatIntStringEvent : ExposedUnityEvent<float, int, string> { }
		[Serializable] public class ExposedFloatIntStringBoolEvent : ExposedUnityEvent<float, int, string, bool> { }


		void Awake()
		{
			// Add and remove some persistent and runtime listeners, the log the count.

			// ExposedUnityEvent<T>.
			_event.AddPersistentListener( Recevice1 );
			_event.AddPersistentListener( Recevice2 );
			_event.AddPersistentListener( Recevice3 );
			_event.RemovePersistentListener( Recevice2 );
			_event.AddListener( Recevice1 );
			_event.AddListener( Recevice2 );
			_event.AddListener( Recevice3 );
			_event.RemoveListener( Recevice2 );
			Debug.Log( "_event RuntimeEventCount: " + _event.GetEventCount() );
			Debug.Log( "_event PersistentEventCount: " + _event.GetPersistentEventCount() );
			Debug.Log( "_event TotalEventCount: " + _event.GetTotalEventCount() );

			// ExposedUnityEvent<T>.
			_floatEvent.AddPersistentListener( ReceviceFloat1 );
			_floatEvent.AddPersistentListener( ReceviceFloat2 );
			_floatEvent.AddPersistentListener( ReceviceFloat3 );
			_floatEvent.RemovePersistentListener( ReceviceFloat2 );
			_floatEvent.AddListener( ReceviceFloat1 );
			_floatEvent.AddListener( ReceviceFloat2 );
			_floatEvent.AddListener( ReceviceFloat3 );
			_floatEvent.RemoveListener( ReceviceFloat2 );
			Debug.Log( "_floatEvent RuntimeEventCount: " + _floatEvent.GetEventCount() );
			Debug.Log( "_floatEvent PersistentEventCount: " + _floatEvent.GetPersistentEventCount() );
			Debug.Log( "_floatEvent TotalEventCount: " + _floatEvent.GetTotalEventCount() );

			// ExposedUnityEvent<T,U>.
			_floatIntEvent.AddPersistentListener( ReceviceFloatInt1 );
			_floatIntEvent.AddPersistentListener( ReceviceFloatInt2 );
			_floatIntEvent.AddPersistentListener( ReceviceFloatInt3 );
			_floatIntEvent.RemovePersistentListener( ReceviceFloatInt2 );
			_floatIntEvent.AddListener( ReceviceFloatInt1 );
			_floatIntEvent.AddListener( ReceviceFloatInt2 );
			_floatIntEvent.AddListener( ReceviceFloatInt3 );
			_floatIntEvent.RemoveListener( ReceviceFloatInt2 );
			Debug.Log( "_floatIntEvent RuntimeEventCount: " + _floatIntEvent.GetEventCount() );
			Debug.Log( "_floatIntEvent PersistentEventCount: " + _floatIntEvent.GetPersistentEventCount() );
			Debug.Log( "_floatIntEvent TotalEventCount: " + _floatIntEvent.GetTotalEventCount() );

			// ExposedUnityEvent<T,U,V>.
			_floatIntStringEvent.AddPersistentListener( ReceviceFloatIntString1 );
			_floatIntStringEvent.AddPersistentListener( ReceviceFloatIntString2 );
			_floatIntStringEvent.AddPersistentListener( ReceviceFloatIntString3 );
			_floatIntStringEvent.RemovePersistentListener( ReceviceFloatIntString2 );
			_floatIntStringEvent.AddListener( ReceviceFloatIntString1 );
			_floatIntStringEvent.AddListener( ReceviceFloatIntString2 );
			_floatIntStringEvent.AddListener( ReceviceFloatIntString3 );
			_floatIntStringEvent.RemoveListener( ReceviceFloatIntString2 );
			Debug.Log( "_floatIntStringEvent RuntimeEventCount: " + _floatIntStringEvent.GetEventCount() );
			Debug.Log( "_floatIntStringEvent PersistentEventCount: " + _floatIntStringEvent.GetPersistentEventCount() );
			Debug.Log( "_floatIntStringEvent TotalEventCount: " + _floatIntStringEvent.GetTotalEventCount() );

			// ExposedUnityEvent<T,U,V,X>.
			_floatIntStringBoolEvent.AddPersistentListener( ReceviceFloatIntStringBool1 );
			_floatIntStringBoolEvent.AddPersistentListener( ReceviceFloatIntStringBool2 );
			_floatIntStringBoolEvent.AddPersistentListener( ReceviceFloatIntStringBool3 );
			_floatIntStringBoolEvent.RemovePersistentListener( ReceviceFloatIntStringBool2 );
			_floatIntStringBoolEvent.AddListener( ReceviceFloatIntStringBool1 );
			_floatIntStringBoolEvent.AddListener( ReceviceFloatIntStringBool2 );
			_floatIntStringBoolEvent.AddListener( ReceviceFloatIntStringBool3 );
			_floatIntStringBoolEvent.RemoveListener( ReceviceFloatIntStringBool2 );
			Debug.Log( "_floatIntStringBoolEvent RuntimeEventCount: " + _floatIntStringBoolEvent.GetEventCount() );
			Debug.Log( "_floatIntStringBoolEvent PersistentEventCount: " + _floatIntStringBoolEvent.GetPersistentEventCount() );
			Debug.Log( "_floatIntStringBoolEvent TotalEventCount: " + _floatIntStringBoolEvent.GetTotalEventCount() );
		}


		void Recevice1() { }
		void Recevice2() { }
		void Recevice3() { }

		void ReceviceFloat1( float floatValue ) { }
		void ReceviceFloat2( float floatValue ) { }
		void ReceviceFloat3( float floatValue ) { }

		void ReceviceFloatInt1( float floatValue, int intValue ) { }
		void ReceviceFloatInt2( float floatValue, int intValue ) { }
		void ReceviceFloatInt3( float floatValue, int intValue ) { }

		void ReceviceFloatIntString1( float floatValue, int intValue, string stringValue ) { }
		void ReceviceFloatIntString2( float floatValue, int intValue, string stringValue ) { }
		void ReceviceFloatIntString3( float floatValue, int intValue, string stringValue ) { }

		void ReceviceFloatIntStringBool1( float floatValue, int intValue, string stringValue, bool boolValue ) { }
		void ReceviceFloatIntStringBool2( float floatValue, int intValue, string stringValue, bool boolValue ) { }
		void ReceviceFloatIntStringBool3( float floatValue, int intValue, string stringValue, bool boolValue ) { }
	}
}