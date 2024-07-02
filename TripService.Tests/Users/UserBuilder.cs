using LanguageExt;
using TripService.Trips;
using TripService.Users;
using static LanguageExt.Seq;

namespace TripService.Tests.Users
{
    public class UserBuilder
    {
        private Seq<User> _friends = empty<User>();
        private Seq<Trip> _trips = empty<Trip>();

        public static UserBuilder AUser() => new();

        public UserBuilder FriendsWith(params User[] friends)
            => this.Let(builder => builder._friends = friends.ToSeq());

        public UserBuilder TravelledTo(params Trip[] trips)
            => this.Let(builder => builder._trips = trips.ToSeq());

        public User Build() => new User(_trips, _friends);
    }
}