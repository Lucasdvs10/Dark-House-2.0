using System.Collections.Generic;

namespace Unit_Tests {
    public interface ISoundQueueGenerator {
        Queue<string> GenerateSoundQueue(int queueLength);
    }
}