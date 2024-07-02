using LanguageExt;
using TripService.Trips;

namespace TripService.Users
{
    public class User(Seq<Trip> trips, Seq<User> friends)
    {
        public Seq<User> GetFriends() => friends;
        public Seq<Trip> Trips() => trips;
        public User AddFriend(User user) => new(trips, friends.Add(user));
        public User AddTrip(Trip trip) => new(trips.Add(trip), friends);
        public bool IsFriendWith(User anotherUser) => friends.Contains(anotherUser);
    }
}