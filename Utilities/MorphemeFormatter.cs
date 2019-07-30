namespace Utilities
{
    public static class MorphemeFormatter
    {
        public static string FormatMorpheme(this string value, int number)
        {
            return number + " " + (number == 1 ? value : value + "s");
        }

        public static string FormatMorpheme(this int number, string value)
        {
            return value.FormatMorpheme(number);
        }
    }
}
