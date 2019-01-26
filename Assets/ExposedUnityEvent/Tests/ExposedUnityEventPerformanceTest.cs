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
using UnityEngine.Events;

namespace ExposedUnityEventTools
{
	public class ExposedUnityEventPerformanceTest : MonoBehaviour
	{
		[SerializeField] FloatEvent _floatEvent = new FloatEvent();
		[SerializeField] ExposedFloatEvent _exposedFloatEvent = new ExposedFloatEvent();

		[Serializable] public class FloatEvent : UnityEvent<float> { }
		[Serializable] public class ExposedFloatEvent : ExposedUnityEvent<float> { }


		void Update()
		{
			AddAndRemoveRuntime();
			AddAndRemovePersistent();

			_floatEvent.AddListener( ReceviceFloat1 );
			_exposedFloatEvent.AddPersistentListener( ReceviceFloat1 );

			InvokeRuntime();
			InvokePersistent();

			_floatEvent.RemoveListener( ReceviceFloat1 );
			_exposedFloatEvent.RemovePersistentListener( ReceviceFloat1 );
		}


		void AddAndRemoveRuntime()
		{
			for( int i = 0; i < 1000; i++ ) {
				_floatEvent.AddListener( ReceviceFloat1 );
				_floatEvent.RemoveListener( ReceviceFloat1 );
			}
		}


		void AddAndRemovePersistent()
		{
			for( int i = 0; i < 1000; i++ ) {
				_exposedFloatEvent.AddPersistentListener( ReceviceFloat1 );
				_exposedFloatEvent.RemovePersistentListener( ReceviceFloat1 );
			}
		}


		void InvokeRuntime()
		{
			for( int i = 0; i < 1000; i++ ) {
				_floatEvent.Invoke( 0f );
			}
		}

		void InvokePersistent()
		{
			for( int i = 0; i < 1000; i++ ) {
				_exposedFloatEvent.Invoke( 0f );
			}
		}

		void ReceviceFloat1( float floatValue ) { }
	}
}