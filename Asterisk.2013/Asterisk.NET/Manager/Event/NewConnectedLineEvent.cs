namespace AsterNET.Manager.Event
{
    /// <summary>
    /// A NewConnectedLineEvent is triggered when a channel's connected line information is changed.<br/>
    /// It is implemented in channel.c
    /// </summary>
    public class NewConnectedLineEvent : AbstractChannelEvent
	{
		public NewConnectedLineEvent(ManagerConnection source)
			: base(source)
		{
		}
	}
}