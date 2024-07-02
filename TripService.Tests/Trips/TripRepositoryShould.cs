using FluentAssertions;
using TripService.Exception;
using TripService.Tests.Users;
using TripService.Trips;
using Xunit;

namespace TripService.Tests.Trips
{
    public class TripRepositoryShould
    {
        private readonly ITripRepository _tripRepository = new TripRepository();
        private class TripRepository : ITripRepository;

        [Fact]
        public void Retrieve_User_Trips_Throw_An_Exception()
        {
            var findTrips = () => _tripRepository.FindTripsByUser(UserBuilder.AUser().Build());
            findTrips.Should().Throw<CollaboratorCallException>();
        }
    }
}