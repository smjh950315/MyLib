using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using String = MyLib.String;

namespace MyLib.Mvc
{
    //public class TemplateHub : Microsoft.AspNetCore.SignalR.Hub
    //{
    //    protected static List<HubConnection> Connections = new();
    //    public override Task OnDisconnectedAsync(Exception? ex)
    //    {
    //        var Connection = Connections.Find(c => c.ConnectionID == Context.ConnectionId);
    //        if (Connection != null) 
    //        {
    //            Connections.Remove(Connection);
    //        }            
    //        return base.OnDisconnectedAsync(ex);
    //    }
    //    public void ConnectToGroup(object? groupId)
    //    {
    //        string? GroupId = String.GetValidContent(groupId);
    //        if (GroupId == null)
    //        {
    //            return;
    //        }
    //        HubConnection newConnection = new HubConnection(Context.ConnectionId, GroupId);
    //        Connections.Add(newConnection);
    //    }
    //}
    //public class HubConnection
    //{
    //    public bool IsConnected { get; private set; }
    //    public string? ConnectionID { get; private set; }
    //    public string? ClientId { get; set; } 
    //    public void SetClientId(object? clientId)
    //    {
    //        ClientId = clientId?.ToString();
    //    }
    //    public void SetConnectionId(string? connectionId)
    //    {
    //        ConnectionID = connectionId;
    //        if (ConnectionID != null)
    //        {                
    //            IsConnected = true;
    //        }
    //        else 
    //        {
    //            IsConnected = false; 
    //        }
    //    }
    //    public HubConnection() : this(null, null)
    //    {
    //    }
    //    public HubConnection(string? connectionId, string? clientId)
    //    {
    //        SetClientId(clientId);
    //        SetConnectionId(connectionId);
    //    }

    //}
}
