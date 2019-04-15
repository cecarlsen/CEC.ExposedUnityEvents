CEC.ExposedUnityEvents
======================

Tested with Unity 2018.3.

Extensions of UnityEvent that expose these methods through reflection:

 - AddPersistentListener()
 - RemovePersistentListener()
 - GetEventCount()
 - GetTotalEventCount()

Usage
-----

Extend ExposedUnityEvent, just like you are used to extending UnityEvent.

```
[SerializeField] ExposedFloatEvent _floatEvent;

[Serializable] public class ExposedFloatEvent : ExposedUnityEvent<float> { }

void Awake()
{
	_floatEvent.AddPersistentListener( ReceviceFloat1 );
}

void ReceviceFloat1( float floatValue ) { }

```

The problem
-----------

UnityEvents hold two kinds of listeners; "persistent" and "runtime". These have some differences:

- Persistent listeners are displayed in the inspector, but runtime listeners are not.
- Runtime listeners accessible through scripting, but persistent listeners are not.
- The count is accessible for persistent listeners, but not for runtime listeners.

This can be a great source of annoyance in cases where:

- You need something to happen only if an event has ANY listeners.
- You need ALL your listeners displayed in the inspector.

The reasoning behind having two kinds of events is that persistent events are serialized (only allowing references to methods on objects that extend UnityEngine.Object) and runtime events are not serialised (allowing references to any method).

Performance
-----------

As expected, accessing private methods through reflection is slow and creates garbage. However, AddPersistentListener only seems to be about four times slower than AddListener, and AddListener generates plenty of garbage as is, so the difference is bearable. Use the included ExposedUnityEventPerformanceTest scene to test for yourself.

1000 iterations on a 2012 MacBookPro:

Methods                 | Time | Garbage
----------------------- | ---- | -------
Add & Remove runtime    | 13ms | 422 KB
Add & Remove persistent | 40ms | 500 KB
Invoke runtime          | 3ms  | 0 KB
Invoke persistent       | 3ms  | 0 KB


License
-----------
Copyright (c) 2019 Carl Emil Carlsen

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
