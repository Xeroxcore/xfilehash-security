namespace xfilehash
{
    public static class Validation
    {
        public static bool ObjectIsNull(object obj)
            => obj is null;

        public static bool StringsAreEqual(string text, string text2)
            => text.ToLower() == text2.ToLower();
    }
}