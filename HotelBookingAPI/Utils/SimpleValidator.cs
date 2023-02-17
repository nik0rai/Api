namespace HotelBookingAPI.Utils
{
    public static class SimpleValidator
    {

        /// <summary>
        /// Check if value is not null or empty.
        /// </summary>
        /// <param name="values">Values to check/</param>
        /// <returns>True if everyting is good.</returns>
        /// <exception cref="ArgumentNullException">String or value can`t be null.</exception>
        public static bool NotNull(params object[] values)
        {
            var result = NotNull(values);

            foreach (var value in values)
            {
                try
                {
                    var asString = value as string;
                    result &= string.IsNullOrEmpty(asString)
                        ? throw new ArgumentNullException(nameof(values), "String can`t be null or empty.")
                        : true;
                }
                catch
                {
                    // Value is not string.

                    result &= value is null
                        ? throw new ArgumentNullException(nameof(values), "Value can`t be null.")
                        : true;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if value in determend range.
        /// </summary>
        /// <typeparam name="T">Type of objects.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <param name="low">Lower bound of range.</param>
        /// <param name="high">Upper bound of range.</param>
        /// <returns>True if everything ok.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool InRange<T>(T value, T low, T high) where T : IComparable
        {
            var result = NotNull(value, low, high)
                            && low.Equals(high)
                                    ? throw new ArgumentOutOfRangeException(nameof(low), $"Can`t be equal to {nameof(high)}")
                                    : true;

            result &= value.CompareTo(low) < 0 // Check if value is less then low.
                ? throw new ArgumentOutOfRangeException(nameof(value), $"Can`t be less then {nameof(low)}") 
                : true;

            result &= value.CompareTo(high) > 0 // Check if value is higher then heigh.
                ? throw new ArgumentOutOfRangeException(nameof(value), $"Can`t be higher then {nameof(high)}")
                : true;

            return result;
        }
    }
}
