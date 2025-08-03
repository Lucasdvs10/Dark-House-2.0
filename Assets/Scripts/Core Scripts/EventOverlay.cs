using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameScripts.GameEvent;

public class EventOverlay : MonoBehaviour
{
    [SerializeField] private Text eventLogText;
    [SerializeField] private List<SOBaseGameEvent> eventsToWatch;

    private Queue<string> recentEvents = new Queue<string>();
    private const int maxLines = 10;

    private Dictionary<SOBaseGameEvent, System.Action> eventHandlers = new Dictionary<SOBaseGameEvent, System.Action>();

    private void OnEnable()
    {
        foreach (var gameEvent in eventsToWatch)
        {
            System.Action handler = () => LogEvent(gameEvent.name);
            gameEvent.Subscribe(handler);
            eventHandlers[gameEvent] = handler;
        }
    }

    private void OnDisable()
    {
        foreach (var gameEvent in eventsToWatch)
        {
            if (eventHandlers.TryGetValue(gameEvent, out var handler))
            {
                gameEvent.Unsubscribe(handler);
            }
        }

        eventHandlers.Clear();
    }

    private void LogEvent(string eventName)
    {
        if (recentEvents.Count >= maxLines)
        {
            recentEvents.Dequeue();
        }

        recentEvents.Enqueue($"Event: {eventName} at {Time.time:F2}s");
        eventLogText.text = string.Join("\n", recentEvents.ToArray());
    }
}