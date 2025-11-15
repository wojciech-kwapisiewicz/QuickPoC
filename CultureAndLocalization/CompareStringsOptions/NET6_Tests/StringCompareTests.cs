using System.Globalization;
using Xunit;
using Xunit.Abstractions;

namespace NET6_Tests
{
    public class CompareIgnoreCaseAndAccent
    {
        private ITestOutputHelper _output;

        public CompareIgnoreCaseAndAccent(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void StringCompare_IgnoreNonSpaceForVariantsOfE_OutputIfEqual()
        {
            var charactersWithAccents = new List<string>()
            {
                "C", "é", "è",  "ê", "ë", "e", "E", "Ę", "ę"
            };

            for (int i = 0; i < charactersWithAccents.Count; i++)
            {
                for (int j = i + 1; j < charactersWithAccents.Count; j++)
                {
                    _output.WriteLine(string.Format("Comparison result {0} for: {1}  {2}",
                        string.Compare(charactersWithAccents[i], charactersWithAccents[j],
                            CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace),
                        charactersWithAccents[i],
                        charactersWithAccents[j]));
                }
            }
        }        
    }
}