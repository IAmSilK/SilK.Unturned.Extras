﻿using OpenMod.API.Eventing;
using OpenMod.Core.Plugins.Events;
using SilK.Unturned.Extras.Events;
using System.Threading.Tasks;

namespace SilK.Unturned.Extras.Plugins
{
    public class PluginLoadedEventListener : IEventListener<PluginLoadedEvent>
    {
        private readonly IEventSubscriber _eventSubscriber;

        public PluginLoadedEventListener(IEventSubscriber eventSubscriber)
        {
            _eventSubscriber = eventSubscriber;
        }

        public Task HandleEventAsync(object? sender, PluginLoadedEvent @event)
        {
            _eventSubscriber.Subscribe(@event.Plugin, @event.Plugin);
            _eventSubscriber.SubscribeServices(@event.Plugin.GetType().Assembly, @event.Plugin);

            return Task.CompletedTask;
        }
    }
}
