
namespace InfraShapeApp.Models;
using System;


public class SyncState
{
    public bool IsSyncEnabled { get; set; }
    public DateTime? LastSyncDate { get; set; }
    public int PendingItemCount { get; set; }
}
