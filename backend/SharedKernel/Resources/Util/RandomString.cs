using System.Text;

namespace SharedKernel.Resources.Util
{
    public static class RandomString
    {
        public static string GenerateNumbers(int length)
        {
            var randomNumber = new SecureRandom();
            var newNumber = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                newNumber.Append(randomNumber.Next(0, 10));
            }

            return newNumber.ToString();
        }

        public static string GenerateLetters(int length)
        {
            var letters = "A B C D E F G H I J K L M N P Q R S T U V W X Y Z".Split();
            var randomNumber = new SecureRandom();
            var newLetterString = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                newLetterString.Append(letters[randomNumber.Next(0, 25)]);
            }

            return newLetterString.ToString();
        }

        public static string GenerateRandomLettersAndNumbers(int length)
        {
            var letters = "A B C D E F G H I J K L M N P Q R S T U V W X Y Z 1 2 3 4 5 6 7 8 9".Split();
            var randomNumber = new SecureRandom();
            var newLetterNumberString = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                newLetterNumberString.Append(letters[randomNumber.Next(0, 34)]);
            }

            return newLetterNumberString.ToString();
        }

        public static string GenerateLongGuidCode()
        {
            return new StringBuilder().Insert(0, Guid.NewGuid().ToString(), 4).ToString().Replace("-", "");
        }
    }
}
