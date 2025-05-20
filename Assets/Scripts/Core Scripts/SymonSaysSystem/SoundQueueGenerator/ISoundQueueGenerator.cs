using System.Collections.Generic;

namespace Unit_Tests {
    public interface ISoundQueueGenerator {
        List<string> GenerateSoundList(int listLength);
    }
}