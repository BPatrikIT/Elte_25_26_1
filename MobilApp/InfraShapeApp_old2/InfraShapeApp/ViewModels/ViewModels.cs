using System.Collections.ObjectModel;
using InfraShapeApp.Models;
using InfraShapeApp.Services;

namespace InfraShapeApp.ViewModels;
public class ProfileViewModel{public UserProfile Profile{get;} public ProfileViewModel(IUserService s)=>Profile=s.Get();}
public class PassesViewModel{public ObservableCollection<Pass> Passes{get;} public PassesViewModel(IPassService s)=>Passes=new(s.Get());}
public class ProgressViewModel{public ObservableCollection<ProgressEntry> Items{get;} public ProgressViewModel(IProgressService s)=>Items=new(s.Get());}
public class DataEntryViewModel{public double Weight{get;set;} public Command SaveCommand{get;} public DataEntryViewModel(IProgressService s)=>SaveCommand=new(()=>s.Add(new ProgressEntry(Weight)));}
public class BookingsViewModel{public ObservableCollection<Booking> Items{get;} public BookingsViewModel(IBookingService s)=>Items=new(s.Get());}
public class MachinesViewModel{public ObservableCollection<MachineInfo> Items{get;}=new(){new("InfraShape"),new("RollShape")};}
