# SignalR IoT Cluster Example

This repo contains a bare-bone application that enables multiple IoT devices to act as a single cluster and communicate via SignalR.

Clustering allows multiple IoT devices to work as a single distributed IoT application. Inside each cluster, the devices are aware of each other and coordinate their work accordingly.

There are many real-world application for this concept. For example, you may have IoT devices that are making real-time audio announcements at departure gates of an airport. If the gates are in a close proximity to each other, you may arrange the devices into a single cluster, so each device knows when any other device is making an announcement, which would allow the devices not to interrupt each other.

Another use could be to create a local system with redundancy and failover. The devices in the same cluster can synchronise the state and become aware if any of them goes offline.

With SignalR, arranging devices into distinct clusters can be done by using [groups](https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups), while regestering individual devices can be done by using any caching mechanism, which may be in-memory dictionary for the most basic applications or something like [Redis](https://redis.io/) for more advanced ones.
