namespace CvGenerator.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        private const string Format = "yyyy-MM-dd";

        public static string ToCustomString(this DateTime dateTime)
            => dateTime.ToString(Format) ?? "Present";

        public static string ToCustomString(this DateTime? dateTime)
            => dateTime?.ToString(Format) ?? "Present";
    }
}
