# Yet Another Event Bus For Unity

## How to install:
via PackageManager - https://github.com/Metsker/Yet-Another-Event-Bus-For-Unity.git?path=Package

## How to use:
```
using YetAnotherEventBus;
using UnityEngine;

public struct ExampleEvent : IEvent
{
  public string data;
}

public class ExampleSubscriber : MonoBehaviour
{
    private void OnEnable() =>
      EventBus.Register<ExampleEvent>(OnExampleEvent);

    private void OnDisable() =>
      EventBus.Deregister<ExampleEvent>(OnExampleEvent);

    private void OnExampleEvent(ExampleEvent exampleEvent)
    {
      print(exampleEvent.data);
    }
}

public class ExamplePublisher : MonoBehaviour
{
    private void Start()
    {
      EventBus.Raise(new ExampleEvent(){ data = "Hello world!" });
    }
}
```
