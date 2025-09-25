using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public class MeasurementResult
    {
        public int NumberOfCycles { get; }
        public int NumberOfFlatCycles { get; }
        public int NumberOfBentCycles { get; }
        public List<int> TestsOk { get; }
        public List<int> TestsFlatOk { get; }
        public List<int> TestsBentOk { get; }
        public List<int> CardIds { get; }

        public List<bool> Result { get; }

        public MeasurementResult(int numberOfCycles, int numberOfFlatCycles, int numberOfBentCycles, List<int> testsOk, List<int> testsFlatOk, List<int> testsBentOk, List<int> cardIds)
        {
            if (testsOk == null) throw new ArgumentNullException(nameof(testsOk));
            if (testsFlatOk == null) throw new ArgumentNullException(nameof(testsFlatOk));
            if (testsBentOk == null) throw new ArgumentNullException(nameof(testsBentOk));
            if (cardIds == null) throw new ArgumentNullException(nameof(cardIds));

            NumberOfCycles = numberOfCycles;
            NumberOfFlatCycles = numberOfFlatCycles;
            NumberOfBentCycles = numberOfBentCycles;
            TestsOk = testsOk;
            TestsFlatOk = testsFlatOk;
            TestsBentOk = testsBentOk;
            CardIds = cardIds;

            Result = cardIds
                .Select((id, index) => testsOk[index] == numberOfCycles &&
                                                          testsFlatOk[index] == numberOfFlatCycles &&
                                                          testsBentOk[index] == numberOfBentCycles).ToList();
        }

        public static MeasurementResult Empty()
        {
            return new MeasurementResult(0, 0, 0, [], [], [], []);
        }
    }
}
