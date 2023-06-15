namespace EmployeeProjectTrackerAPI.Helpers
{
    public static class PredicateExtensions
    {
        public static Func<T, bool> ComposeAnd<T>(this Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return x => predicate1(x) && predicate2(x);
        }
        public static Func<T, bool> ComposeOr<T>(this Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return x => predicate1(x) || predicate2(x);
        }
    }
}
