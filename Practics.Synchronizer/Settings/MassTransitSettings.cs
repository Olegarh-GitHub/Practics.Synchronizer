namespace Practics.Synchronizer.Settings
{
    public class MassTransitSettings
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseDelayedMessageScheduler { get; set; }
    }
}