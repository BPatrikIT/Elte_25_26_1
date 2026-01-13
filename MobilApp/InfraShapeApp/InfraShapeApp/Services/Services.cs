using InfraShapeApp.Models;
namespace InfraShapeApp.Services;
public interface IUserService{UserProfile Get();}
public interface IPassService{IEnumerable<Pass> Get();}
public interface IProgressService{IEnumerable<ProgressEntry> Get();void Add(ProgressEntry e);}
public interface IBookingService{IEnumerable<Booking> Get();}

public class MockUserService:IUserService{public UserProfile Get()=>new("Test User");}
public class MockPassService:IPassService{public IEnumerable<Pass> Get()=>new[]{new Pass("Monthly")};}
public class MockProgressService:IProgressService{
List<ProgressEntry> d=new();
public IEnumerable<ProgressEntry> Get()=>d;
public void Add(ProgressEntry e)=>d.Add(e);}
public class MockBookingService:IBookingService{public IEnumerable<Booking> Get()=>new[]{new Booking("InfraShape")};}
