using LanguageExt;
using TripService.Users;

namespace TripService.Trips
{
    public interface ITripRepository
    {
        public Seq<Trip> FindTripsByUser(User user) => TripDAO.FindTripsByUser(user).ToSeq();
    }
}