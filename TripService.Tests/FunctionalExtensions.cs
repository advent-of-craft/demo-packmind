namespace TripService.Tests
{
    public static class FunctionalExtensions
    {
        public static T Do<T>(this T obj, Action<T> action)
        {
            if (obj != null) action(obj);
            return obj;
        }
    }
}