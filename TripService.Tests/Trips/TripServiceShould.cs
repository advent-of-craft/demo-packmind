using FluentAssertions;
using LanguageExt;
using TripService.Exception;
using TripService.Trips;
using TripService.Users;
using Xunit;
using static TripService.Tests.Trips.SeamTripService;
using static TripService.Tests.Users.UserBuilder;

namespace TripService.Tests.Trips
{
    public class TripServiceShould
    {
        private static readonly User RegisteredUser = AUser().Build();

        public class Return : TripServiceShould
        {
            private static readonly Trip Lisbon = new();
            private static readonly Trip Springfield = new();
            private static readonly User AnotherUser = AUser().Build();

            [Fact]
            public void No_Trips_When_Logged_User_Is_Not_A_Friend_Of_The_Target_User()
            {
                var aUserWithTrips = AUser()
                    .FriendsWith(AnotherUser)
                    .TravelledTo(Lisbon)
                    .Build();

                TripServiceWithLoggedUser(RegisteredUser)
                    .GetTripsByUser(aUserWithTrips)
                    .Should()
                    .BeEmpty();
            }

            [Fact]
            public void All_The_Target_User_Trips_When_Logged_User_Is_A_Friend_Of_The_Target_User()
                => AUser()
                    .FriendsWith(AnotherUser, RegisteredUser)
                    .TravelledTo(Lisbon, Springfield).Build()
                    .Let(aUserWithTrips =>
                    {
                        TripServiceWithLoggedUser(RegisteredUser)
                            .SimulateDaoWith(aUserWithTrips.Trips())
                            .GetTripsByUser(aUserWithTrips)
                            .Should()
                            .ContainInOrder(Lisbon, Springfield);
                    });
        }

        public class ReturnAnError : TripServiceShould
        {
            private readonly User? _guest = null;

            [Fact]
            public void When_User_Is_Not_LoggedIn()
            {
                var action = () => TripServiceWithLoggedUser(_guest).GetTripsByUser(RegisteredUser);
                action.Should().ThrowExactly<UserNotLoggedInException>();
            }
        }
    }

    internal sealed class SeamTripService : TripService.Trips.TripService
    {
        private readonly User? _loggedUser;
        private Seq<Trip> _daoResult;
        private SeamTripService(User? loggedUser) => _loggedUser = loggedUser;
        public static SeamTripService TripServiceWithLoggedUser(User? loggedUser) => new(loggedUser);
        protected override User? GetLoggedUser() => _loggedUser;

        public SeamTripService SimulateDaoWith(Seq<Trip> trips)
        {
            _daoResult = trips;
            return this;
        }

        protected override List<Trip> FindTripsByUser(User user) => _daoResult.ToList();
    }
}