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

using UnityEngine.Events;
using System;
using ExposedUnityEventTools;

[Serializable]
public abstract class ExposedUnityEvent<T,U,V,X> : UnityEvent<T,U,V,X>
{
	object _persistentCallsObject;
	object _callsObject;


	/// <summary>
	/// Adds a persistent listener. Beware this is a hack using reflection.
	/// </summary>
	public void AddPersistentListener( UnityAction<T,U,V,X> action, UnityEventCallState state = UnityEventCallState.RuntimeOnly )
	{
		if( !UnityEventReflection.TryAccessPersistentCalls( this, ref _persistentCallsObject ) ) return;
		int index = GetPersistentEventCount();
		UnityEventReflection.AddPersistentListner( _persistentCallsObject, index, action.Target as UnityEngine.Object, action.Method );
		SetPersistentListenerState( index, state );
	}


	/// <summary>
	/// Removes a persistent listener. Beware this is a hack using reflection.
	/// </summary>
	public void RemovePersistentListener( UnityAction<T,U,V,X> action )
	{
		if( !UnityEventReflection.TryAccessPersistentCalls( this, ref _persistentCallsObject ) ) return;
		UnityEventReflection.RemovePersistentListener( _persistentCallsObject, action.Target as UnityEngine.Object, action.Method );
	}


	/// <summary>
	/// Gets the runtime event count. Beware this is a hack using reflection.
	/// </summary>
	public int GetEventCount()
	{
		if( !UnityEventReflection.TryAccessCalls( this, ref _callsObject ) ) return 0;
		return UnityEventReflection.GetEventCount( _callsObject );
	}


	/// <summary>
	/// Gets the total event count (sum of runtime and persistent events). Beware this is a hack using reflection.
	/// </summary>
	public int GetTotalEventCount()
	{
		if( !UnityEventReflection.TryAccessCalls( this, ref _callsObject ) ) return 0;
		return UnityEventReflection.GetEventCount( _callsObject ) + GetPersistentEventCount();
	}
}