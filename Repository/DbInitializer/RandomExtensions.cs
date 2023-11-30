namespace Repository.DbInitializer {
    public static class RandomExtensions {
        public static DateTime NextDate(this Random rand, int startYear, int endYear) {
            var day = rand.Next(1, 29);
            var month = rand.Next(1, 13);
            var year = rand.Next(startYear, endYear);
            return new DateTime(year, month, day);
        }

        public static T NextItem<T>(this Random rand, IList<T> items) {
            return items[rand.Next(0, items.Count)];
        }

        public static string NextPhoneNumber(this Random rand) {
            return $"+375 {rand.Next(10, 100)} {rand.Next(100, 1000)}-{rand.Next(10, 100)}-{rand.Next(10, 100)}";
        }

        public static string NextPassportNumber(this Random rand) {
            return $"HB{rand.Next(1000000, 10000000)}";
        }

        public static string NextPolicyNumber(this Random rand) {
            return $"{rand.NextInt64(1000000000000000, 10000000000000000)}";
        }
    }
}
