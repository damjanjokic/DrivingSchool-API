namespace DrivingSchool.Infrastructure.Interfaces;

public interface IUserAccessor
{
    Guid GetCurrentUserId();
}