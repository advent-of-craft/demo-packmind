using TripService.Exception;
using TripService.Users;

namespace TripService.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User? loggedUser = GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    tripList = FindTripsByUser(user);
                }

                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        protected virtual User? GetLoggedUser() => UserSession.GetInstance().GetLoggedUser();
        protected virtual List<Trip> FindTripsByUser(User user) => TripDAO.FindTripsByUser(user);
    }
}