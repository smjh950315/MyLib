using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MyLib.Mvc
{
    public class Hub : Microsoft.AspNetCore.SignalR.Hub
    {
        protected static List<HubConnection> Connections = new();
        public override Task OnDisconnectedAsync(Exception? ex)
        {
            var Connection = Connections.Find(c => c.ConnectionID == Context.ConnectionId);
            if (Connection != null) 
            {
                Connections.Remove(Connection);
            }            
            return base.OnDisconnectedAsync(ex);
        }
        public void ConnectToGroup(object? groupId)
        {
            string? GroupId = null;
            if (groupId == null) 
            {
                return;
            }
            GroupId = groupId.ToString();
            if (string.IsNullOrEmpty(GroupId))
            {
                return;
            }
            HubConnection newConnection = new HubConnection(Context.ConnectionId, GroupId);
            Connections.Add(newConnection);
        }
    }
    public class HubConnection
    {
        public bool IsConnected { get; set; }
        public string? ConnectionID { get; set; } 
        public dynamic UserID { get; set; }
        public dynamic? GuorpId { get; set; }
        public void SetConnectionId(string connectionId)
        {
            ConnectionID = connectionId;
            IsConnected = true;
        }
        public HubConnection()
        {
            UserID = 0;
            GuorpId = 0;            
            IsConnected = false;
        }
        public HubConnection(string connectionId,string groupId):this()
        {
            GuorpId = groupId;
            IsConnected = false;
            SetConnectionId(connectionId);
        }
    }
}
