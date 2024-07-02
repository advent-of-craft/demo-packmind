using TripService.Exception;
using TripService.Users;

namespace TripService.Trips
{
    public class TripDAO
    {
        public static List<Trips.Trip> FindTripsByUser(User user)
        {
            throw new CollaboratorCallException("TripDAO should not be invoked on an unit test.");
        }
    }
}